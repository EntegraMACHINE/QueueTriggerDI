using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace QueueTriggerDI.Storage.Services
{
    public class BlobClientService : IBlobClientService
    {
        private readonly ILogger logger;
        private readonly string connectionString;

        public BlobClientService(IConfiguration configuration, ILogger<BlobClientService> logger)
        {
            this.logger = logger;
            connectionString = configuration.GetConnectionString("BlobStorageConnectionString");
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
            return new BlobServiceClient(connectionString);
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
