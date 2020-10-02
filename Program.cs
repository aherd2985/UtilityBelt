using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace UtilityBelt
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Loading the Utility Belt");

      IServiceProvider services = ServiceProviderBuilder.GetServiceProvider(args);
      IOptions<SecretsModel> options = services.GetRequiredService<IOptions<SecretsModel>>();

      MenuOptions(options);


    }

    static void MenuOptions(IOptions<SecretsModel> options)
    {
      Console.WriteLine("");
      Console.WriteLine("Select the Tool");
      Console.WriteLine("1) Port Scanner");
      Console.WriteLine("2) Text Message");
      Console.WriteLine("");

      string optionPicked = Console.ReadLine().ToLower();
      switch (optionPicked)
      {

        case "1":
        case "port":
        case "port scanner":
          Console.WriteLine("Please enter a domain:");
          string domain = Console.ReadLine().ToLower();
          Console.WriteLine("Please enter a starting Port Number:");
          int lowPort = int.Parse(Console.ReadLine());
          Console.WriteLine("Please enter an ending Port Number:");
          int highPort = int.Parse(Console.ReadLine());

          PortScanner.Scanner(domain, lowPort, highPort);
          break;

        case "2":
        case "ssms":
        case "text":
        case "text message":

          Console.WriteLine("");
          Console.WriteLine("Input number to receive message:");

          string send2Number = Console.ReadLine().ToLower();

          SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
          {
            Credentials = new NetworkCredential(options.Value.Email, options.Value.EmailPassword),
            EnableSsl = true
          };          

          MailMessage message = new MailMessage();
          message.From = new MailAddress(options.Value.Email);
          
          // assuming at&t for now
          // todo: add other carriers... fun

          message.To.Add(new MailAddress(send2Number + "@txt.att.net"));
          message.Subject = "This is my subject";
          message.Body = "This is the content";

          client.Send(message);
          Console.WriteLine("Sent");
          Console.ReadLine();
          break;

        default:
          Console.WriteLine("Please make a valid option");
          MenuOptions(options);
          break;

      }
    }
  }
}
