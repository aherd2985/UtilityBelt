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
  internal class RandomBank : IUtility
  {
    public IList<string> Commands => new List<string> { "bank" };

    public string Name => "RandomBank";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string bankUrl = "https://random-data-api.com/api/v2/banks";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(bankUrl);
      }

      BankModel randomBank = JsonSerializer.Deserialize<BankModel>(content);
      Console.WriteLine("Here's your bank:");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine($"{randomBank.AccountNumber} - {randomBank.BankName}");
      Console.WriteLine();
    }
  }
}