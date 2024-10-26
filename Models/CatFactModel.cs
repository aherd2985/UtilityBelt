using System;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class CatFactModel
    {
        [JsonPropertyName("used")]
        public bool Used { get; set; }
        [JsonPropertyName("source")]
        public string Source { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("user")]
        public string User { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("__v")]
        public int Version { get; set; }
        [JsonPropertyName("status")]
        public CatFactStatus Status { get; set; }
    }
    public class CatFactStatus
    {
        [JsonPropertyName("verified")]
        public bool Verified { get; set; }
        [JsonPropertyName("sentCount")]
        public int SentCount { get; set; }
    }
}
