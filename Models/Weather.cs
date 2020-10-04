using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityBelt
{
    public partial class WeatherRoot
    {
        public Coord coord { get; set; }

        public List<Weather> weather { get; set; }

        public Main main { get; set; }

        public long visibility { get; set; }

        public Wind wind { get; set; }

        public Clouds clouds { get; set; }

        public long dt { get; set; }

        public Sys sys { get; set; }

        public long timezone { get; set; }

        public long id { get; set; }

        public string name { get; set; }

        public long cod { get; set; }
    }

    public partial class Clouds
    {
        public long all { get; set; }
    }

    public partial class Coord
    {
        public double lon { get; set; }

        public double lat { get; set; }
    }

    public partial class Main
    {
        public double temp { get; set; }

        public double feels_like { get; set; }

        public double temp_min { get; set; }

        public double temp_max { get; set; }

        public long pressure { get; set; }

        public long humidity { get; set; }
    }

    public partial class Sys
    {
        public long type { get; set; }

        public long id { get; set; }

        public double message { get; set; }

        public string country { get; set; }

        public long sunrise { get; set; }

        public long sunset { get; set; }
    }

    public partial class Weather
    {
        public long id { get; set; }

        public string main { get; set; }

        public string description { get; set; }

        public string icon { get; set; }

        internal static string KtoC(double temp)
        {
            return Math.Round(temp - 273.15,2).ToString();
        }
        internal static string KtoF(double temp)
        {
            return Math.Round((temp - 273.15) * 9 / (double)5 + 32, 2).ToString();
        }
    }

    public partial class Wind
    {
        public double speed { get; set; }

        public long deg { get; set; }
    }
}
