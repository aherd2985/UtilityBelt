using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  class ProgrammingQuoteModel
  {
    [JsonPropertyName("_id")]
    public string Id_ { get; set; }
    [JsonPropertyName("en")]
    public string Quote { get; set; }
    [JsonPropertyName("author")]
    public string Author { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
  }
}
