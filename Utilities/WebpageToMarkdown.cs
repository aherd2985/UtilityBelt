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
  internal class WebpageToMarkdown : IUtility
  {
    public IList<string> Commands => new List<string> { "markdown" };

    public string Name => "Convert a webpage to markdown";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;

      Console.WriteLine("Please enter a webapge to get it in markdown");
      String userInput = Console.ReadLine();

      if (string.IsNullOrEmpty(userInput))
      {
        Console.WriteLine("No page is provided!");
        Console.WriteLine();
        return;
      }

      string web2markdownURL = $"https://urltomarkdown.herokuapp.com/?url={WebUtility.UrlEncode(userInput)}";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(web2markdownURL);
      }

      Console.WriteLine();
      Console.WriteLine(@$"{content}");
      Console.WriteLine();
    }
  }
}