using QueueTriggerDI.Storage.Models;

namespace QueueTriggerDI.API.Models
{
    public class DownloadMessage
    {
        public BlobParameters Parameters { get; set; }
    }
}
