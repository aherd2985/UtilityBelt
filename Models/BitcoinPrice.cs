using System;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class BitcoinPrice
    {
        public BitcoinTime Time { get; set; }
        public string Disclaimer { get; set; }
        public string ChartName { get; set; }
        public BitcoinBpi Bpi { get; set; }
    }
    public class BitcoinTime
    {
        public string Updated { get; set; }
        public DateTime UpdatedISO { get; set; }
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
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("rate_float")]
        public double RateFloat { get; set; }
    }
}
