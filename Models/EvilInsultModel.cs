using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    class EvilInsultModel
    {
        [JsonPropertyName("insult")]
        public string Insult { get; set; }
    }
}
