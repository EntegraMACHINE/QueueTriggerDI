using QueueTriggerDI.Storage.Models;
using System.Collections.Generic;

namespace QueueTriggerDI.Storage.Services
{
    public interface IBlobStorageService
    {
        string MakeSnapshot(BlobParametersDto blobParameters);

        void UploadBlob<T>(BlobParametersDto blobParameters, T content);

        void StageBlock<T>(BlobParametersDto blobParameters, T content);

        void CommitBlocks(BlobParametersDto blobParameters);

        T DownloadBlob<T>(BlobParametersDto blobParameters);

        IList<T> DownloadBlocksBlob<T>(BlobParametersDto blobParameters);
    }
}
