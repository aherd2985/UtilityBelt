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
  internal class RandomTaco : IUtility
  {
    public IList<string> Commands => new List<string> { "taco" };

    public string Name => "Random Taco";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string tacoRandomizerUrl = "http://taco-randomizer.herokuapp.com/random/";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(tacoRandomizerUrl);
      }

      TacoRecipeModel recipe = JsonSerializer.Deserialize<TacoRecipeModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Why not try {recipe.BaseLayer.Name} with {recipe.Mixin.Name},");
      Console.WriteLine(@$"seasoned with {recipe.Seasoning.Name}, topped off with {recipe.Condiment.Name}");
      Console.WriteLine(@$"and wrapped in delicious {recipe.Shell.Name}.");

      Console.WriteLine();
    }
  }
}