using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace QueueTriggerDI.Storage.Services
{
    public class BlobClientService : IBlobClientService
    {
        private readonly ILogger logger;
        private readonly BlobServiceSettings settings;

        public BlobClientService(IOptions<BlobServiceSettings> settings, ILogger<BlobClientService> logger)
        {
            this.logger = logger;
            this.settings = settings.Value;
        }

        public BlobClient GetBlobClient(BlobContainerClient blobContainerClient, string blobName)
        {
            if(!CheckBlobContainerExist(blobContainerClient))
                return null;

            return blobContainerClient.GetBlobClient(blobName);
        }

        public BlockBlobClient GetBlockBlobClient(BlobContainerClient blobContainerClient, string blobName)
        {
            if (!CheckBlobContainerExist(blobContainerClient))
                return null;

            return blobContainerClient.GetBlockBlobClient(blobName);
        }

        public BlobContainerClient GetBlobContainerClient(string containerName)
        {
            return GetBlobServiceClient().GetBlobContainerClient(containerName);
        }

        private BlobServiceClient GetBlobServiceClient()
        {
            BlobClientOptions blobClientOptions = new BlobClientOptions();
            blobClientOptions.Retry.Mode = RetryMode.Exponential;
            blobClientOptions.Retry.Delay = TimeSpan.FromSeconds(settings.DelayInSeconds);
            blobClientOptions.Retry.MaxRetries = settings.MaxRetry;

            return new BlobServiceClient(settings.ConnectionString, blobClientOptions);
        }

        private bool CheckBlobContainerExist(BlobContainerClient blobContainerClient)
        {
            if (!blobContainerClient.Exists())
            {
                logger.LogWarning($"Container {blobContainerClient.Name} does not exist!");
                return false;
            }

            return true;
        }
    }
}
