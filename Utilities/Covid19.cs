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
  internal class Covid19 : IUtility
  {
    public IList<string> Commands => new List<string> { "covid", "covid19" };

    public string Name => "COVID-19";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    private static void ShowCovidInfo(string countryName, long newConfirmed, long totalConfirmed, long newDeaths, long totalDeaths, long newRecovered, long totalRecovered)
    {
      Console.WriteLine("Statistics: " + countryName);
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("New Confirmed: " + newConfirmed);
      Console.WriteLine("Total Confirmed: " + totalConfirmed);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("New Deaths: " + newDeaths);
      Console.WriteLine("Total Deaths: " + totalDeaths);
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("New Recovered: " + newRecovered);
      Console.WriteLine("Total Recovered: " + totalRecovered);
    }

    public void Run()
    {
      while (true)
      {
        Console.WriteLine("");
        Console.WriteLine("Enter the country name to get the information. If you want to see the global information, type \"Global\". For the list of all countries type \"List\". To exit Covid-19 Statistics type \"Exit\": ");
        string userInput = Console.ReadLine();
        Console.WriteLine("");

        if (userInput == "Exit")
          return;

        string content = string.Empty;
        using (var wc = new WebClient())
        {
          content = wc.DownloadString("https://api.covid19api.com/summary");
        }
        CovidRoot summary = JsonSerializer.Deserialize<CovidRoot>(content);

        if (userInput.StartsWith("List", StringComparison.InvariantCultureIgnoreCase))
        {
          userInput = userInput.Substring(4).TrimStart();
          Console.WriteLine("Found countries: ");
          foreach (var countryJson in summary.Countries)
          {
            if (countryJson.Country.StartsWith(userInput, StringComparison.InvariantCultureIgnoreCase))
              Console.WriteLine(countryJson.Country);
          }
        }
        else if (userInput.Equals("Global", StringComparison.InvariantCultureIgnoreCase))
        {
          ShowCovidInfo("Global", summary.Global.NewConfirmed, summary.Global.TotalConfirmed, summary.Global.NewDeaths, summary.Global.TotalDeaths, summary.Global.NewRecovered, summary.Global.TotalRecovered);
        }
        else
        {
          bool countryExists = false;

          foreach (var countryJson in summary.Countries)
          {
            if (userInput.Equals(countryJson.Country, StringComparison.InvariantCultureIgnoreCase))
            {
              ShowCovidInfo(countryJson.Country, countryJson.NewConfirmed, countryJson.TotalConfirmed, countryJson.NewDeaths, countryJson.TotalDeaths, countryJson.NewRecovered, countryJson.TotalRecovered);
              countryExists = true;
            }
          }

          if (countryExists == false)
            Console.WriteLine("Country does not exist. Type \"List\" to see the list of available countries.");
        }
      }
    }
  }
}