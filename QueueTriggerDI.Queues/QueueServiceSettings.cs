namespace QueueTriggerDI.Queues
{
    public class QueueServiceSettings
    {
        public const string SettingsSectionName = "QueueServiceSettings";

        public string ConnectionString { get; set; }

        public int DelayInSeconds { get; set; }

        public int MaxRetry { get; set; }
    }
}
