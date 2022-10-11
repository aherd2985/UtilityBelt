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
  internal class SlipAdvice : IUtility
  {
    public IList<string> Commands => new List<string> { "slip" };

    public string Name => "Slip Advice";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content;
      string url = "https://api.adviceslip.com/advice";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(url);
      }

      SlipRoot slipAdvice = JsonSerializer.Deserialize<SlipRoot>(content);

      Console.WriteLine();
      Console.WriteLine(slipAdvice.Slip.Advice);
      Console.WriteLine();
    }
  }
}