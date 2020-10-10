using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Text;
using System.Text.Json;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  public class KanyeQuote : IUtility
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
      KanyeQuoteModel kanyeQuote = new KanyeQuoteModel();
      
      try
      {
        using (var wc = GetWebClient())
        {
          content = wc.DownloadString(kanyeQuoteUrl);
        }
        kanyeQuote = kanyeQuote = JsonSerializer.Deserialize<KanyeQuoteModel>(content);
      }
      catch
      {
        Console.WriteLine("Got no result or couldn't convert to an insightful Yeezy meme");
      }
      Console.WriteLine($"\n{kanyeQuote?.Quote}");
      Console.WriteLine();
    }

    protected virtual IWebClient GetWebClient()
    {
      var factory = new SystemWebClientFactory();
      return factory.Create();
    }
  }
}
