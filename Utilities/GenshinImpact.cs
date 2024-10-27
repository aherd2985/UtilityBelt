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
  internal class GenshinImpact : IUtility
  {
    public IList<string> Commands => new List<string> { };

    public string Name => "Genshin Impact";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      Console.Write("Type the entity, and then press Enter: ");
      string entity = Console.ReadLine().ToLower();

      if (string.IsNullOrEmpty(entity))
      {
        Console.WriteLine("No entity provided!");
        Console.WriteLine();
        return;
      }

      Console.Write("Type the optional detail, and then press Enter: ");
      string detail = Console.ReadLine().ToLower();
      if (!string.IsNullOrEmpty(detail)) entity = $"{entity}/{detail}";      

      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string genderizatorUrl = $"https://genshin.jmp.blue/{entity}?lang=en";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(genderizatorUrl);
      }

      string genshinResult = content;
      Console.WriteLine();
      Console.WriteLine(@$"The result is {genshinResult}");

      Console.WriteLine();
    }
  }
}