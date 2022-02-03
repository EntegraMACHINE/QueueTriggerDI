using Azure.Data.Tables.Models;

namespace QueueTriggerDI.Tables.Services
{
    public interface ITableService
    {
        TableItem CreateTable(string tableName);

        TableItem CreateTableIfNotExist(string tableName);

        void DeleteTable(string tableName);
    }
}
