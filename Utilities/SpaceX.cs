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
  internal class SpaceX : IUtility
  {
    public IList<string> Commands => new List<string> { "spacex" };

    public string Name => "SpaceX Data";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content;
      string url = "https://api.spacexdata.com/v5/launches/latest";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(url);
      }

      SpaceXModel spaceXData = JsonSerializer.Deserialize<SpaceXModel>(content);

      Console.WriteLine();
      Console.WriteLine("Latest launch data");
      string launchDetails = (string.IsNullOrEmpty(spaceXData.details)) ? "No details actually" : spaceXData.details;
      Console.WriteLine($"Wiki {spaceXData.links.wikipedia} - {launchDetails}");
      Console.WriteLine();
    }
  }
}