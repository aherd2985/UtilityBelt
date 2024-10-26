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
  class ElephantFacts : IUtility
  {
    public IList<string> Commands => new List<string> { "elephant", "elephant facts" };

    public string Name => "Elephant facts";

    public void Configure(IOptions<SecretsModel> options)
    {
      //throw new NotImplementedException()
    }

    public void Run()
    {
      string content = string.Empty;

      string elephantFactsApi = @"https://elephant-api.herokuapp.com/elephants";

      using (var wc = new WebClient())
      {
        content = wc.DownloadString(elephantFactsApi);
      }

      List<ElephantModel> elephants = new List<ElephantModel>();
      try
      {
        elephants = JsonSerializer.Deserialize<List<ElephantModel>>(content);
        Random random = new Random();

        int randomNumber = random.Next(0, 46);

        for ( int i = 0; i < 47; i++)
        {
          Console.WriteLine($"{elephants[i].ElephantName}");
        }
        
      }
      catch (Exception e)
      {        
          Console.WriteLine(e);
      }
     
    }
  }
}
