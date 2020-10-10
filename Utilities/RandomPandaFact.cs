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
  internal class RandomPandaFact : IUtility
  {
    public IList<string> Commands => new List<string> { "panda", "panda fact" };

    public string Name => "Random Panda Fact";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content;
      string url = "https://some-random-api.ml/facts/panda";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(url);
      }

      PandaFactModel pandaFact = JsonSerializer.Deserialize<PandaFactModel>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(pandaFact.Fact);
      Console.WriteLine();
    }
  }
}