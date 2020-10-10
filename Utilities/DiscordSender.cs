using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  internal class WebHookContent
  {
    public string content { get; set; }
  }

  [Export(typeof(IUtility))]
  internal class DiscordSender : IUtility
  {
    private IOptions<SecretsModel> options;

    public IList<string> Commands => new List<string> { "discord", "ds", "webhook", "ws" };

    public string Name => "Discord Sender";

    public void Configure(IOptions<SecretsModel> options)
    {
      this.options = options;
    }

    public void Run()
    {
      string whook = options.Value.DiscordWebhook;

      if (String.IsNullOrEmpty(whook))
      {
        Console.WriteLine("Whoops! You dont have a webhook defined in your config!"); return;
      }

      Console.Write("Enter the message:");
      string msg = Console.ReadLine();

      WebHookContent cont = new WebHookContent()
      {
        content = msg
      };
      string json = JsonSerializer.Serialize(cont);

      using (var www = new HttpClient())
      {
        var content = new StringContent(json);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var task = www.PostAsync(whook, content);
        task.Wait();
      }
      Console.WriteLine("Message sent!");
    }
  }
}