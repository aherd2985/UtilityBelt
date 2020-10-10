using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class RandomQuote : IUtility
  {
    public IList<string> Commands => new List<string> { "quote" };

    public string Name => "Random Quote";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string quoteUrl = "https://api.forismatic.com/api/1.0/?method=getQuote&lang=en&format=json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(quoteUrl);
      }
      QuoteModel quote = JsonSerializer.Deserialize<QuoteModel>(content);
      Console.WriteLine();
      Console.WriteLine(quote.QuoteText);
      Console.WriteLine($"--{quote.QuoteAuthor}");
      Console.WriteLine();
    }
  }
}