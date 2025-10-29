using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    class DogImageModel
    {
        [JsonPropertyName("message")]
        public string Url { get; set; }
    }
}
