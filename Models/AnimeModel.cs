using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class AnimeModel
  {
    [JsonPropertyName("anime")]
    public string Anime { get; set; }
    [JsonPropertyName("character")]
    public string Character { get; set; }
    [JsonPropertyName("quote")]
    public string Quote { get; set; }
  }
}
