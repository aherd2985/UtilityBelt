using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class AgifyModel
  {
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("age")]
    public int Age { get; set; }
    [JsonPropertyName("count")]
    public long Count { get; set; }
  }
}
