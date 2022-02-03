using Azure.Core;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace QueueTriggerDI.Tables.Services
{
    public class TableClientService : ITableClientService
    {
        private readonly TableServiceSettings settings;

        public TableClientService(IOptions<TableServiceSettings> settings)
        {
            this.settings = settings.Value;
        }

        public TableClient GetTableClient(string tableName)
        {
            return GetTableServiceClient().GetTableClient(tableName);
        }

        private TableServiceClient GetTableServiceClient()
        {
            TableClientOptions tableClientOptions = new TableClientOptions();
            tableClientOptions.Retry.Mode = RetryMode.Exponential;
            tableClientOptions.Retry.Delay = TimeSpan.FromSeconds(settings.DelayInSeconds);
            tableClientOptions.Retry.MaxRetries = settings.MaxRetry;

            return new TableServiceClient(settings.ConnectionString, tableClientOptions);
        }
    }
}
