using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    class CountryInformation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("area")]
        public decimal Area { get; set; }

        [JsonPropertyName("currencies")]
        public List<Currencies> Currencies { get; set; }

        [JsonPropertyName("languages")]
        public List<Languages> Languages { get; set; }

    }

    class Currencies
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }

    class Languages
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nativeName")]
        public string NativeName { get; set; }
    }

}
