using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using UtilityBelt.Helpers;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class UUIDGenerator : IUtility
  {
    public IList<string> Commands => new List<string> { "UUID" };

    public string Name => "UUID Generator";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string quoteUrl = "https://www.uuidtools.com/api/generate/v1";

      try
      {
        using var http = new HttpClient();

        var content = http.GetStringAsync(quoteUrl).GetAwaiter().GetResult();

        if (!string.IsNullOrEmpty(content))
        {
          Console.WriteLine(content);
        }
        else
        {
          Console.WriteLine("Error: invalid response from API.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error fetching UUID: {ex.Message}");
      }
    }
  }
}