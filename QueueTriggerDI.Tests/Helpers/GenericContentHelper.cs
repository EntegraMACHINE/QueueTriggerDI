using System;

namespace QueueTriggerDI.Tests.Helpers
{
    public class GenericContentHelper
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = "Test Content Name";

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name};";
        }
    }
}
