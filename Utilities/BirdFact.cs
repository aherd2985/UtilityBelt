using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Text;
using System.Text.Json;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  public class BirdFact : IUtility
  {
    public IList<string> Commands => new List<string> { "bird", "bird fact" };

    public string Name => "Bird Facts";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string birdUrl = "https://some-random-api.ml/facts/bird";
      BirdFactModel BirdFact = new BirdFactModel();

      try
      {
        using (var wc = GetWebClient())
        {
          content = wc.DownloadString(birdUrl);
        }
        BirdFact = JsonSerializer.Deserialize<BirdFactModel>(content);
      }
      catch
      {
        Console.WriteLine("Got no result or couldn't convert to a bird fact");
        return;
      }

      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;

      Console.WriteLine(BirdFact.Fact);
      Console.WriteLine();
    }

    protected virtual IWebClient GetWebClient()
    {
      var factory = new SystemWebClientFactory();
      return factory.Create();
    }
  }
}