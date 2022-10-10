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
  internal class RandomCommit : IUtility
  {
    public IList<string> Commands => new List<string> { "commit" };

    public string Name => "Random Commit Text";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content;
      string url = "http://whatthecommit.com/index.txt";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(url);
      }

      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(content);
      Console.WriteLine();
    }
  }
}