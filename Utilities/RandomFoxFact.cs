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
  internal class RandomFoxFact : IUtility
  {
    public IList<string> Commands => new List<string> { "fox", "fox fact" };

    public string Name => "Random Fox Fact";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content;
      string url = "https://some-random-api.ml/facts/fox";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(url);
      }

      PandaFactModel foxFact = JsonSerializer.Deserialize<PandaFactModel>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(foxFact.Fact);
      Console.WriteLine();
    }
  }
}