using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class ChuckJokeModel
    {
        public List<object> Categories { get; set; }
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }
        public string Id { get; set; }
        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }
    }
}
