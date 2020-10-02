using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.IO;
using System.Text;
using UtilityBelt.Models;
using System.Drawing;

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
      Console.WriteLine("3) Random Chuck Norris Joke");
      Console.WriteLine("4) Random Cat Fact");
      Console.WriteLine("5) Bitcoin Prices");
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

        case "3":
        case "random chuck norris joke":
        case "chuck norris joke":
        case "chuck norris":
        case "joke":
          ChuckJokeModel returnObject = new ChuckJokeModel();
          string content = string.Empty;
          string url = "https://api.chucknorris.io/jokes/random";
          WebRequest myReq = WebRequest.Create(url);

          using (WebResponse wr = myReq.GetResponse())
          using (Stream receiveStream = wr.GetResponseStream())
          using (StreamReader sReader = new StreamReader(receiveStream, Encoding.UTF8))
            content = sReader.ReadToEnd();
          ChuckJokeModel chuckJoke = JsonSerializer.Deserialize<ChuckJokeModel>(content);
          Console.WriteLine("");
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine(chuckJoke.value);
          Console.WriteLine("");
          break;

        case "4":
        case "cat fact":
        case "cat":
          ChuckJokeModel returnCat = new ChuckJokeModel();
          string catContent = string.Empty;
          string catUrl = "https://cat-fact.herokuapp.com/facts/random";
          WebRequest catReq = WebRequest.Create(catUrl);

          using (WebResponse wr = catReq.GetResponse())
          using (Stream receiveStream = wr.GetResponseStream())
          using (StreamReader sReader = new StreamReader(receiveStream, Encoding.UTF8))
            content = sReader.ReadToEnd();
          CatFactModel catFact = JsonSerializer.Deserialize<CatFactModel>(content);
          Console.WriteLine("");
          if(catFact.status.verified)
            Console.ForegroundColor = ConsoleColor.Yellow;
          else
            Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine(catFact.text);
          Console.WriteLine("");
          break;

        case "5":
        case "bitcoin prices":
        case "bitcoin":
          ChuckJokeModel returnBitcoin = new ChuckJokeModel();
          string bitContent = string.Empty;
          string bitUrl = "https://api.coindesk.com/v1/bpi/currentprice.json";
          WebRequest bitReq = WebRequest.Create(bitUrl);

          using (WebResponse wr = bitReq.GetResponse())
          using (Stream receiveStream = wr.GetResponseStream())
          using (StreamReader sReader = new StreamReader(receiveStream, Encoding.UTF8))
            content = sReader.ReadToEnd();
          BitcoinPrice bitFact = JsonSerializer.Deserialize<BitcoinPrice>(content);
          Console.WriteLine("");
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("As Of - " + bitFact.time.updated);
          Console.WriteLine("USD - $ " + bitFact.bpi.USD.rate);
          Console.WriteLine("");
          break;

        default:
          Console.WriteLine("Please make a valid option");
          MenuOptions(options);
          break;

      }
    }
  }
}
