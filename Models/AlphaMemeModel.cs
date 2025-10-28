using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class AlphaMemeModel
    {
        [JsonPropertyName("data")]
        public AlphaMemeDataModel Data { get; set; }
    }

    public class AlphaMemeDataModel
    {
      [JsonPropertyName("detail")]
      public string Detail { get; set; }

      [JsonPropertyName("image")]
      public string Images { get; set; }
    }
}
