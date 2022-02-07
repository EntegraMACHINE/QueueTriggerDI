namespace QueueTriggerDI.Cosmos.DTO
{
    public class CreateContainerDto
    {
        public string DatabaseId { get; set; }

        public string ContainerId { get; set; }

        public string ContainerPartitionKeyPath { get; set; }
    }
}
