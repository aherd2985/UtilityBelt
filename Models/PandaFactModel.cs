using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class PandaFactModel
    {
        [JsonPropertyName("fact")]
        public string Fact { get; set; }
    }
}