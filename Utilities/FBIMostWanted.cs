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
  internal class FBIMostWanted : IUtility
  {
    public IList<string> Commands => new List<string> { "FBI", "FBI Top Ten" };
    public string Name => "FBI Top Ten Most Wanted";

    public void Configure(IOptions<SecretsModel> options)
    {

    }

    public void Run()
    {
      string content = string.Empty;
      string fbiUrl = "https://api.fbi.gov/wanted/v1/list";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(fbiUrl);
      }
      FBIMostWantedModel mostWanted = JsonSerializer.Deserialize<FBIMostWantedModel>(content);
      Console.WriteLine();
      FBISuspect topSuspect = mostWanted.Suspects[0];
      Console.WriteLine($"Reward: {topSuspect.Reward}");
      Console.WriteLine($"Details: { topSuspect.Details}");
      Console.WriteLine();
    }      
  }
}
