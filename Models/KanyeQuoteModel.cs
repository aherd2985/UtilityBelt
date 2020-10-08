using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class KanyeQuoteModel
  {
    [JsonPropertyName("quote")]
    public string Quote { get; set; }
  }
}
