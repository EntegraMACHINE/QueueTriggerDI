using QueueTriggerDI.Context.DTO;
using System;
using System.Collections.Generic;

namespace QueueTriggerDI.Context.Services
{
    public interface IBookService
    {
        BookDto GetBookById(Guid id);

        BookDto AddBook(BookDto book);

        BookDto UpdateBook(Guid bookId, BookDto book);

        bool DeleteBook(Guid id);
    }
}
