using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    class GenderizatorModel
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("probability")]
        public double Probability { get; set; }
    }
}
