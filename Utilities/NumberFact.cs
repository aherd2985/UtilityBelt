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
  internal class NumberFact : IUtility
  {
    public IList<string> Commands => new List<string> { "numberfact" };

    public string Name => "Number Fact";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      int numberEntered;

      Console.WriteLine("Please enter an integer number");
      String userInput = Console.ReadLine();

      if (int.TryParse(userInput, out numberEntered))
      {
        string numberFactURL = $"http://numbersapi.com/{userInput}?format=json";
        using (var wc = new WebClient())
        {
          content = wc.DownloadString(numberFactURL);
        }

        Console.WriteLine("Random fact for number " + userInput + ":");
        Console.WriteLine(content);
      }
      else
      {
        Console.WriteLine("Input was not an integer");
      }
    }
  }
}