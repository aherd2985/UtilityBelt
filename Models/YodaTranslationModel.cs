using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class YodaTranslationModel
  {
    [JsonPropertyName("contents")]
    public Contents contents { get; set; }
  }

  public class Contents
  {
    public string translated { get; set; }

    public string text { get; set; }

    public string translation { get; set; }
  }
}
