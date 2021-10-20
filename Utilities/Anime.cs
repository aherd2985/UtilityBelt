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
  internal class Anime : IUtility
  {
    public IList<string> Commands => new List<string> { "anime" };

    public string Name => "Anime Quotes";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      
      string agifyURL = $"https://animechan.vercel.app/api/random";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(agifyURL);
      }

      AnimeModel anime = JsonSerializer.Deserialize<AnimeModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Anime -- {anime.Anime}");
      Console.WriteLine(@$"Character -- {anime.Character}");
      Console.WriteLine(@$"Quote -- {anime.Quote}");
    }
  }
}