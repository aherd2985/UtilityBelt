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
  internal class RandomBeer : IUtility
  {
    public IList<string> Commands => new List<string> { "beer" };

    public string Name => "RandomBeer";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string beerUrl = "https://random-data-api.com/api/beer/random_beer";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(beerUrl);
      }

      BeerModel randomBeer = JsonSerializer.Deserialize<BeerModel>(content);
      Console.WriteLine("Here's your beer dude:");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine($"{randomBeer.Name} - {randomBeer.Alcohol}");
      Console.WriteLine();
    }
  }
}