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
      string magicUrl = $"https://api.magicthegathering.io/v1/cards/{cardId}";
      MagicCardModel magicCard = new MagicCardModel();

      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(magicUrl);
      }
      magicCard = magicCard = JsonSerializer.Deserialize<MagicCardModel>(content);

      Console.WriteLine();
      Console.WriteLine(@$"The card is a {magicCard.Card.Name} of type {magicCard.Card.Type}");

      Console.WriteLine();
    }
  }
}