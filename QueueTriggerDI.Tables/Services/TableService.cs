using Azure.Data.Tables.Models;

namespace QueueTriggerDI.Tables.Services
{
    public class TableService : ITableService
    {
        private readonly ITableClientService tableClientService;

        public TableService(ITableClientService tableClientService)
        {
            this.tableClientService = tableClientService;
        }

        public TableItem CreateTable(string tableName)
        {
            return tableClientService.GetTableClient(tableName).Create().Value;
        }

        public TableItem CreateTableIfNotExist(string tableName)
        {
            return tableClientService.GetTableClient(tableName).CreateIfNotExists().Value;
        }

        public void DeleteTable(string tableName)
        {
            tableClientService.GetTableClient(tableName).Delete();
        }
    }
}
