using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UtilityBelt
{
    public partial class WeatherRoot
    {
        [JsonPropertyName("coord")]
        public Coordinates Coord { get; set; }
        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }
        [JsonPropertyName("main")]
        public Main Main { get; set; }
        [JsonPropertyName("visibility")]
        public long Visibility { get; set; }
        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }
        [JsonPropertyName("clouds")]
        public Clouds Clouds { get; set; }
        [JsonPropertyName("dt")]
        public long Dt { get; set; }
        [JsonPropertyName("sys")]
        public Sys Sys { get; set; }
        [JsonPropertyName("timezone")]
        public long Timezone { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("cod")]
        public long Cod { get; set; }
    }

    public partial class Clouds
    {
        [JsonPropertyName("All")]
        public long All { get; set; }
    }

    public partial class Coordinates
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
    }

    public partial class Main
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }
        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }
        [JsonPropertyName("temp_min")]
        public double TempMin { get; set; }
        [JsonPropertyName("temp_max")]
        public double TempMax { get; set; }
        [JsonPropertyName("pressure")]
        public long Pressure { get; set; }
        [JsonPropertyName("humidity")]
        public long Humidity { get; set; }
    }

    public partial class Sys
    {
        [JsonPropertyName("type")]
        public long Type { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("message")]
        public double Message { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("sunrise")]
        public long Sunrise { get; set; }
        [JsonPropertyName("sunset")]
        public long Sunset { get; set; }
    }

    public partial class Weather
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("main")]
        public string Main { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        internal static string KtoC(double temp)
        {
            return Math.Round(temp - 273.15, 2).ToString();
        }
        internal static string KtoF(double temp)
        {
            return Math.Round((temp - 273.15) * 9 / (double)5 + 32, 2).ToString();
        }
    }

    public partial class Wind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
        [JsonPropertyName("deg")]
        public long Degrees { get; set; }
    }
}
