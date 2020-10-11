using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Text.Json;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  public class WeatherForecastUtility : IUtility
  {
    public IList<string> Commands => new List<string> { "weather", "weather forecast" };

    public string Name => "Weather forecast";

    private string _openWeatherMapKey;

    public void Configure(IOptions<SecretsModel> options)
    {
      _openWeatherMapKey = options.Value.OpenWeatherMapApiKey;
    }

    public void Run() => Run(null);

    public void Run(string townParm = null)
    {
      if (String.IsNullOrEmpty(_openWeatherMapKey))
      {
        Console.WriteLine("Whoops! API key is not defined.");
        return;
      }

      Console.Write("Enter your town name:");
      string town = (String.IsNullOrEmpty(townParm)) ? Console.ReadLine() : townParm;
      string resp = "";
      WeatherRoot wr = new WeatherRoot();
      try
      {
        using (var wc = GetWebClient())
        {
          resp = wc.DownloadString($"http://api.openweathermap.org/data/2.5/weather?q={town}&appid={_openWeatherMapKey}");
        }
        wr = JsonSerializer.Deserialize<WeatherRoot>(resp);
      }
      catch
      {
        Console.WriteLine("Got no result or couldn't convert to Weather object");
        return;
      }

      Console.WriteLine();
      Console.WriteLine("Temperature: " + Weather.KtoF(wr.Main.Temperature) + "°F or " + Weather.KtoC(wr.Main.Temperature) + "°C. Feels like: " + Weather.KtoF(wr.Main.Temperature) + "°F or " + Weather.KtoC(wr.Main.Temperature) + "°C");
      Console.WriteLine("Wind speed: " + wr.Wind.Speed + " m/s. Air pressure is " + wr.Main.Pressure + "mmHg or " + Math.Round(wr.Main.Pressure * 133.322, 1) + " Pascals.");

      long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
      bool SunWhat = currentTime > wr.Sys.Sunrise;
      long whatNext = SunWhat ? wr.Sys.Sunset : wr.Sys.Sunrise;
      long diff = whatNext - currentTime;
      var dto = DateTimeOffset.FromUnixTimeSeconds(diff);

      if (SunWhat) //If sun should be setting...
      {
        Console.WriteLine("It's day right now. The sun will set in " + dto.ToString("HH:mm:ss"));
      }
      else
      {
        Console.WriteLine("It's night right now. The sun will rise in " + dto.ToString("HH:mm:ss"));
      }
    }

    protected virtual IWebClient GetWebClient()
    {
      var factory = new SystemWebClientFactory();
      return factory.Create();
    }
  }
}