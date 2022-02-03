using Azure.Data.Tables;
using System;
using System.Collections.Generic;

namespace QueueTriggerDI.Tables.Services
{
    public interface ITableEntityService<T> where T : class, ITableEntity, new ()
    {
        void AddEntity(string tableName, T entity);

        T GetEntity(string tableName, Guid id);

        IList<T> GetEntities(string tableName);

        void DeleteEntity(string tableName, Guid id);
    }
}
