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
  internal class PeopleInSpace : IUtility
  {
    public IList<string> Commands => new List<string> { "who is in space", "space" };

    public string Name => "People In Space";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      string content = string.Empty;
      string spacePeopleUrl = "http://api.open-notify.org/astros.json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(spacePeopleUrl);
      }
      SpacePersonModel spacePeopleFact = JsonSerializer.Deserialize<SpacePersonModel>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("There are " + spacePeopleFact.People.Count + " in space right now!");
      foreach (SpacePerson spacePerson in spacePeopleFact.People)
      {
        Console.WriteLine(spacePerson.Name + " is in " + spacePerson.Craft);
      }
      Console.WriteLine();
    }
  }
}