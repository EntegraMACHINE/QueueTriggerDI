using Microsoft.Azure.Cosmos;
using QueueTriggerDI.Cosmos.Entities;
using System.Net;
using System.Threading.Tasks;

namespace QueueTriggerDI.Cosmos.Repositories
{
    public class CosmosItemRepository<T> : ICosmosItemRepository<T> where T : class, ICosmosItem
    {
        public async Task<(T, HttpStatusCode)> AddItemAsync(Container container, T entity, PartitionKey partitionKey)
        {
            try
            {
                await container.ReadItemAsync<T>(entity.Id, partitionKey);
                return (entity, HttpStatusCode.OK);
            }
            catch
            {
                ItemResponse<T> response = await container.CreateItemAsync(entity);
                return (response.Resource, response.StatusCode);
            }
        }

        public async Task<T> GetItemAsync(Container container, string itemId, PartitionKey partitionKey)
        {
            ItemResponse<T> response = await container.ReadItemAsync<T>(itemId, partitionKey);
            return response.Resource;
        }
    }
}
