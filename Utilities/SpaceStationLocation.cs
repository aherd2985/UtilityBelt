using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class SpaceStationLocation : IUtility
  {
    public IList<string> Commands => new List<string> { "international space station", "iss" };

    public string Name => "Space Station Location";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string adviceUrl = "http://api.open-notify.org/iss-now.json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(adviceUrl);
      }

      Models.SpaceStationLocation spaceStation = JsonSerializer.Deserialize<Models.SpaceStationLocation>(content);
      Console.WriteLine("Woah! The International Space Station is currently at:");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine($"{spaceStation.Location.Latitude} Latitude and {spaceStation.Location.Longitude} Longitude");
      Console.Write("That's so cool!");
      Console.WriteLine();
    }
  }
}