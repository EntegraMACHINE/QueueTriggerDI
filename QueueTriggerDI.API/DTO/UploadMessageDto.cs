using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Storage.DTO;

namespace QueueTriggerDI.API.DTO
{
    public class UploadMessageDto
    {
        public BlobParametersDto Parameters { get; set; }

        public BookDto Book { get; set; }
    }
}
