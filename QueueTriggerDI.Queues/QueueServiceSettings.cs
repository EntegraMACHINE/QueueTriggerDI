namespace QueueTriggerDI.Queues
{
    public class QueueServiceSettings
    {
        public const string ServiceSettings = "QueueServiceSettings";

        public string ConnectionString { get; set; }

        public int DelayInSeconds { get; set; }

        public int MaxRetry { get; set; }
    }
}
