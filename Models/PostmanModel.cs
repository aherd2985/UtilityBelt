using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class PostmanModel
  {
    [JsonPropertyName("args")]
    public Args Arguments { get; set; }
    [JsonPropertyName("headers")]
    public Headers HeaderDetail { get; set; }
    [JsonPropertyName("url")]
    public string UrlDetail { get; set; }

    public class Args
    {
      [JsonPropertyName("irepeat")]
      public string Irepeat { get; set; }
    }

    public class Headers
    {
      [JsonPropertyName("forwarded")]
      public string Forwarded { get; set; }
    }
  }
}
