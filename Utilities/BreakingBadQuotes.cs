using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Net;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class BreakingBadQuotes : IUtility
  {
    public IList<string> Commands => new List<string> { "quote3", "breaking bad" };

    public string Name => "Breaking Bad Quotes";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string breakingBadQuoteUrl = $"https://breaking-bad-quotes.herokuapp.com/v1/quotes";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(breakingBadQuoteUrl);
      }

      List<BreakingBadQuoteModel> quote = JsonSerializer.Deserialize<List<BreakingBadQuoteModel>>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Quote -- {quote.FirstOrDefault().Quote}");
      Console.WriteLine(@$"From -- {quote.FirstOrDefault().Author}");

      Console.WriteLine();
    }
  }
}