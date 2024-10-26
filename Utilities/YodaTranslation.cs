using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class YodaTranslation : IUtility
  {
    public IList<string> Commands => new List<string> { "yodatranslate", "Yoda Tranlate" };

    public string Name => "Yoda Translation";

    
    void IUtility.Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string url;
      string originalText;
      string translation;
      YodaTranslationModel yodaTranslationModel;
      try
      {
        url = "https://api.funtranslations.com/translate/yoda.json?text=";
        Console.WriteLine("Enter the phrase you want Yoda to translate: ");
        originalText = Console.ReadLine();
        using (var wc = new WebClient())
        {
          translation = wc.DownloadString(url + originalText);
        }

        yodaTranslationModel = JsonSerializer.Deserialize<YodaTranslationModel>(translation);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("In Yoda's words:");
        Console.WriteLine(yodaTranslationModel.contents.translated);
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Yoda Translation Error");
      }



    }
  }
}
