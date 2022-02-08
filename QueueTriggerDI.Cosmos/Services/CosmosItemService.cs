using Microsoft.Azure.Cosmos;
using QueueTriggerDI.Cosmos.Entities;
using QueueTriggerDI.Cosmos.DTO;
using QueueTriggerDI.Cosmos.Repositories;
using System.Net;
using System.Threading.Tasks;

namespace QueueTriggerDI.Cosmos.Services
{
    public class CosmosItemService<T> : ICosmosItemService<T> where T : class, ICosmosItem
    {
        private readonly ICosmosDatabaseService cosmosClientService;
        private readonly ICosmosItemRepository<T> cosmosItemRepository;

        public CosmosItemService(ICosmosDatabaseService cosmosClientService, ICosmosItemRepository<T> cosmosItemRepository)
        {
            this.cosmosClientService = cosmosClientService;
            this.cosmosItemRepository = cosmosItemRepository;
        }

        public async Task<T> AddItemAync(CreateItemDto<T> createItenModel)
        {
            Container container = cosmosClientService.GetContainer(createItenModel.DatabaseId, createItenModel.ContainerId);
            (T, HttpStatusCode) result = await cosmosItemRepository.AddItemAsync(container, createItenModel.Item, new PartitionKey(createItenModel.PartitionKey));

            return result.Item1;
        }
    }
}
