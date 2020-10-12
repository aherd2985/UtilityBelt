using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class BirdFactModel
  {
    [JsonPropertyName("fact")]
    public string Fact { get; set; }
  }
}