namespace QueueTriggerDI.Cosmos
{
    public class CosmosServiceSettings
    {
        public const string SettingsSectionName = "CosmosServiceSettings";

        public string AccountEndpoint { get; set; }

        public string AccountKey { get; set; }
    }
}
