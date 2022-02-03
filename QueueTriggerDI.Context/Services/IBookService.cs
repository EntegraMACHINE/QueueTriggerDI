using QueueTriggerDI.Context.DTO;
using System;
using System.Collections.Generic;

namespace QueueTriggerDI.Context.Services
{
    public interface IBookService
    {
        BookDto GetBook(Guid id);

        IList<BookDto> GetBooks(string author);

        BookDto AddBook(BookDto book);

        BookDto UpdateBook(BookDto book);

        void DeleteBook(Guid id);
    }
}
