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
  internal class Joke : IUtility
  {
    public IList<string> Commands => new List<string> { };

    public string Name => "Joke";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      Console.Write("Type the optional joke search string, and then press Enter: ");
      string jokeString = Console.ReadLine().ToLower();

      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string jokeUrl = $"https://v2.jokeapi.dev/joke/Programming?blacklistFlags=nsfw,religious,political,racist,sexist,explicit&type=single";
      if (!string.IsNullOrEmpty(jokeString)) jokeUrl += $"&contains={jokeString}";
      JokeModel jokeResult = new JokeModel();

      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(jokeUrl);
      }
      jokeResult = JsonSerializer.Deserialize<JokeModel>(content);

      Console.WriteLine();
      Console.WriteLine(@$"{jokeResult.Joke}");

      Console.WriteLine();
    }
  }
}