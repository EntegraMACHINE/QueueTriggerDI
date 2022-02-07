using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QueueTriggerDI.API.Models;
using QueueTriggerDI.Queues.Services;

namespace QueueTriggerDI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IQueueStorageService queueStorageService;

        private readonly string downloadQueueName;
        private readonly string uploadQueueName;

        public MessagesController(IQueueStorageService queueStorageService, IConfiguration configuration)
        {
            this.queueStorageService = queueStorageService;

            downloadQueueName = configuration.GetSection("Queues:DownloadQueue").Value;
            uploadQueueName = configuration.GetSection("Queues:UploadQueue").Value;
        }

        [HttpGet]
        [Route("[controller]/get-queue-messages")]
        public IActionResult GetQueueMessages(string queueName)
        {
            return Ok(JsonConvert.SerializeObject(queueStorageService.GetQueueMessages(queueName)));
        }

        [HttpPost]
        [Route("[controller]/send-download-message")]
        public IActionResult SendDownloadMessage([FromBody] DownloadMessageDto message)
        {
            return Ok(queueStorageService.SendMessage(downloadQueueName, JsonConvert.SerializeObject(message)));
        }

        [HttpPost]
        [Route("[controller]/send-upload-message")]
        public IActionResult SendUploadMessage([FromBody] UploadMessage message)
        {
            return Ok(queueStorageService.SendMessage(uploadQueueName, JsonConvert.SerializeObject(message)));
        }

        [HttpDelete]
        [Route("[controller]/clear-queue")]
        public IActionResult ClearQueue(string queueName)
        {
            queueStorageService.ClearQueue(queueName);

            return Ok();
        }
    }
}
