using Microsoft.AspNetCore.Mvc;
using QueueTriggerDI.Context.DTO;
using QueueTriggerDI.Context.Services;
using System;

namespace QueueTriggerDI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContextBookController : ControllerBase
    {
        private readonly IBookService bookService;

        public ContextBookController(IBookService bookService)
        {
            this.bookService = bookService;
        }


        [HttpGet]
        [Route("[controller]/get-book-by-id")]
        public IActionResult GetBookById(Guid id)
        {
            BookDto bookDto = bookService.GetBookById(id);

            return Ok(bookDto);
        }

        [HttpPost]
        [Route("[controller]/update-book")]
        public IActionResult UpdateBook(Guid bookId, [FromBody] BookDto newBookData)
        {
            BookDto updateBook = bookService.UpdateBook(bookId, newBookData);

            return Ok(updateBook);
        }
    }
}
