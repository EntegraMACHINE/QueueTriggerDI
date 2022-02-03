using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace QueueTrigger.CosmosDB.Services
{
    public class CosmosClientService
    {
        private readonly IConfiguration configuration;

        public CosmosClientService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosClient GetCosmosClient()
        {
            return new CosmosClient("");
        }
    }
}
