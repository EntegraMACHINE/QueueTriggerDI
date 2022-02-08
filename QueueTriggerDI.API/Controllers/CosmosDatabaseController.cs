using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using QueueTriggerDI.Cosmos.DTO;
using QueueTriggerDI.Cosmos.Services;
using System.Net;
using System.Threading.Tasks;

namespace QueueTriggerDI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CosmosDatabaseController : ControllerBase
    {
        private readonly ICosmosDatabaseService cosmosClientService;

        public CosmosDatabaseController(ICosmosDatabaseService cosmosClientService)
        {
            this.cosmosClientService = cosmosClientService;
        }

        [HttpGet]
        [Route("[controller]/get-database")]
        public IActionResult GetDatabase(string databaseId)
        {
            return Ok(cosmosClientService.GetDatabase(databaseId));
        }

        [HttpGet]
        [Route("[controller]/get-container")]
        public IActionResult GetContainer(string databaseId, string containerId)
        {
            return Ok(cosmosClientService.GetContainer(databaseId, containerId));
        }

        [HttpPost]
        [Route("[controller]/create-database-if-not-exist")]
        public async Task<IActionResult> CreateDatabaseIfNotExist(string databaseId)
        {
            (Database, HttpStatusCode) response = await cosmosClientService.CreateDatabaseIfNotExist(databaseId);

            if (response.Item2 == HttpStatusCode.OK)
                return Ok($"Database {databaseId} already exist!");

            return Created(response.Item1.Id, response.Item1);
        }

        [HttpPost]
        [Route("[controller]/create-container-if-not-exist")]
        public async Task<IActionResult> CreateContainerIfNotExist([FromBody] CreateContainerDto createContainerModel)
        {
            (Container, HttpStatusCode) response = await cosmosClientService.CreateContainerIfNotExist(createContainerModel);

            if (response.Item2 == HttpStatusCode.OK)
                return Ok($"Database {createContainerModel.ContainerId} already exist!");

            return Created(response.Item1.Id, response.Item1);
        }
    }
}
