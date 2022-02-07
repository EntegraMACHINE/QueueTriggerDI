using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using QueueTriggerDI.Cosmos.Models;
using System.Net;
using System.Threading.Tasks;

namespace QueueTriggerDI.Cosmos.Services
{
    public class CosmosDatabaseService : ICosmosDatabaseService
    {
        private readonly CosmosServiceSettings options;

        public CosmosDatabaseService(IOptions<CosmosServiceSettings> options)
        {
            this.options = options.Value;
        }

        public async Task<(Database, HttpStatusCode)> CreateDatabaseIfNotExist(string databaseId)
        {
            DatabaseResponse response = await GetCosmosClient().CreateDatabaseIfNotExistsAsync(databaseId);
            return (response.Database, response.StatusCode);
        }

        public Database GetDatabase(string databaseId)
        {
            return GetCosmosClient().GetDatabase(databaseId);
        }

        public async Task<(Container, HttpStatusCode)> CreateContainerIfNotExist(CreateContainerDto createContainerModel)
        {
            ContainerProperties containerProperties = new ContainerProperties();
            containerProperties.Id = createContainerModel.ContainerId;
            containerProperties.PartitionKeyPath = createContainerModel.ContainerPartitionKeyPath;

            ContainerResponse response = await GetCosmosClient().GetDatabase(createContainerModel.DatabaseId).CreateContainerIfNotExistsAsync(containerProperties);
            return (response.Container, response.StatusCode);
        }

        public Container GetContainer(string databaseId, string containerId)
        {
            return GetDatabase(databaseId).GetContainer(containerId);
        }

        private CosmosClient GetCosmosClient()
        {
            return new CosmosClient(options.AccountEndpoint, options.AccountKey);
        }
    }
}
