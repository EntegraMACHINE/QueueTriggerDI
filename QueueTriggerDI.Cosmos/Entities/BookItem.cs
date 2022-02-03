using Newtonsoft.Json;

namespace QueueTriggerDI.Cosmos.Entities
{
    public class BookItem : ICosmosItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }
    }
}
