using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;

namespace QueueTriggerDI.Tables.Services
{
    public class TableClientService : ITableClientService
    {
        private readonly string connectionString;

        public TableClientService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("TableStorageConnectionString");
        }

        public TableClient GetTableClient(string tableName)
        {
            return GetTableServiceClient().GetTableClient(tableName);
        }

        private TableServiceClient GetTableServiceClient()
        {
            return new TableServiceClient(connectionString);
        }
    }
}
