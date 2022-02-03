using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QueueTriggerDI.Tables.DTO;
using QueueTriggerDI.Tables.Entities;
using QueueTriggerDI.Tables.Services;
using System;
using System.Collections.Generic;

namespace QueueTriggerDI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ITableEntityService<BookEntity> tableEntityService;
        private readonly IMapper mapper;

        public BooksController(ITableEntityService<BookEntity> tableEntityService, IMapper mapper)
        {
            this.tableEntityService = tableEntityService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]/get-book")]
        public IActionResult GetBook(string tableName, Guid id)
        {
            return Ok(mapper.Map<BookEntityDto>(tableEntityService.GetEntity(tableName, id)));
        }

        [HttpGet]
        [Route("[controller]/get-books")]
        public IActionResult GetBooks(string tableName)
        {
            return Ok(mapper.Map<List<BookEntityDto>>(tableEntityService.GetEntities(tableName)));
        }

        [HttpPost]
        [Route("[controller]/add-book")]
        public IActionResult AddEntity(string tableName, [FromBody] BookEntityDto bookEntityDto)
        {
            tableEntityService.AddEntity(tableName, mapper.Map<BookEntity>(bookEntityDto));

            return Ok();
        }

        [HttpDelete]
        [Route("[controller]/delete-book")]
        public IActionResult DeleteBook(string tableName, Guid id)
        {
            tableEntityService.DeleteEntity(tableName, id);

            return Ok();
        }
    }
}
