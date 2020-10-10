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
  internal class RandomProgrammerQuote : IUtility
  {
    public IList<string> Commands => new List<string> { "quote4" };

    public string Name => "Random Programmer Quote";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string programmerQuoteUrl = $"https://programming-quotes-api.herokuapp.com/quotes/random";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(programmerQuoteUrl);
      }

      ProgrammingQuoteModel programmingQuote = JsonSerializer.Deserialize<ProgrammingQuoteModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"{programmingQuote.Quote}");
      Console.WriteLine(@$"by {programmingQuote.Author}");
      Console.WriteLine();
    }
  }
}