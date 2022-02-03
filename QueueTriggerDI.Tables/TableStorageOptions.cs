namespace QueueTriggerDI.Tables
{
    public class TableStorageOptions
    {
        public const string TableStorage = "TableStorage";

        public string Endpoint { get; set; } = string.Empty;

        public string AccountName { get; set; } = string.Empty;

        public string AccountKey { get; set; } = string.Empty;
    }
}
