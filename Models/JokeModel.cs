using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class JokeModel
    {
      [JsonPropertyName("joke")]
      public string Joke { get; set; }
    }
}