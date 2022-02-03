using Azure.Storage.Queues;

namespace QueueTriggerDI.Queues.Services
{
    public interface IQueueClientService
    {
        QueueClient GetQueueClient(string queueName);
    }
}
