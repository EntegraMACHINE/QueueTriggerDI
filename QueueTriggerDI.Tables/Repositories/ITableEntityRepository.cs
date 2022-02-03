using Azure.Data.Tables;
using System.Collections.Generic;

namespace QueueTriggerDI.Tables.Repositories
{
    public interface ITableEntityRepository<T> where T : class, ITableEntity, new()
    {
        T GetEntity(string tableName, string partitionKey, string rowKey);

        IList<T> GetEntities(string tableName);

        void AddEntity(string tableName, T entity);

        void DeleteEntity(string tableName, string partitionKey, string rowKey);
    }
}
