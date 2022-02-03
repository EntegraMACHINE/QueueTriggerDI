using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueTriggerDI.Queues.Services
{
    public class QueueStorageService : IQueueStorageService
    {
        private readonly IQueueClientService queueClientService;

        public QueueStorageService(IQueueClientService queueClientService)
        {
            this.queueClientService = queueClientService;
        }

        public IList<QueueMessage> GetQueueMessages(string queueName)
        {
            QueueClient queueClient = queueClientService.GetQueueClient(queueName);
            CheckQueueClientExist(queueClient);

            int count = queueClient.GetProperties().Value.ApproximateMessagesCount;

            return queueClient.ReceiveMessages(count).Value.ToList();
        }

        public string SendMessage(string queueName, string message)
        {
            try
            {
                QueueClient queueClient = queueClientService.GetQueueClient(queueName);
                CheckQueueClientExist(queueClient);

                queueClient.SendMessage(message);

                return "Message was successfully added!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void ClearQueue(string queueName)
        {
            QueueClient queueClient = queueClientService.GetQueueClient(queueName);
            CheckQueueClientExist(queueClient);

            queueClient.ClearMessages();
        }

        private void CheckQueueClientExist(QueueClient queueClient)
        {
            if (!queueClient.Exists())
                throw new InvalidOperationException($"Queue {queueClient.Name} does not exist!");
        }
    }
}
