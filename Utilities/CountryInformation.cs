using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Text;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class CountryInformation : IUtility
  {
    public IList<string> Commands => new List<string> { "country" };

    public string Name => "Country Information";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      Console.WriteLine("");
      Console.WriteLine("Please enter a country name:");

      string countryName = Console.ReadLine().ToLower();
      string url = $"https://restcountries.eu/rest/v2/name/{countryName}";

      try
      {
        using (var wc = new WebClient())
        {
          content = wc.DownloadString(url);
        }

        var countryInformationResult = JsonSerializer.Deserialize<List<Models.CountryInformation>>(content);

        foreach (var item in countryInformationResult)
        {
          Console.WriteLine("===============================================");
          Console.WriteLine($"Country Name: {item.Name}");
          Console.WriteLine($"Capital: {item.Capital}");
          Console.WriteLine($"Region: {item.Region}");
          Console.WriteLine($"Population: {item.Population.ToString("N1")}");
          Console.WriteLine($"Area: {item.Area.ToString("N1")} km²");
          Console.WriteLine("Currencies");
          foreach (var moneda in item.Currencies)
          {
            Console.WriteLine($"*Code:\t\t{moneda.Code}");
            Console.WriteLine($"*Name:\t\t{moneda.Name}");
            Console.WriteLine($"*Symbol:\t{moneda.Symbol}");
          }
          Console.WriteLine("Languages");
          foreach (var language in item.Languages)
          {
            Console.WriteLine($"* Name:\t\t{language.Name} / {language.NativeName}");
          }
          Console.WriteLine("===============================================");
        }
      }
      catch (WebException)
      {
        //logger.LogWarning("Country not found");
        Console.WriteLine("Country not found");
      }
    }
  }
}