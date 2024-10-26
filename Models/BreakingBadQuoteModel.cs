using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class BreakingBadQuoteModel
    {
        [JsonPropertyName("quote")]
        public string Quote { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; set; }
    }
}
