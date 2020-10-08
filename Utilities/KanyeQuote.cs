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
  internal class KanyeQuote : IUtility
  {
    public IList<string> Commands => new List<string> { "kanye", "kanye quote" };

    public string Name => "Kanye Quote";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string kanyeQuoteUrl = @"https://api.kanye.rest/";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(kanyeQuoteUrl);
      }
      KanyeQuoteModel kanyeQuote = JsonSerializer.Deserialize<KanyeQuoteModel>(content);
      Console.WriteLine($"\n{kanyeQuote.Quote}");
      Console.WriteLine();
    }

  }
}
