using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;

namespace QueueTriggerDI.Queues.Services
{
    public class QueueClientService : IQueueClientService
    {
        private readonly IConfiguration configuration;

        public QueueClientService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public QueueClient GetQueueClient(string queueName)
        {
            return new QueueClient(
                configuration.GetConnectionString("QueueStorageConnectionString"), 
                queueName, 
                new QueueClientOptions()
                {
                    MessageEncoding = QueueMessageEncoding.Base64
                });
        }
    }
}
