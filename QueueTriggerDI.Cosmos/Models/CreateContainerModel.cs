namespace QueueTriggerDI.Cosmos.Models
{
    public class CreateContainerModel
    {
        public string DatabaseId { get; set; }

        public string ContainerId { get; set; }

        public string ContainerPartitionKeyPath { get; set; }
    }
}
