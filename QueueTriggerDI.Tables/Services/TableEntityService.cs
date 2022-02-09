using Azure.Data.Tables;
using QueueTriggerDI.Tables.Repositories;
using QueueTriggerDI.Utils.Checkers;
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
            Verify.NotEmpty(nameof(tableName), tableName);
            Verify.NotNullOrDefault(nameof(entity), entity);

            entityRepository.AddEntity(tableName, entity);
        }

        public T GetEntity(string tableName, Guid id)
        {
            Verify.NotEmpty(nameof(tableName), tableName);
            Verify.NotEmpty(nameof(id), id);

            (string, string) keys = GuidToSeparatedString(id);

            return entityRepository.GetEntity(tableName, keys.Item1, keys.Item2);
        }

        public IList<T> GetEntities(string tableName)
        {
            Verify.NotEmpty(nameof(tableName), tableName);

            return entityRepository.GetEntities(tableName);
        }

        public void DeleteEntity(string tableName, Guid id)
        {
            Verify.NotEmpty(nameof(tableName), tableName);
            Verify.NotEmpty(nameof(id), id);

            (string, string) keys = GuidToSeparatedString(id);

            entityRepository.DeleteEntity(tableName, keys.Item1, keys.Item2);
        }


        private (string, string) GuidToSeparatedString(Guid id)
        {
            Verify.NotEmpty(nameof(id), id);

            string stringId = id.ToString();
            return (stringId[..(stringId.Length / 2)], stringId[(stringId.Length / 2)..]);
        }
    }
}
