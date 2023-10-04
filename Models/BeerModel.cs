using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class BeerModel
  {
    [JsonPropertyName("brand")]
    public string Brand { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("style")]
    public string Style { get; set; }
    [JsonPropertyName("yeast")]
    public string Yeast { get; set; }
    [JsonPropertyName("ibu")]
    public string Ibu { get; set; }
    [JsonPropertyName("alcohol")]
    public string Alcohol { get; set; }
  }

}
