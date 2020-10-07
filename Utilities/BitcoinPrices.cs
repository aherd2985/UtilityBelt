using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Text;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class BitcoinPrices : IUtility
  {
    public IList<string> Commands => new List<string> { "bitcoin", "bitcoin price" };

    public string Name => "Bitcoin Prices";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string bitUrl = "https://api.coindesk.com/v1/bpi/currentprice.json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(bitUrl);
      }
      BitcoinPrice bitFact = JsonSerializer.Deserialize<BitcoinPrice>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("As Of - " + bitFact.Time.Updated);
      Console.WriteLine("USD - $ " + bitFact.Bpi.USD.Rate);
      Console.WriteLine();
    }
  }
}