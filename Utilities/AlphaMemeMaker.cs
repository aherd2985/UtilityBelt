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
  internal class AlphaMemeMaker : IUtility
  {
    public IList<string> Commands => new List<string> { "meme" };

    public string Name => "AlphaMeme";

    public void Configure(IOptions<SecretsModel> options) { }  

    public void Run()
    {
      string content = string.Empty;

      Console.WriteLine("Please enter a number for the meme");
      if (!int.TryParse(Console.ReadLine(), out var num)) return;

      string memeURL = $"http://alpha-meme-maker.herokuapp.com/memes/{num}";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(memeURL);
      }

      AlphaMemeModel meme = JsonSerializer.Deserialize<AlphaMemeModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Detail -- {meme.Data.Detail}");
      Console.WriteLine(@$"Image -- {meme.Data.Images}");
    }
  }
}