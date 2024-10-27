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
  internal class MagicTheGathering : IUtility
  {
    public IList<string> Commands => new List<string> { };

    public string Name => "Magic The Gathering";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      Console.Write("Type the card ID, and then press Enter: ");
      string cardId = Console.ReadLine().ToLower();

      if (string.IsNullOrEmpty(cardId))
      {
        Console.WriteLine("No card ID provided!");
        Console.WriteLine();
        return;
      }    

      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string genderizatorUrl = $"https://api.magicthegathering.io/v1/cards/{cardId}";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(genderizatorUrl);
      }

      string magicResult = content;
      Console.WriteLine();
      Console.WriteLine(@$"The result is {magicResult}");

      Console.WriteLine();
    }
  }
}