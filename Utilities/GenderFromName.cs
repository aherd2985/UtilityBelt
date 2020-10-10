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
  internal class GenderFromName : IUtility
  {
    public IList<string> Commands => new List<string> { };

    public string Name => "Gender From Name";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      Console.Write("Type the name, and then press Enter: ");
      string enteredName = Console.ReadLine().ToLower();

      if (string.IsNullOrEmpty(enteredName))
      {
        Console.WriteLine("No name provided!");
        Console.WriteLine();
        return;
      }

      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string genderizatorUrl = $"https://api.genderize.io?name={enteredName}";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(genderizatorUrl);
      }

      GenderizatorModel genderResult = JsonSerializer.Deserialize<GenderizatorModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"The gender is {genderResult.Gender} with a probability of {genderResult.Probability}");

      Console.WriteLine();
    }
  }
}