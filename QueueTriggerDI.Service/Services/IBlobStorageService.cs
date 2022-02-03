using QueueTriggerDI.Storage.Models;
using System.Collections.Generic;

namespace QueueTriggerDI.Storage.Services
{
    public interface IBlobStorageService
    {
        string MakeSnapshot(BlobParameters blobParameters);

        void UploadBlob<T>(BlobParameters blobParameters, T content);

        void StageBlock<T>(BlobParameters blobParameters, T content);

        void CommitBlocks(BlobParameters blobParameters);

        T DownloadBlob<T>(BlobParameters blobParameters);

        IList<T> DownloadBlocksBlob<T>(BlobParameters blobParameters);
    }
}
