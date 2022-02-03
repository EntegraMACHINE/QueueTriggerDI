using Microsoft.Azure.Cosmos;
using QueueTriggerDI.Cosmos.Models;
using System.Net;
using System.Threading.Tasks;

namespace QueueTriggerDI.Cosmos.Services
{
    public interface ICosmosDatabaseService
    {
        Task<(Database, HttpStatusCode)> CreateDatabaseIfNotExist(string databaseId);

        Database GetDatabase(string databaseId);

        Task<(Container, HttpStatusCode)> CreateContainerIfNotExist(CreateContainerModel createContainerModel);

        Container GetContainer(string databaseId, string containerId);
    }
}
