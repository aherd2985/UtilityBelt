using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class BirdFactModel
  {
    [JsonPropertyName("fact")]
    public string Fact { get; set; }
  }
}