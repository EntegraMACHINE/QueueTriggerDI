using QueueTriggerDI.Storage.DTO;
using System.Collections.Generic;

namespace QueueTriggerDI.Storage.Services
{
    public interface IBlobStorageService
    {
        bool UploadBlob<T>(BlobParametersDto blobParameters, T content);

        bool StageBlock<T>(BlobParametersDto blobParameters, T content);

        bool CommitBlocks(BlobParametersDto blobParameters);

        T DownloadBlob<T>(BlobParametersDto blobParameters);

        IList<T> DownloadBlocksBlob<T>(BlobParametersDto blobParameters);
    }
}
