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
  internal class NextMarvelMovie : IUtility
  {
    public IList<string> Commands => new List<string> { };

    public string Name => "Next Marvel movie";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      Console.WriteLine("Here is the next planner Marvel movie");

      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string jokeUrl = $"https://www.whenisthenextmcufilm.com/api";
      NextMarvelMovieModel nextMarvelMovieResult = new NextMarvelMovieModel();

      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(jokeUrl);
      }
      nextMarvelMovieResult = JsonSerializer.Deserialize<NextMarvelMovieModel>(content);

      Console.WriteLine();
      Console.WriteLine(@$"{nextMarvelMovieResult.Title} ");
      Console.WriteLine(@$"{nextMarvelMovieResult.Overview}");
      Console.WriteLine(@$"Release date {nextMarvelMovieResult.ReleaseDate} in {nextMarvelMovieResult.DaysUntil} days");

      Console.WriteLine();
    }
  }
}