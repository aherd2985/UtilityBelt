using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.Json;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  public class CountryInformation : IUtility
  {
    public IList<string> Commands => new List<string> { "country" };

    public string Name => "Country Information";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run() => Run(null);

    public void Run(string paramCountryName = null)
    {
      string content = string.Empty;
      Console.WriteLine("");
      Console.WriteLine("Please enter a country name:");

      string countryName = (String.IsNullOrEmpty(paramCountryName)) ? Console.ReadLine() : paramCountryName;
      string url = $"https://restcountries.eu/rest/v2/name/{countryName}";
      List<CountryInformationModel> countryInformationResult = new List<CountryInformationModel>();
      try
      {
        using (var wc = GetWebClient())
        {
          content = wc.DownloadString(url);
        }

        countryInformationResult = JsonSerializer.Deserialize<List<Models.CountryInformationModel>>(content);
      }
      catch
      {
        Console.WriteLine("Got no response or country not found");
        return;
      }

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

    protected virtual IWebClient GetWebClient()
    {
      var factory = new SystemWebClientFactory();
      return factory.Create();
    }
  }
}