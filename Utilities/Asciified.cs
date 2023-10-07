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
  internal class Asciified : IUtility
  {
    public IList<string> Commands => new List<string> { "ascii" };

    public string Name => "Asciified";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;

      Console.WriteLine("Please enter a text su asciify");
      String userInput = Console.ReadLine();

      string asciiUrl = $"https://asciified.thelicato.io/api/v2/ascii?text={userInput}";
      using (var wc = new WebClient())
      {
        wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36");
        content = wc.DownloadString(asciiUrl);
      }

      Console.WriteLine("Here's your asciified text:");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine(content);
      Console.WriteLine();
    }
  }
}