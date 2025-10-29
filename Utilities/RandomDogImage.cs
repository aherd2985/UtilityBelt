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
  internal class RandomDogImage : IUtility
  {
    public IList<string> Commands => new List<string> { "dog" };

    public string Name => "Random Dog Image";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string quoteUrl = "https://dog.ceo/api/breeds/image/random";

      try
      {
        using var http = new HttpClient();

        var content = http.GetStringAsync(quoteUrl).GetAwaiter().GetResult();

        var dog = JsonSerializer.Deserialize<DogImageModel>(content);
        if (dog != null && !string.IsNullOrEmpty(dog.Url))
        {
          ImageToConsole.Show(dog.Url, 60);
          Console.WriteLine(dog.Url);
        }
        else
        {
          Console.WriteLine("Error: invalid response from API.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error fetching dog image: {ex.Message}");
      }
    }
  }
}