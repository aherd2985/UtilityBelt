using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Net;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class DadJoke : IUtility
  {
    public IList<string> Commands => new List<string> { "dadjoke" };

    public string Name => "Dad Joke";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      WebRequest request = WebRequest.Create("https://icanhazdadjoke.com");

      request.Headers.Add("User-Agent", "GitHub library https://github.com/aherd2985/UtilityBelt");
      request.Headers.Add("Accept", "application/json");

      // Get the response.
      WebResponse response = request.GetResponse();

      var status = ((HttpWebResponse)response).StatusCode;

      if (status != HttpStatusCode.OK)
      {
        Console.WriteLine(@"There was an error retrieving the joke. Please try again later.");
      }
      else
      {
        using Stream dataStream = response.GetResponseStream();

        if (dataStream != null)
        {
          StreamReader reader = new StreamReader(dataStream);
          // Read the content.
          string responseFromServer = reader.ReadToEnd();
          // Display the content.
          DadJokeModel dadJoke = JsonSerializer.Deserialize<DadJokeModel>(responseFromServer);
          Console.WriteLine(@"Random Dad Joke:");
          Console.WriteLine(dadJoke.Joke);
        }
        else
        {
          Console.WriteLine(@"The joke data is null. This is probably bad.");
        }
      }

      // Close the response.
      response.Close();
    }
  }
}