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
  internal class PotterCharacters : IUtility
  {
    public IList<string> Commands => new List<string> { "potter" };

    public string Name => "Harry Potter - Characters search";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;

      Console.WriteLine("Please enter a name a character name to search");
      String userInput = Console.ReadLine();

      if (string.IsNullOrEmpty(userInput))
      {
        Console.WriteLine("No character is provided!");
        Console.WriteLine();
        return;
      }

      string potterURL = $"https://potterapi-fedeperin.vercel.app/es/characters?search={userInput}";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(potterURL);
      }

      PotterCharactersModel[] potter = JsonSerializer.Deserialize<PotterCharactersModel[]>(content);
      Console.WriteLine();
      foreach (var character in potter)
      {
        Console.WriteLine(@$"Name -- {character.FullName}");
        Console.WriteLine(@$"Nickname -- {character.Nickname}");
        Console.WriteLine(@$"Hogwarts House -- {character.HogwartsHouse}");
        Console.WriteLine();
      }
    }
  }
}