using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Configuration;
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
            CheckBlobContainerExist(blobContainerClient);

            return blobContainerClient.GetBlobClient(blobName);
        }

        public BlockBlobClient GetBlockBlobClient(BlobContainerClient blobContainerClient, string blobName)
        {
            CheckBlobContainerExist(blobContainerClient);

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

        private void CheckBlobContainerExist(BlobContainerClient blobContainerClient)
        {
            if (!blobContainerClient.Exists())
            {
                string message = $"Container {blobContainerClient.Name} does not exist!";

                logger.LogWarning(message);
                throw new InvalidOperationException(message);
            }
        }
    }
}
