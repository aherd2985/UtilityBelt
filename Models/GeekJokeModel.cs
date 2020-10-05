using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class GeekJokeModel
    {
        [JsonPropertyName("joke")]
        public string Joke { get; set; }
    }
}
