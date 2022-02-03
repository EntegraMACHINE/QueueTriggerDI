using System;

namespace QueueTriggerDI.Tables
{
    public class TableServiceSettings
    {
        public const string SettingsSectionName = "TableServiceSettings";

        public string ConnectionString { get; set; }

        public int DelayInSeconds { get; set; }

        public int MaxRetry { get; set; } 
    }
}
