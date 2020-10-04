using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    class SpacePersonModel
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("people")]
        public List<SpacePerson> People { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
    public class SpacePerson
    {
        [JsonPropertyName("craft")]
        public string Craft { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
