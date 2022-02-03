using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueTriggerDI.Storage.Models;
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

        public string MakeSnapshot(BlobParameters blobParameters)
        {
            CheckBlobParameters(blobParameters);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            BlobClient blobClient = blobClientService.GetBlobClient(blobContainerClient, blobParameters.BlobName);
            
            Response<BlobSnapshotInfo> response = blobClient.CreateSnapshot();
            return response.Value.Snapshot;
        }

        public void UploadBlob<T>(BlobParameters blobParameters, T content)
        {
            CheckBlobParameters(blobParameters);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            BlobClient blobClient = blobClientService.GetBlobClient(blobContainerClient, blobParameters.BlobName);

            using (Stream stream = new MemoryStream())
            {
                ContentToStream(stream, content);

                blobClient.Upload(stream);
            }
        }

        public void StageBlock<T>(BlobParameters blobParameters, T content)
        {
            CheckBlobParameters(blobParameters);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            BlockBlobClient blockBlobClient = blobClientService.GetBlockBlobClient(blobContainerClient, blobParameters.BlobName);

            CheckBlobExist(blockBlobClient);

            using (Stream stream = new MemoryStream())
            {
                ContentToStream(stream, content);

                blockBlobClient.StageBlock(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), stream);
            }
        }

        public void CommitBlocks(BlobParameters blobParameters)
        {
            CheckBlobParameters(blobParameters);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            BlockBlobClient blockBlobClient = blobClientService.GetBlockBlobClient(blobContainerClient, blobParameters.BlobName);

            CheckBlobExist(blockBlobClient);

            Response<BlockList> response = blockBlobClient.GetBlockList();
            blockBlobClient.CommitBlockList(response.Value.UncommittedBlocks.Select(x => x.Name));
        }

        public T DownloadBlob<T>(BlobParameters blobParameters)
        {
            CheckBlobParameters(blobParameters);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            BlobClient blobClient = blobClientService.GetBlobClient(blobContainerClient, blobParameters.BlobName);
            
            CheckBlobExist(blobClient);

            Response<BlobDownloadInfo> response = blobClient.Download();

            return GetBlobContent<T>(response);
        }

        public IList<T> DownloadBlocksBlob<T>(BlobParameters blobParameters)
        {
            CheckBlobParameters(blobParameters);

            BlobContainerClient blobContainerClient = blobClientService.GetBlobContainerClient(blobParameters.BlobContainerName);
            BlockBlobClient blockBlobClient = blobClientService.GetBlockBlobClient(blobContainerClient, blobParameters.BlobName);

            CheckBlobExist(blockBlobClient);

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

        private void CheckBlobExist(BlobBaseClient blobClient)
        {
            if (!blobClient.Exists())
            {
                string message = $"Blob {blobClient.Name} does not exist!";
                logger.LogWarning(message);
                throw new InvalidOperationException(message);
            }
        }

        private void CheckBlobParameters(BlobParameters blobParameters)
        {
            if (string.IsNullOrWhiteSpace(blobParameters.BlobName) || string.IsNullOrWhiteSpace(blobParameters.BlobContainerName))
            {
                string message = "Blob parameters are not valid!";
                logger.LogWarning(message);
                throw new InvalidOperationException(message);
            }
        }
    }
}
