using System;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class BitcoinPrice
    {
        [JsonPropertyName("time")]
        public BitcoinTime Time { get; set; }

        [JsonPropertyName("disclaimer")]
        public string Disclaimer { get; set; }

        [JsonPropertyName("chartName")]
        public string ChartName { get; set; }

        [JsonPropertyName("bpi")]
        public BitcoinBpi Bpi { get; set; }
    }
    public class BitcoinTime
    {
        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonPropertyName("updatedISO")]
        public DateTime UpdatedISO { get; set; }

        [JsonPropertyName("updateduk")]
        public string Updateduk { get; set; }
    }
    public class BitcoinBpi
    {
        [JsonPropertyName("USD")]
        public Currency USD { get; set; }

        [JsonPropertyName("GBP")]
        public Currency GBP { get; set; }

        [JsonPropertyName("EUR")]
        public Currency EUR { get; set; }
    }
    public class Currency
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("rate")]
        public string Rate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("rate_float")]
        public double RateFloat { get; set; }
    }
}
