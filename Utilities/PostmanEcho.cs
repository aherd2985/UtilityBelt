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
  internal class PostmanEcho : IUtility
  {
    public IList<string> Commands => new List<string> { "postman" };

    public string Name => "Postman Echo";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {

      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;

      Console.WriteLine("Please enter a value to echo");
      String userInput = Console.ReadLine();

      string postManEchoUrl = $"https://postman-echo.com/get?irepeat={userInput}";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(postManEchoUrl);
      }

      PostmanModel postmanResponse = JsonSerializer.Deserialize<PostmanModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"{postmanResponse.Arguments.Irepeat}");
      Console.WriteLine(@$"with forward {postmanResponse.HeaderDetail.Forwarded}");
      Console.WriteLine();
    }
  }
}