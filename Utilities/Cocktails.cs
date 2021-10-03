using System;
using System.Collections.Generic;
using System.Composition;
using UtilityBelt.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;
using UtilityBelt.Interfaces;
using System.Web;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  class Cocktails : IUtility
  {
    public IList<string> Commands => new List<string> { "cocktail", "cocktails" };
    public string Name => "Cocktails";

    public void Configure(IOptions<SecretsModel> options)
    {

    }

    public string CocktailSearchMethod(string methodInput)
    {
      string methodUrl = string.Empty;

      switch (methodInput)
      {
        case "1":
        case "search cocktail by name":
        case "search cocktail":
          methodUrl = "https://www.thecocktaildb.com/api/json/v1/1/search.php?s=";
          break;
        case "2":
        case "list all cocktails by first letter":
        case "list all cocktails":
          methodUrl = "https://www.thecocktaildb.com/api/json/v1/1/search.php?f=";
          break;
        case "3":
        case "search ingredient by name":
        case "search ingredient name":
          methodUrl = "https://www.thecocktaildb.com/api/json/v1/1/search.php?i=";
          break;
        case "4":
        case "lookup a random cocktail":
        case "random":
          methodUrl = "https://www.thecocktaildb.com/api/json/v1/1/random.php";
          break;
        case "5":
        case "search by ingredient":
        case "search ingredient":
          methodUrl = "https://www.thecocktaildb.com/api/json/v1/1/filter.php?i=";
          break;
      }

      if(string.IsNullOrEmpty(methodUrl))
      {
        Console.WriteLine("Type what you would category to search for: ");
        Console.WriteLine("1 - Search cocktail by name");
        Console.WriteLine("2 - List all cocktails by first letter");
        Console.WriteLine("3 - Search ingredient by name");
        Console.WriteLine("4 - Lookup a random cocktail");
        Console.WriteLine("5 - Search by ingredient");

        return CocktailSearchMethod(Console.ReadLine().ToLower());
      }
      
      return methodUrl;
    }

    public string searchValue(string userInput)
    {
      if (string.IsNullOrEmpty(userInput))
      {
        Console.WriteLine("No value provided!");
        Console.WriteLine();
        Console.Write("Type value: ");
        searchValue(Console.ReadLine().ToLower());
      }

      return userInput;
    }

    public void Run()
    {
      Console.WriteLine("Type what you would category to search for: ");
      Console.WriteLine("1 - Search cocktail by name");
      Console.WriteLine("2 - List all cocktails by first letter");
      Console.WriteLine("3 - Search ingredient by name");
      Console.WriteLine("4 - Lookup a random cocktail");
      Console.WriteLine("5 - Search by ingredient");

      string searchMethod = CocktailSearchMethod(Console.ReadLine().ToLower());
      string enteredValue = string.Empty;

      if (searchMethod != "https://www.thecocktaildb.com/api/json/v1/1/random.php")
      {
        Console.Write("Type value: ");
        enteredValue = searchValue(Console.ReadLine().ToLower());

        if (string.IsNullOrEmpty(enteredValue))
        {
          Console.WriteLine("No name provided!");
          Console.WriteLine();
          Run();
        }
      }

      string content = string.Empty;
      string cocktailUrl = searchMethod + HttpUtility.UrlEncode(enteredValue);

      switch (searchMethod)
      {
        case "https://www.thecocktaildb.com/api/json/v1/1/search.php?s=":
          try
          {
            using (var wc = GetWebClient())
            {
              content = wc.DownloadString(cocktailUrl);
            }
            CocktailDrinkModel drinkClass = JsonSerializer.Deserialize<CocktailDrinkModel>(content);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;

            drinkClass.drinks.ForEach(Print);
          }
          catch(Exception ex)
          {
            Console.WriteLine("Got no results!");
            return;
          }
          break;

        case "https://www.thecocktaildb.com/api/json/v1/1/random.php":
          try
          {
            using (var wc = GetWebClient())
            {
              content = wc.DownloadString(cocktailUrl);
            }
            CocktailDrinkModel drinkClass = JsonSerializer.Deserialize<CocktailDrinkModel>(content);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;

            drinkClass.drinks.ForEach(Print);
          }
          catch (Exception ex)
          {
            Console.WriteLine("Got no results!");
            return;
          }
          break;

      }


      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine();
    }

    void Print(Drink d)
    {
      Console.WriteLine(@$"{d.strDrink}");
      Console.WriteLine(@$"Category: {d.strCategory}");
      Console.WriteLine(@$"IBA: {d.strIBA}");
      Console.WriteLine(@$"Alcoholic: {d.strAlcoholic}");
      Console.WriteLine(@$"Glass: {d.strGlass}");
      Console.WriteLine("");
      Console.WriteLine("Ingredients:");
      if(!string.IsNullOrEmpty(d.strIngredient1))
        Console.WriteLine(@$"{d.strIngredient1} - {d.strMeasure1}");
      if (!string.IsNullOrEmpty(d.strIngredient2))
        Console.WriteLine(@$"{d.strIngredient2} - {d.strMeasure2}");
      if (!string.IsNullOrEmpty(d.strIngredient3))
        Console.WriteLine(@$"{d.strIngredient3} - {d.strMeasure3}");
      if (!string.IsNullOrEmpty(d.strIngredient4))
        Console.WriteLine(@$"{d.strIngredient4} - {d.strMeasure4}");
      if (!string.IsNullOrEmpty(d.strIngredient5))
        Console.WriteLine(@$"{d.strIngredient5} - {d.strMeasure5}");
      if (!string.IsNullOrEmpty(d.strIngredient6))
        Console.WriteLine(@$"{d.strIngredient6} - {d.strMeasure6}");
      if (!string.IsNullOrEmpty(d.strIngredient7))
        Console.WriteLine(@$"{d.strIngredient7} - {d.strMeasure7}");
      if (!string.IsNullOrEmpty(d.strIngredient8))
        Console.WriteLine(@$"{d.strIngredient8} - {d.strMeasure8}");
      if (!string.IsNullOrEmpty(d.strIngredient9))
        Console.WriteLine(@$"{d.strIngredient9} - {d.strMeasure9}");
      if (!string.IsNullOrEmpty(d.strIngredient10))
        Console.WriteLine(@$"{d.strIngredient10} - {d.strMeasure10}");
      if (!string.IsNullOrEmpty(d.strIngredient11))
        Console.WriteLine(@$"{d.strIngredient11} - {d.strMeasure11}");
      if (!string.IsNullOrEmpty(d.strIngredient12))
        Console.WriteLine(@$"{d.strIngredient12} - {d.strMeasure12}");
      if (!string.IsNullOrEmpty(d.strIngredient13))
        Console.WriteLine(@$"{d.strIngredient13} - {d.strMeasure13}");
      if (!string.IsNullOrEmpty(d.strIngredient14))
        Console.WriteLine(@$"{d.strIngredient14} - {d.strMeasure14}");
      if (!string.IsNullOrEmpty(d.strIngredient15))
        Console.WriteLine(@$"{d.strIngredient15} - {d.strMeasure15}");
      Console.WriteLine("");
      Console.WriteLine("Instructions:");
      Console.WriteLine(@$"{d.strInstructions}");
      Console.WriteLine("");
      Console.WriteLine("");
    }

    protected virtual IWebClient GetWebClient()
    {
      var factory = new SystemWebClientFactory();
      return factory.Create();
    }
  }
}
