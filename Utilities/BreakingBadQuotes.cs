using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Net;
using System.Text.Json;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  public class BreakingBadQuotes : IUtility
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
      List<BreakingBadQuoteModel> quote;
      try
      {
        using (var wc = GetWebClient())
        {
          content = wc.DownloadString(breakingBadQuoteUrl);
        }
        quote = JsonSerializer.Deserialize<List<BreakingBadQuoteModel>>(content);
      }
      catch
      {
        Console.WriteLine("Got no result or couldn't convert to a breaking bad quote");
        return;
      }

      Console.WriteLine();
      Console.WriteLine(@$"Quote -- {quote.FirstOrDefault().Quote}");
      Console.WriteLine(@$"From -- {quote.FirstOrDefault().Author}");

      Console.WriteLine();
    }

    protected virtual IWebClient GetWebClient()
    {
      var factory = new SystemWebClientFactory();
      return factory.Create();
    }
  }
}