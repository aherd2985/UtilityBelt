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
  internal class GoTCharacters : IUtility
  {
    public IList<string> Commands => new List<string> { "GoT", "Thrones" };

    public string Name => "GOT Characters";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      int numberEntered;

      Console.WriteLine("Please enter an integer number");
      String userInput = Console.ReadLine();

      GoTCharactersModel goTCharacter = new GoTCharactersModel();

      if (int.TryParse(userInput, out numberEntered))
      {
        try
        {
          string goTApiURL = $"https://thronesapi.com/api/v2/Characters/{userInput}";
          using (var wc = new WebClient())
          {
            content = wc.DownloadString(goTApiURL);
          }

          goTCharacter = JsonSerializer.Deserialize<GoTCharactersModel>(content);

          Console.WriteLine($"Well done you found {goTCharacter?.FullName} - {goTCharacter?.Title}");
        }
        catch
        {
          Console.WriteLine("You don't know nothing Jon Snow");
        }        
      }
      else
      {
        Console.WriteLine("Input was not an integer");
      }
    }
  }
}