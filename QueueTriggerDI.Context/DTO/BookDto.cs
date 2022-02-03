using System;

namespace QueueTriggerDI.Context.DTO
{
    public class BookDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }
    }
}
