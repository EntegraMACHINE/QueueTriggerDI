using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueTriggerDI.Storage.DTO;
using QueueTriggerDI.Utils.Checkers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QueueTriggerDI.Storage.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IBlobClientService blobClientService;
        private readonly ILogger logger;

        public BlobStorageService(IBlobClientService blobClientService, ILogger<BlobStorageService> logger)
        {
            this.blobClientService = blobClientService;
            this.logger = logger;
        }

        public bool UploadBlob<T>(BlobParametersDto blobParameters, T content)
        {
            Verify.NotEmpty(nameof(blobParameters.BlobName), blobParameters.BlobName);
            Verify.NotEmpty(nameof(blobParameters.BlobContainerName), blobParameters.BlobContainerName);
            Verify.NotNullOrDefault(nameof(content), content);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            if(blobContainerClient == null)
                return false;

            BlobClient blobClient = blobClientService.GetBlobClient(blobContainerClient, blobParameters.BlobName);
            if (!CheckBlobExist(blobClient))
                return false;

            using (Stream stream = new MemoryStream())
            {
                blobClient.Upload(ContentToStream(stream, content));
                return true;
            }
        }

        public bool StageBlock<T>(BlobParametersDto blobParameters, T content)
        {
            Verify.NotEmpty(nameof(blobParameters.BlobName), blobParameters.BlobName);
            Verify.NotEmpty(nameof(blobParameters.BlobContainerName), blobParameters.BlobContainerName);
            Verify.NotNullOrDefault(nameof(content), content);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            if (blobContainerClient == null)
                return false;

            BlockBlobClient blockBlobClient = blobClientService.GetBlockBlobClient(blobContainerClient, blobParameters.BlobName);

            if (!CheckBlobExist(blockBlobClient))
                return false;

            using (Stream stream = new MemoryStream())
            {
                blockBlobClient.StageBlock(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), ContentToStream(stream, content));
                return true;
            }
        }

        public bool CommitBlocks(BlobParametersDto blobParameters)
        {
            Verify.NotEmpty(nameof(blobParameters.BlobName), blobParameters.BlobName);
            Verify.NotEmpty(nameof(blobParameters.BlobContainerName), blobParameters.BlobContainerName);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            if (blobContainerClient == null)
                return false;

            BlockBlobClient blockBlobClient = blobClientService.GetBlockBlobClient(blobContainerClient, blobParameters.BlobName);

            if(!CheckBlobExist(blockBlobClient))
                return false;

            Response<BlockList> response = blockBlobClient.GetBlockList();
            blockBlobClient.CommitBlockList(response.Value.UncommittedBlocks.Select(x => x.Name));

            return true;
        }

        public T DownloadBlob<T>(BlobParametersDto blobParameters)
        {
            Verify.NotEmpty(nameof(blobParameters.BlobName), blobParameters.BlobName);
            Verify.NotEmpty(nameof(blobParameters.BlobContainerName), blobParameters.BlobContainerName);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            if (blobContainerClient == null)
                return default;

            BlobClient blobClient = blobClientService.GetBlobClient(blobContainerClient, blobParameters.BlobName);

            if(!CheckBlobExist(blobClient))
                return default;

            Response<BlobDownloadInfo> response = blobClient.Download();

            return GetBlobContent<T>(response);
        }

        public IList<T> DownloadBlocksBlob<T>(BlobParametersDto blobParameters)
        {
            Verify.NotEmpty(nameof(blobParameters.BlobName), blobParameters.BlobName);
            Verify.NotEmpty(nameof(blobParameters.BlobContainerName), blobParameters.BlobContainerName);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            if (blobContainerClient == null)
                return null;

            BlockBlobClient blockBlobClient = blobClientService.GetBlockBlobClient(blobContainerClient, blobParameters.BlobName);

            if(!CheckBlobExist(blockBlobClient))
                return null;

            Response<BlockList> response = blockBlobClient.GetBlockList();

            List<T> result = new List<T>();
            int offset = 0;

            foreach (BlobBlock block in response.Value.CommittedBlocks)
            {
                Response<BlobDownloadInfo> blockData = blockBlobClient.Download(new HttpRange(offset, block.Size));
                result.Add(GetBlobContent<T>(blockData));

                offset += block.Size;
            }

            return result;
        }

        private Stream ContentToStream<T>(Stream stream, T content)
        {
            string result = JsonConvert.SerializeObject(content);
            byte[] bytes = Encoding.ASCII.GetBytes(result);

            stream.Write(bytes, 0, bytes.Length);
            stream.Position = 0;

            return stream;
        }

        private T GetBlobContent<T>(BlobDownloadInfo blobDownloadInfo)
        {
            byte[] result = new byte[blobDownloadInfo.ContentLength];
            blobDownloadInfo.Content.Read(result, 0, (int)blobDownloadInfo.ContentLength);

            return JsonConvert.DeserializeObject<T>(Encoding.ASCII.GetString(result));
        }

        private bool CheckBlobExist(BlobBaseClient blobClient)
        {
            if(blobClient == null)
                return false;

            if (!blobClient.Exists().Value)
            {
                logger.LogWarning($"Blob {blobClient.Name} does not exist!");
                return false;
            }

            return true;
        }
    }
}
