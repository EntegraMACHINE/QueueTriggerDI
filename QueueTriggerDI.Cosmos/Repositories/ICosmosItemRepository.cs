using Microsoft.Azure.Cosmos;
using QueueTriggerDI.Cosmos.Entities;
using System.Net;
using System.Threading.Tasks;

namespace QueueTriggerDI.Cosmos.Repositories
{
    public interface ICosmosItemRepository<T> where T : class, ICosmosItem
    {
        Task<(T, HttpStatusCode)> AddItemAsync(Container container, T entity, PartitionKey partitionKey);

        Task<T> GetItemAsync(Container container, string itemId, PartitionKey partitionKey);
    }
}
