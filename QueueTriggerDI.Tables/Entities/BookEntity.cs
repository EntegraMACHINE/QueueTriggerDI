using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;
using System;

namespace QueueTriggerDI.Tables.Entities
{
    public class BookEntity : ITableEntity
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }
    }
}
