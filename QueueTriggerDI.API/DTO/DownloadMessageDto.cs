using QueueTriggerDI.Storage.Models;

namespace QueueTriggerDI.API.DTO
{
    public class DownloadMessageDto
    {
        public BlobParametersDto Parameters { get; set; }
    }
}
