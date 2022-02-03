using Azure.Data.Tables;
using QueueTriggerDI.Tables.Repositories;
using System;
using System.Collections.Generic;

namespace QueueTriggerDI.Tables.Services
{
    public class TableEntityService<T> : ITableEntityService<T> where T : class, ITableEntity, new()
    {
        private readonly ITableEntityRepository<T> entityRepository;

        public TableEntityService(ITableEntityRepository<T> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public void AddEntity(string tableName, T entity)
        {
            CheckTableName(tableName);

            entityRepository.AddEntity(tableName, entity);
        }

        public T GetEntity(string tableName, Guid id)
        {
            CheckTableName(tableName);

            (string, string) keys = GuidToSeparatedString(id);

            return entityRepository.GetEntity(tableName, keys.Item1, keys.Item2);
        }

        public IList<T> GetEntities(string tableName)
        {
            CheckTableName(tableName);

            return entityRepository.GetEntities(tableName);
        }

        public void DeleteEntity(string tableName, Guid id)
        {
            CheckTableName(tableName);  

            (string, string) keys = GuidToSeparatedString(id);

            entityRepository.DeleteEntity(tableName, keys.Item1, keys.Item2);
        }

        private void CheckTableName(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new Exception("Table name is empty!");
        }

        private (string, string) GuidToSeparatedString(Guid id)
        {
            string stringId = id.ToString();
            return (stringId[..(stringId.Length / 2)], stringId[(stringId.Length / 2)..]);
        }
    }
}
