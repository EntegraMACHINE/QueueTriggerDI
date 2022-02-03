using System;

namespace QueueTriggerDI.Context.Entities
{
    public class BookContent
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public string Content { get; set; }
    }
}
