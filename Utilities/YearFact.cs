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
  internal class YearFact : IUtility
  {
    public IList<string> Commands => new List<string> { "yearfact" };

    public string Name => "Year Fact";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;

      string randomYearFactURL = $"http://numbersapi.com/random/year?format=json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(randomYearFactURL);
      }

      Console.WriteLine(content);

    }
  }
}