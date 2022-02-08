using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.DTO;
using QueueTriggerDI.Storage.Services;

namespace QueueTriggerDI.Functions
{
    public class UploadQueueTrigger
    {
        private readonly IBlobStorageService blobStorageService;
        private readonly ILogger logger;

        public UploadQueueTrigger(IBlobStorageService blobStorageService, ILogger<UploadQueueTrigger> logger)
        {
            this.blobStorageService = blobStorageService;
            this.logger = logger;
        }

        [FunctionName("UploadQueueTrigger")]
        public void Run([QueueTrigger("upload-queue", Connection = "AzureWebJobsStorage")]string message)
        {
            logger.LogInformation($"C# Queue trigger function processed: {message}");

            UploadMessageDto uploadMessage = JsonConvert.DeserializeObject<UploadMessageDto>(message);

            blobStorageService.MakeSnapshot(uploadMessage.Parameters);
        }
    }
}
