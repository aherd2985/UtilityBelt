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
  internal class Decks : IUtility
  {
    public IList<string> Commands => new List<string> { "decks" };

    public string Name => "Decks of Cards";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;

      Console.WriteLine("Please enter a number of decks to use");
      if (!int.TryParse(Console.ReadLine(), out var num)) return;

      string decksURL = $"https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count={num}";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(decksURL);
      }

      DecksModel deck = JsonSerializer.Deserialize<DecksModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"DeckId -- {deck.Id}");
      Console.WriteLine(@$"Shuffled -- {deck.Shuffled}");
      Console.WriteLine(@$"Remaining -- {deck.Remaining}");

      Console.WriteLine("Pick some cards from the decks");
      if (!int.TryParse(Console.ReadLine(), out var cardNum)) return;

      string decksCardURL = $"https://deckofcardsapi.com/api/deck/{deck.Id}/draw/?count={cardNum}";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(decksCardURL);
      }

      DecksModelCardPick deckCards = JsonSerializer.Deserialize<DecksModelCardPick>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Picked -- {deckCards.Cards.Count}");
      Console.WriteLine(@$"Remaining -- {deckCards.Remaining}");
    }
  }
}