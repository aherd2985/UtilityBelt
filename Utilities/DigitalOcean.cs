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
  internal class DigitalOcean : IUtility
  {
    public IList<string> Commands => new List<string> { "digitalocean" };

    public string Name => "Digital Ocean";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string digitalOceanStatusUrl = $"https://s2k7tnzlhrpw.statuspage.io/api/v2/status.json";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(digitalOceanStatusUrl);
      }

      DigitalOceanStatusModel digitalOceanStatus = JsonSerializer.Deserialize<DigitalOceanStatusModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Indicator -- {digitalOceanStatus.Status.Indicator}");
      Console.WriteLine(@$"Description -- {digitalOceanStatus.Status.Description}");

      Console.WriteLine();
    }
  }
}