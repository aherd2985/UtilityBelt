using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class DecksModel
  {
    [JsonPropertyName("deck_id")]
    public string Id { get; set; }
    [JsonPropertyName("shuffled")]
    public bool Shuffled { get; set; }
    [JsonPropertyName("remaining")]
    public int Remaining { get; set; }
  }
}
