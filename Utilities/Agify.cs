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
  internal class Agify : IUtility
  {
    public IList<string> Commands => new List<string> { "agify" };

    public string Name => "Agify";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;

      Console.WriteLine("Please enter a name  to guess its age");
      String userInput = Console.ReadLine();

      string agifyURL = $"https://api.agify.io/?name={userInput}";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(agifyURL);
      }

      AgifyModel agify = JsonSerializer.Deserialize<AgifyModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Age -- {agify.Age}");
    }
  }
}