using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class SlipRoot
  {
    [JsonPropertyName("slip")]
    public SlipAdviceModel Slip { get; set; }
  }

  public class SlipAdviceModel
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("advice")]
    public string Advice { get; set; }
  }
}
