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
  internal class RandomChuckNorrisJoke : IUtility
  {
    public IList<string> Commands => new List<string> { "chuck norris joke", "chuck norris", "joke" };

    public string Name => "Random Chuck Norris Joke";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string url = "https://api.chucknorris.io/jokes/random";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(url);
      }
      ChuckJokeModel chuckJoke = JsonSerializer.Deserialize<ChuckJokeModel>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(chuckJoke.Value);
      Console.WriteLine();
    }
  }
}