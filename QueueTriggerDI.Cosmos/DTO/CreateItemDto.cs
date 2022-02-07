using QueueTriggerDI.Cosmos.Entities;

namespace QueueTriggerDI.Cosmos.DTO
{
    public class CreateItemDto<T> where T : class, ICosmosItem
    {
        public string DatabaseId { get; set; }

        public string ContainerId { get; set; }

        public string PartitionKey { get; set; }

        public T Item { get; set; }
    }
}
