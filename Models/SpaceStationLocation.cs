using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class SpaceStationLocation
    {
        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("iss_position")]
        public ISSCoordinates Location { get; set; }
    }

    public class ISSCoordinates
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }
}
