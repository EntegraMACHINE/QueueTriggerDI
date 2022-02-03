using System;

namespace QueueTriggerDI.Storage
{
    public class BlobServiceSettings
    {
        public const string ServiceSettings = "BlobServiceSettings";

        public string ConnectionString { get; set; }

        public int DelayInSeconds { get; set; }

        public int MaxRetry { get; set; }
    }
}
