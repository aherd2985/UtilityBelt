using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    class DigitalOceanStatusModel
    {
        [JsonPropertyName("page")]
        public DigitalOceanStatusPageModel Page { get; set; }
        [JsonPropertyName("status")]
        public DigitalOceanStatusStatusModel Status { get; set; }
    }

    class DigitalOceanStatusPageModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("time_zone")]
        public string TimeZone { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime LastUpdate { get; set; }
    }

    class DigitalOceanStatusStatusModel
    {
        [JsonPropertyName("indicator")]
        public string Indicator { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
