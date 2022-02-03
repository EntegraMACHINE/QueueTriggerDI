using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Context.Services;
using QueueTriggerDI.Models;
using QueueTriggerDI.Storage.Services;
using System.Collections.Generic;

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

            DownloadMessage downloadMessage = JsonConvert.DeserializeObject<DownloadMessage>(message);

            BookDto book = blobStorageService.DownloadBlob<BookDto>(downloadMessage.Parameters);
            bookService.AddBook(book);
        }
    }
}
