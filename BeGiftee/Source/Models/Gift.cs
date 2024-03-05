
using Newtonsoft.Json;

namespace BeGiftee.Source.Models
{
    public class Gift
    {
        [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }
        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }
        [JsonProperty("labels")]
        public Label[] Labels { get; set; }
        [JsonProperty("archived")]
        public bool Archived { get; set; }
    }
}
