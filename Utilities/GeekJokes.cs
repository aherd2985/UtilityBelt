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
  internal class GeekJokes : IUtility
  {
    public IList<string> Commands => new List<string> { "geek" };

    public string Name => "Geek Jokes";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string geekJokeUrl = "https://geek-jokes.sameerkumar.website/api?format=json";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(geekJokeUrl);
      }

      GeekJokeModel joke = JsonSerializer.Deserialize<GeekJokeModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"The Geek says -- {joke.Joke}");

      Console.WriteLine();
    }
  }
}