using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class BoredModel
  {
    [JsonPropertyName("activity")]
    public string Activity { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("participants")]
    public int Participants { get; set; }
    [JsonPropertyName("price")]
    public long Price { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("key")]
    public string Key { get; set; }
    [JsonPropertyName("accessibility")]
    public int Accessibility { get; set; }
  }
}
