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
  internal class RandomUserGenerator : IUtility
  {
    public IList<string> Commands => new List<string> { };

    public string Name => "Random User Generator";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string randomUserUrl = @"https://randomuser.me/api";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(randomUserUrl);
      }

      RandomUser randomUser = JsonSerializer.Deserialize<RandomUser>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Cyan;

      int cnt = 1;

      foreach (var user in randomUser.results)
      {
        Console.WriteLine($"User Generated Nbr: {cnt}");
        Console.WriteLine($"Gender: {user.Gender}");
        Console.WriteLine($"Name: {user.Name.First} {user.Name.Last}");
        Console.WriteLine($"Email: {user.Email}");
        Console.WriteLine($"Phone: {user.Phone}");
        Console.WriteLine("");
      }

      Console.WriteLine();
    }
  }
}