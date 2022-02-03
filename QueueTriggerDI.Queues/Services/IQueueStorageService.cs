using Azure.Storage.Queues.Models;
using System.Collections.Generic;

namespace QueueTriggerDI.Queues.Services
{
    public interface IQueueStorageService
    {
        IList<QueueMessage> GetQueueMessages(string queueName);

        string SendMessage(string queueName, string message);

        void ClearQueue(string queueName);
    }
}
