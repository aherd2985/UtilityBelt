using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Net.Mail;
using System.Text;
using UtilityBelt.Helpers;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class TextMessage : IUtility
  {
    private IOptions<SecretsModel> options;

    public IList<string> Commands => new List<string> { "ssms", "sms", "text" };

    public string Name => "Text Message";

    public void Configure(IOptions<SecretsModel> options)
    {
      this.options = options;
    }

    private T MenuEnum<T>(string subject)
              where T : struct, Enum
    {
      Console.WriteLine("");
      Console.WriteLine($"Select the {subject}");

      Array values = Enum.GetValues(typeof(T));
      for (int i = 0; i < values.Length; i++)
      {
        var current = values.GetValue(i) as Enum;
        var meta = EnumHelper.GetAttributeOfType<CarrierMetaAttribute>(current);
        string description = meta?.Name ?? Enum.GetName(typeof(T), current);
        Console.WriteLine($"{i + 1}) {description}");
      }

      Console.WriteLine("");

      Console.Write("Your choice:");
      string optionPicked = Console.ReadLine().ToLower();

      // TODO: think about giving logger instance to utilities if needed
      //logger.LogInformation($"User Choice : {optionPicked}");

      bool isValid = Enum.TryParse(optionPicked, true, out T result);

      if (!isValid || !Enum.IsDefined(typeof(T), result))
      {
        //logger.LogWarning($"Invalid User Choice : {optionPicked}");
        Console.WriteLine("Please select a valid option");
        return MenuEnum<T>(subject);
      }

      return result;
    }

    public void Run()
    {
      Console.WriteLine();
      Console.Write("Input number to receive message:");

      string send2Number = Console.ReadLine().ToLower();

      SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
      {
        Credentials = new NetworkCredential(options.Value.Email, options.Value.EmailPassword),
        EnableSsl = true
      };

      MailMessage message = new MailMessage();
      message.From = new MailAddress(options.Value.Email);

      var carrierType = MenuEnum<CarrierType>("Carrier");
      var meta = EnumHelper.GetAttributeOfType<CarrierMetaAttribute>(carrierType);

      message.To.Add(new MailAddress(send2Number + $"@{meta?.Domain}"));
      message.Subject = "This is my subject";
      message.Body = "This is the content";

      try
      {
        client.Send(message);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Sent successfully");
      }
      catch
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Text Message failed");
      }

      message.Dispose();
      client.Dispose();
    }
  }
}