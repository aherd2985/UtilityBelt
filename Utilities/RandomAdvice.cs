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
  internal class RandomAdvice : IUtility
  {
    public IList<string> Commands => new List<string> { "advice" };

    public string Name => "RandomAdvice";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string adviceUrl = "https://api.adviceslip.com/advice";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(adviceUrl);
      }

      AdviceModel randomAdvice = JsonSerializer.Deserialize<AdviceModel>(content);
      Console.WriteLine("Here's your advice:");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine(randomAdvice.Slip.Advice);
      Console.WriteLine();
    }
  }
}