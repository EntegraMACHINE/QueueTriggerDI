using Microsoft.AspNetCore.Mvc;
using QueueTriggerDI.Cosmos.Entities;
using QueueTriggerDI.Cosmos.DTO;
using QueueTriggerDI.Cosmos.Services;
using System.Threading.Tasks;

namespace QueueTriggerDI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CosmosBooksController : ControllerBase
    {
        private readonly ICosmosItemService<BookItem> cosmosItemService;

        public CosmosBooksController(ICosmosItemService<BookItem> cosmosItemService)
        {
            this.cosmosItemService = cosmosItemService;
        }

        [HttpPost]
        [Route("[controller]/add-cosmos-book")]
        public async Task<IActionResult> AddBook([FromBody] CreateItemDto<BookItem> createItemModel)
        {
            BookItem result = await cosmosItemService.AddItemAync(createItemModel);

            return Ok(result);
        }
    }
}
