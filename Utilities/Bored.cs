using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Net;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class Bored : IUtility
  {
    public IList<string> Commands => new List<string> { "bored" };

    public string Name => "Bored api";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string boredUrl = $"https://www.boredapi.com/api/activity";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(boredUrl);
      }

      BoredModel bored = JsonSerializer.Deserialize<BoredModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Activity -- {bored.Activity}");
      Console.WriteLine(@$"Type -- {bored.Type}");

      Console.WriteLine();
    }
  }
}