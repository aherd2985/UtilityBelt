using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    class QuoteModel
    {
        [JsonPropertyName("quoteText")]
        public string QuoteText { get; set; }
        [JsonPropertyName("quoteAuthor")]
        public string QuoteAuthor { get; set; }
        [JsonPropertyName("senderName")]
        public string SenderName { get; set; }
        [JsonPropertyName("senderLink")]
        public string SenderLink { get; set; }
        [JsonPropertyName("quoteLink")]
        public string QuoteLink { get; set; }
    }
}
