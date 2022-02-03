using QueueTriggerDI.Storage.Models;

namespace QueueTriggerDI.Models
{
    public class DownloadMessage
    {
        public BlobParameters Parameters { get; set; }
    }
}
