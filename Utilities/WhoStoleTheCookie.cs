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
  internal class WhoStoleTheCookie : IUtility
  {
    public IList<string> Commands => new List<string> { "cookie" };

    public string Name => "Who Stole The Cookie";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string suspectUrl = "https://randomuser.me/api/?inc=name&format=json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(suspectUrl);
      }
      CookieSuspectModel cookieSuspect = JsonSerializer.Deserialize<CookieSuspectModel>(content);
      string suspectFullName = cookieSuspect.Results[0].Name.First + " " + cookieSuspect.Results[0].Name.Last;
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("");
      Console.WriteLine("Jacques Clouseau: " + suspectFullName + " stole the cookie from the cookie jar.");
      Console.WriteLine("");
      Console.WriteLine(suspectFullName + ": Who, me?");
      Console.WriteLine("");
      Console.WriteLine("Jacques Clouseau: Yes, you!");
      Console.WriteLine("");
      Console.WriteLine(suspectFullName + ": Couldn't be!");
      Console.WriteLine("");
      Console.WriteLine("Jacques Clouseau: Then who?");
      Console.WriteLine();
    }
  }
}