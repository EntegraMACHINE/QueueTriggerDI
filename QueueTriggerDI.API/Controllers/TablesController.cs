using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueTriggerDI.Tables.Services;

namespace QueueTriggerDI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService tableService;

        public TablesController(ITableService tableService)
        {
            this.tableService = tableService;
        }

        [HttpPost]
        [Route("[controller]/create-table")]
        public IActionResult CreateTable(string tableName)
        {
            tableService.CreateTable(tableName);

            return Ok();    
        }

        [HttpDelete]
        [Route("[controller]/delete-table")]
        public IActionResult DeleteTable(string tableName)
        {
            tableService.DeleteTable(tableName);

            return Ok();
        }
    }
}
