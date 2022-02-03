using Azure.Data.Tables;

namespace QueueTriggerDI.Tables.Services
{
    public interface ITableClientService
    {
        TableClient GetTableClient(string tableName);
    }
}
