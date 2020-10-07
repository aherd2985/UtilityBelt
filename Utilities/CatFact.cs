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
  internal class CatFact : IUtility
  {
    public IList<string> Commands => new List<string> { "cat", "cat fact" };

    public string Name => "Cat Facts";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string catUrl = "https://cat-fact.herokuapp.com/facts/random";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(catUrl);
      }
      CatFactModel catFact = JsonSerializer.Deserialize<CatFactModel>(content);
      Console.WriteLine();
      if (catFact.Status != null && catFact.Status.Verified)
        Console.ForegroundColor = ConsoleColor.Yellow;
      else
        Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(catFact.Text);
      Console.WriteLine();
    }
  }
}