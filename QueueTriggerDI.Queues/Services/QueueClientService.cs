using Azure.Core;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using QueueTriggerDI.Utils.Checkers;
using System;

namespace QueueTriggerDI.Queues.Services
{
    public class QueueClientService : IQueueClientService
    {
        private readonly QueueServiceSettings settings;

        public QueueClientService(IOptions<QueueServiceSettings> settings)
        {
            this.settings = settings.Value;
        }

        public QueueClient GetQueueClient(string queueName)
        {
            Verify.NotEmpty(nameof(queueName), queueName);

            QueueClientOptions options = new QueueClientOptions();
            options.MessageEncoding = QueueMessageEncoding.Base64;
            options.Retry.Mode = RetryMode.Exponential;
            options.Retry.Delay = TimeSpan.FromSeconds(settings.DelayInSeconds);
            options.Retry.MaxRetries = settings.MaxRetry;

            return new QueueClient(settings.ConnectionString, queueName, options);
        }
    }
}
