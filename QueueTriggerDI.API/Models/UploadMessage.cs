using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Storage.Models;

namespace QueueTriggerDI.API.Models
{
    public class UploadMessage
    {
        public BlobParameters Parameters { get; set; }

        public BookDto Book { get; set; }
    }
}
