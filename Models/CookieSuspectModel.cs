using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class CookieSuspectModel
  {
    [JsonPropertyName("results")]
    public List<ResultData> Results { get; set; }

    public class ResultData
    {
      [JsonPropertyName("name")]
      public CookieSuspectName Name { get; set; }

      public class CookieSuspectName
      {
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
      }
    }
  }
}
