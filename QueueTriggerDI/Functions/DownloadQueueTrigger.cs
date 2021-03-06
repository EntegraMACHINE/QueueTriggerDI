using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Context.Services;
using QueueTriggerDI.DTO;
using QueueTriggerDI.Storage.Services;

namespace QueueTriggerDI.Functions
{
    public class DownloadQueueTrigger
    {
        private readonly IBlobStorageService blobStorageService;
        private readonly IBookService bookService;
        private readonly ILogger logger;

        public DownloadQueueTrigger(IBlobStorageService blobStorageService, IBookService bookService, ILogger<DownloadQueueTrigger> logger)
        {
            this.blobStorageService = blobStorageService;
            this.bookService = bookService;
            this.logger = logger;
        }

        [FunctionName("DownloadQueueTrigger")]
        public void Run([QueueTrigger("download-queue", Connection = "AzureWebJobsStorage")] string message)
        {
            logger.LogInformation($"C# Queue trigger function processed: {message}");

            DownloadMessageDto downloadMessage = JsonConvert.DeserializeObject<DownloadMessageDto>(message);

            BookDto book = blobStorageService.DownloadBlob<BookDto>(downloadMessage.Parameters);
            bookService.AddBook(book);
        }
    }
}
