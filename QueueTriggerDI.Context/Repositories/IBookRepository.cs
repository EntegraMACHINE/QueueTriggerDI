using QueueTriggerDI.Context.Entities;
using System;
using System.Collections.Generic;

namespace QueueTriggerDI.Context.Repositories
{
    public interface IBookRepository
    {
        Book AddBook(Book book);

        Book GetBook(Guid id);

        Book UpdateBook(Book book);

        bool DeleteBook(Guid id);
    }
}
