using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class FBIMostWantedModel
  {
    [JsonPropertyName("items")]
    public List<FBISuspect> Suspects { get; set; }
    
  }
  public class FBISuspect
  {
    [JsonPropertyName("reward_text")]
    public string Reward { get; set; }
    [JsonPropertyName("details")]
    public string Details { get; set; }
    
  }

}
