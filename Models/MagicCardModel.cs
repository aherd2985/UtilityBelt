using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class MagicCardModel
  {
    [JsonPropertyName("card")]
    public MagicCardModelCard Card { get; set; }
  }
  public class MagicCardModelCard
  {
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
  }
}
