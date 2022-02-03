using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace QueueTriggerDI.Storage.Services
{
    public interface IBlobClientService
    {
        BlobContainerClient GetBlobContainerClient(string containerName);

        BlobClient GetBlobClient(BlobContainerClient blobContainerClient, string blobName);

        BlockBlobClient GetBlockBlobClient(BlobContainerClient blobContainerClient, string blobName);
    }
}
