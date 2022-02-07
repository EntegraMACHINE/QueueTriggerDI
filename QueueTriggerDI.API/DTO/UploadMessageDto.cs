using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Storage.Models;

namespace QueueTriggerDI.API.DTO
{
    public class UploadMessageDto
    {
        public BlobParametersDto Parameters { get; set; }

        public BookDto Book { get; set; }
    }
}
