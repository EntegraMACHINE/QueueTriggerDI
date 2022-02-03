using QueueTriggerDI.Context.Entities;
using System;

namespace QueueTriggerDI.Context.Repositories
{
    public interface IBookContentRepository
    {
        BookContent AddBookContent(BookContent bookContent);

        BookContent GetBookContent(Guid bookId);

        BookContent UpdateBookContent(BookContent bookContent);

        void DeleteBookContent(Guid bookId);
    }
}
