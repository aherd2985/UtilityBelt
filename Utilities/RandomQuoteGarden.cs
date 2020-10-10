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
  internal class RandomQuoteGarden : IUtility
  {
    public IList<string> Commands => new List<string> { "quote2" };

    public string Name => "Random Quote Garden";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string randomQuoteUrl = "https://quote-garden.herokuapp.com/api/v2/quotes/random";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(randomQuoteUrl);
      }

      RandomQuoteModel quote = JsonSerializer.Deserialize<RandomQuoteModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Quote -- {quote.Quote.Text}");
      Console.WriteLine(@$"From -- {quote.Quote.Author}");

      Console.WriteLine();
    }
  }
}