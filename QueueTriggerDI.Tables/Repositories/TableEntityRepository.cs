using Azure.Data.Tables;
using QueueTriggerDI.Tables.Services;
using System.Collections.Generic;
using System.Linq;

namespace QueueTriggerDI.Tables.Repositories
{
    public class TableEntityRepository<T> : ITableEntityRepository<T> where T : class, ITableEntity, new()
    {
        private readonly ITableClientService tableClientService;

        public TableEntityRepository(ITableClientService tableClientService)
        {
            this.tableClientService = tableClientService;
        }

        public T GetEntity(string tableName, string partitionKey, string rowKey)
        {
            TableClient tableClient = tableClientService.GetTableClient(tableName);
            
            return tableClient.GetEntity<T>(partitionKey, rowKey);
        }

        public IList<T> GetEntities(string tableName)
        {
            TableClient tableClient = tableClientService.GetTableClient(tableName);

            return tableClient.Query<T>().ToList();
        }

        public void AddEntity(string tableName, T entity)
        {
            TableClient tableClient = tableClientService.GetTableClient(tableName);
            tableClient.AddEntity(entity);
        }

        public void DeleteEntity(string tableName, string partitionKey, string rowKey)
        {
            TableClient tableClient = tableClientService.GetTableClient(tableName);
            tableClient.DeleteEntity(partitionKey, rowKey);
        }
    }
}
