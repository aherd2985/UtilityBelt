using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Text.Json;
using System.Web;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class RandomInsult : IUtility
  {
    public IList<string> Commands => new List<string> { "insult" };

    public string Name => "Random Insult";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string apiUrl = "https://evilinsult.com/generate_insult.php?lang=en&type=json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(apiUrl);
      }
      EvilInsultModel insultResponse = JsonSerializer.Deserialize<EvilInsultModel>(content);
      Console.WriteLine();
      Console.WriteLine(HttpUtility.HtmlDecode(insultResponse.Insult));
      Console.WriteLine();
    }
  }
}