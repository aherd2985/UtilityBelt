using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.Json;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  public class BitcoinPrices : IUtility
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
      BitcoinPrice bitFact = new BitcoinPrice();
      
      try {
        using (var wc = GetWebClient())
        {
          content = wc.DownloadString(bitUrl);
        }
        bitFact = JsonSerializer.Deserialize<BitcoinPrice>(content);
      }
      catch{
        Console.WriteLine("Got no result or couldn't convert to bitcoin pricing");
      }
      
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(bitFact?.Disclaimer);
      Console.WriteLine("As Of - " + bitFact?.Time?.Updated);
      Console.WriteLine("USD - $ " + bitFact?.Bpi?.USD?.Rate);
      Console.WriteLine();
    }

    protected virtual IWebClient GetWebClient()
    {
      var factory = new SystemWebClientFactory();
      return factory.Create();
    }
  }
}