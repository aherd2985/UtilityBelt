using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class RandomQuoteModel
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("quote")]
        public RandomQuote Quote { get; set; }
    }

    public class RandomQuote
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("quoteText")]
        public string Text { get; set; }
        [JsonPropertyName("quoteAuthor")]
        public string Author { get; set; }
        [JsonPropertyName("quoteGenre")]
        public string Genre { get; set; }
    }

}
