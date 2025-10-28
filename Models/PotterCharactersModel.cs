using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class PotterCharactersModel
  {
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    [JsonPropertyName("nickName")]
    public string Nickname { get; set; }
    [JsonPropertyName("hogwartsHouse")]
    public string HogwartsHouse { get; set; }
    [JsonPropertyName("interpretedBy")]
    public string InterpretedBy { get; set; }
  }
}
