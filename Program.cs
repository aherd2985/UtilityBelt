using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text.Json;
using System.Web;
using UtilityBelt.Helpers;
using UtilityBelt.Models;

namespace UtilityBelt
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine(Properties.Resources.ASCIIart);
      Console.WriteLine("Loading...");

      IServiceProvider services = ServiceProviderBuilder.GetServiceProvider(args);
      IOptions<SecretsModel> options = services.GetRequiredService<IOptions<SecretsModel>>();
            bool showMenu;
            do
      {
        MenuOptions(options);
        showMenu = RecursiveOptions();
      } while (showMenu);

    }

    #region Choice Handler
    static bool RecursiveOptions()
    {
      Console.WriteLine("");
      Console.ForegroundColor = ConsoleColor.Green;

            string testUserInput;
            do
      {
        Console.Write("Would you like to run another option?: ");
        testUserInput = Console.ReadLine();
        if (int.TryParse(testUserInput, out _)) //If user input was a number...
        {
          Console.WriteLine("");
          Console.WriteLine("I am sorry, your answer could not be translated to a yes/no.");
          Console.WriteLine("Please try to reformat your answer.\n");
          testUserInput = null;
        }
      } while (testUserInput == null);

      try
      {
        return FromString(testUserInput);
      }
      catch
      {
        Console.WriteLine("");
        Console.WriteLine("I am sorry, your answer could not be translated to a yes/no.");
        Console.WriteLine("Please try to reformat your answer.");
        return RecursiveOptions();
      }
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
      Console.WriteLine("6) Who is in Space");
      Console.WriteLine("7) Weather forecast");
      Console.WriteLine("8) Country Information");
      Console.WriteLine("9) Discord sender");
      Console.WriteLine("10) Random Quote");
      Console.WriteLine("11) Random Insult");
      Console.WriteLine("12) Who Stole the Cookie");
      Console.WriteLine("13) Random Taco Recipe");
      Console.WriteLine("14) COVID-19 Statistics");
      Console.WriteLine("15) Geek jokes");
      Console.WriteLine("16) Number Fact");
      Console.WriteLine("17) Random Advice");
      Console.WriteLine("18) International Space Station Location");
      Console.WriteLine("19) Random Quote 2");
      Console.WriteLine("20) Console Calculator");
      Console.WriteLine("21) Gender from name");
      Console.WriteLine("22) Random Dad Joke");
      Console.WriteLine("23) Breaking Bad");
      Console.WriteLine("24) Fun Ghost Game");
      Console.WriteLine("25) Dns Hostname to IP Address");
      Console.WriteLine("26) Panda Fact");
      Console.WriteLine("27) Random User Generator");
      Console.WriteLine("");

      Console.Write("Your choice:");
      string optionPicked = Console.ReadLine().ToLower();
      switch (optionPicked)
      {

        case "1":
        case "port":
        case "port scanner":
          PortScanner();
          break;

        case "2":
        case "ssms":
        case "text":
        case "text message":
          TextMessage(options);
          break;
        case "3":
        case "random chuck norris joke":
        case "chuck norris joke":
        case "chuck norris":
        case "joke":
          RandomChuckNorrisJoke();
          break;
        case "4":
        case "cat fact":
        case "cat":
          CatFact();
          break;

        case "5":
        case "bitcoin prices":
        case "bitcoin":
          BitcoinPrices();
          break;

        case "6":
        case "who is in space":
        case "space":
          Space();
          break;

        case "7":
        case "weather":
        case "wf":
        case "weather forecast":
          WeatherForecast(options);
          break;

        case "8":
        case "Country":
          CountryInformation();
          break;

        case "9":
        case "discord":
        case "ds":
        case "webhook":
        case "wh":
          DiscordWebhook(options);
          break;

        case "10":
        case "quote":
          RandomQuote();
          break;

        case "11":
        case "insult":
          RandomInsult();
          break;

        case "12":
        case "cookie":
          CookieAccusation();
          break;

        case "13":
        case "taco":
          RandomTaco();
          break;

        case "14":
        case "covid":
        case "covid19":
        case "covid-19":
          Covid19();
          break;

        case "15":
        case "geek":
          GeekJokes();
          break;

        case "16":
        case "numberfact":
          NumberFact();
          break;

        case "17":
        case "advice":
        case "random advice":
          RandomAdvice();
          break;

        case "18":
        case "space station location":
        case "internation space station":
          SpaceStationLocation();
          break;

        case "19":
        case "quote2":
          RandomQuoteGarden();
          break;

        case "20":
          ConsoleCalculator();
          break;

        case "21":
        case "gender from name":
          GenderFromName();
          break;

        case "22":
        case "dadjoke":
          DadJoke();
          break;

        case "23":
        case "breaking bad":
          BreakingBadQuotes();
          break;

        case "24":
          GhostGame();
          break;

        case "25":
        case "hosttoip":
          HostToIp();
          break;

        case "26":
        case "panda fact":
        case "panda":
          RandomPandaFact();
          break;
        
        case "27":
        case "random user generator":
          RandomUserGenerator();
          break;

        default:
          Console.WriteLine("Please make a valid option");
          MenuOptions(options);
          break;

      }
    }

    #endregion


    #region Choice Processors

    #region Weather
    static void WeatherForecast(IOptions<SecretsModel> options)
    {
      var openWeatherMapApiKey = options.Value.OpenWeatherMapApiKey;

      if (String.IsNullOrEmpty(openWeatherMapApiKey))
      {
        Console.WriteLine("Whoops! API key is not defined.");
        return;
      }

      Console.Write("Enter your town name:");
      string town = Console.ReadLine();

      string resp = "";

      using (var wc = new WebClient())
      {
        resp = wc.DownloadString($"http://api.openweathermap.org/data/2.5/weather?q={town}&appid={openWeatherMapApiKey}");
      }
      WeatherRoot wr = JsonSerializer.Deserialize<WeatherRoot>(resp);

      Console.WriteLine();
      Console.WriteLine("Temperature: " + Weather.KtoF(wr.Main.Temperature) + "°F or " + Weather.KtoC(wr.Main.Temperature) + "°C. Feels like: " + Weather.KtoF(wr.Main.Temperature) + "°F or " + Weather.KtoC(wr.Main.Temperature) + "°C");
      Console.WriteLine("Wind speed: " + wr.Wind.Speed + " m/s. Air pressure is " + wr.Main.Pressure + "mmHg or " + Math.Round(wr.Main.Pressure * 133.322, 1) + " Pascals.");
      long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
      bool SunWhat = currentTime > wr.Sys.Sunrise;
      long whatNext = SunWhat ? wr.Sys.Sunset : wr.Sys.Sunrise;
      long diff = whatNext - currentTime;
      var dto = DateTimeOffset.FromUnixTimeSeconds(diff);
      if (SunWhat) //If sun should be setting...
      {
        Console.WriteLine("It's day right now. The sun will set in " + dto.ToString("HH:mm:ss"));
      }
      else
      {
        Console.WriteLine("It's night right now. The sun will rise in " + dto.ToString("HH:mm:ss"));
      }
    }

    #endregion

    #region Port Scanner
    static void PortScanner()
    {
      Console.Write("Please enter a domain:");
      string domain = Console.ReadLine().ToLower();
      Console.Write("Please enter a starting Port Number:");
      int lowPort = int.Parse(Console.ReadLine());
      Console.Write("Please enter an ending Port Number:");
      int highPort = int.Parse(Console.ReadLine());

            UtilityBelt.PortScanner.Scanner(domain, lowPort, highPort);

    }
    #endregion

    #region Text Message
    static void TextMessage(IOptions<SecretsModel> options)
    {
      Console.WriteLine();
      Console.Write("Input number to receive message:");

      string send2Number = Console.ReadLine().ToLower();

      SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
      {
        Credentials = new NetworkCredential(options.Value.Email, options.Value.EmailPassword),
        EnableSsl = true
      };

            MailMessage message = new MailMessage
            {
                From = new MailAddress(options.Value.Email)
            };

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

    static T MenuEnum<T>(string subject)
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

      bool isValid = Enum.TryParse(optionPicked, true, out T result);

      if (!isValid || !Enum.IsDefined(typeof(T), result))
      {
        Console.WriteLine("Please select a valid option");
        return MenuEnum<T>(subject);
      }

      return result;
    }
    #endregion

    #region Chuck Norris Jokes
    static void RandomChuckNorrisJoke()
    {
      string content = string.Empty;
      string url = "https://api.chucknorris.io/jokes/random";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(url);
      }
      ChuckJokeModel chuckJoke = JsonSerializer.Deserialize<ChuckJokeModel>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(chuckJoke.Value);
      Console.WriteLine();
    }

    #endregion

    #region Bitcoin Prices
    static void BitcoinPrices()
    {
      string content = string.Empty;
      string bitUrl = "https://api.coindesk.com/v1/bpi/currentprice.json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(bitUrl);
      }
      BitcoinPrice bitFact = JsonSerializer.Deserialize<BitcoinPrice>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("As Of - " + bitFact.Time.Updated);
      Console.WriteLine("USD - $ " + bitFact.Bpi.USD.Rate);
      Console.WriteLine();
    }
    #endregion

    #region Cat facts
    static void CatFact()
    {
      string content = string.Empty;
      string catUrl = "https://cat-fact.herokuapp.com/facts/random";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(catUrl);
      }
      CatFactModel catFact = JsonSerializer.Deserialize<CatFactModel>(content);
      Console.WriteLine();
      if (catFact.Status != null && catFact.Status.Verified)
        Console.ForegroundColor = ConsoleColor.Yellow;
      else
        Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(catFact.Text);
      Console.WriteLine();
    }
    #endregion

    #region People in space
    static void Space()
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
    #endregion

    #region Country information
    static void CountryInformation()
    {
      string content = string.Empty;
      Console.WriteLine("");
      Console.WriteLine("Please enter a country name:");

      string countryName = Console.ReadLine().ToLower();
      string url = $"https://restcountries.eu/rest/v2/name/{countryName}";

      try
      {
        using (var wc = new WebClient())
        {
          content = wc.DownloadString(url);
        }

        var countryInformationResult = JsonSerializer.Deserialize<List<CountryInformation>>(content);

        foreach (var item in countryInformationResult)
        {
          Console.WriteLine("===============================================");
          Console.WriteLine($"Country Name: {item.Name}");
          Console.WriteLine($"Capital: {item.Capital}");
          Console.WriteLine($"Region: {item.Region}");
          Console.WriteLine($"Population: {item.Population:N1}");
          Console.WriteLine($"Area: {item.Area:N1} km²");
          Console.WriteLine("Currencies");
          foreach (var moneda in item.Currencies)
          {
            Console.WriteLine($"*Code:\t\t{moneda.Code}");
            Console.WriteLine($"*Name:\t\t{moneda.Name}");
            Console.WriteLine($"*Symbol:\t{moneda.Symbol}");
          }
          Console.WriteLine("Languages");
          foreach (var language in item.Languages)
          {
            Console.WriteLine($"* Name:\t\t{language.Name} / {language.NativeName}");
          }
          Console.WriteLine("===============================================");
        }
      }
      catch (WebException)
      {
        Console.WriteLine("Country not found");
      }
    }
    #endregion

    #region Discord Sender

    static void DiscordWebhook(IOptions<SecretsModel> options)
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
        Content = msg
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
    #endregion

    #region Random quote
    static void RandomQuote()
    {
      string content = string.Empty;
      string quoteUrl = "https://api.forismatic.com/api/1.0/?method=getQuote&lang=en&format=json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(quoteUrl);
      }
      QuoteModel quote = JsonSerializer.Deserialize<QuoteModel>(content);
      Console.WriteLine();
      Console.WriteLine(quote.QuoteText);
      Console.WriteLine($"--{quote.QuoteAuthor}");
      Console.WriteLine();

    }

    #endregion

    #region Random insult
    static void RandomInsult()
    {
      string content = string.Empty;
      string apiUrl = "https://evilinsult.com/generate_insult.php?lang=en&type=json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(apiUrl);
      }
      EvilInsultModel insultResponse = JsonSerializer.Deserialize<EvilInsultModel>(content);
      Console.WriteLine();
      Console.WriteLine(HttpUtility.HtmlDecode(insultResponse.Insult));
      Console.WriteLine();
    }

    #endregion

    #region Who stole the cookie
    static void CookieAccusation()
    {
      string content = string.Empty;
      string suspectUrl = "https://randomuser.me/api/?inc=name&format=json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(suspectUrl);
      }
      CookieSuspectModel cookieSuspect = JsonSerializer.Deserialize<CookieSuspectModel>(content);
      string suspectFullName = cookieSuspect.Results[0].Name.First + " " + cookieSuspect.Results[0].Name.Last;
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("");
      Console.WriteLine("Jacques Clouseau: " + suspectFullName + " stole the cookie from the cookie jar.");
      Console.WriteLine("");
      Console.WriteLine(suspectFullName + ": Who, me?");
      Console.WriteLine("");
      Console.WriteLine("Jacques Clouseau: Yes, you!");
      Console.WriteLine("");
      Console.WriteLine(suspectFullName + ": Couldn't be!");
      Console.WriteLine("");
      Console.WriteLine("Jacques Clouseau: Then who?");
      Console.WriteLine();
    }
    #endregion

    #region Random taco recipe

    static void RandomTaco()
    {
      string content = string.Empty;
      string tacoRandomizerUrl = "http://taco-randomizer.herokuapp.com/random/";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(tacoRandomizerUrl);
      }

      TacoRecipeModel recipe = JsonSerializer.Deserialize<TacoRecipeModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Why not try {recipe.BaseLayer.Name} with {recipe.Mixin.Name},");
      Console.WriteLine(@$"seasoned with {recipe.Seasoning.Name}, topped off with {recipe.Condiment.Name}");
      Console.WriteLine(@$"and wrapped in delicious {recipe.Shell.Name}.");

      Console.WriteLine();
    }

    #endregion

    #region Covid-19

    static void Covid19()
    {
      Console.WriteLine("");
      Console.WriteLine("Enter the country name to get the information. If you want to see the global information, type \"Global\". For the list of all countries type \"List\": ");
      string userInput = Console.ReadLine();
      Console.WriteLine("");

      string content = string.Empty;
      using (var wc = new WebClient())
      {
        content = wc.DownloadString("https://api.covid19api.com/summary");
      }
      CovidRoot summary = JsonSerializer.Deserialize<CovidRoot>(content);

      if (userInput.StartsWith("List", StringComparison.InvariantCultureIgnoreCase))
      {
        userInput = userInput.Substring(4).TrimStart();
        Console.WriteLine("Found countries: ");
        foreach (var countryJson in summary.Countries)
        {
          if (countryJson.Country.StartsWith(userInput, StringComparison.InvariantCultureIgnoreCase))
            Console.WriteLine(countryJson.Country);
        }
        return;
      }

      else if (userInput.Equals("Global", StringComparison.InvariantCultureIgnoreCase))
      {
        ShowCovidInfo("Global", summary.Global.NewConfirmed, summary.Global.TotalConfirmed, summary.Global.NewDeaths, summary.Global.TotalDeaths, summary.Global.NewRecovered, summary.Global.TotalRecovered);
        return;
      }

      else
      {
        foreach (var countryJson in summary.Countries)
        {
          if (userInput.Equals(countryJson.Country, StringComparison.InvariantCultureIgnoreCase))
          {
            ShowCovidInfo(countryJson.Country, countryJson.NewConfirmed, countryJson.TotalConfirmed, countryJson.NewDeaths, countryJson.TotalDeaths, countryJson.NewRecovered, countryJson.TotalRecovered);
            return;
          }
        }
      }

      Console.WriteLine("Country not found");
    }

    static void ShowCovidInfo(string countryName, long newConfirmed, long totalConfirmed, long newDeaths, long totalDeaths, long newRecovered, long totalRecovered)
    {
      Console.WriteLine("Statistics: " + countryName);
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("New Confirmed: " + newConfirmed);
      Console.WriteLine("Total Confirmed: " + totalConfirmed);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("New Deaths: " + newDeaths);
      Console.WriteLine("Total Deaths: " + totalDeaths);
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("New Recovered: " + newRecovered);
      Console.WriteLine("Total Recovered: " + totalRecovered);
    }

    #endregion

    #region GeekJokes
    static void GeekJokes()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string geekJokeUrl = "https://geek-jokes.sameerkumar.website/api?format=json";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(geekJokeUrl);
      }

      GeekJokeModel joke = JsonSerializer.Deserialize<GeekJokeModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"The Geek says -- {joke.Joke}");

      Console.WriteLine();
    }
    #endregion

    #region NumberFact
    static void NumberFact()
    {
      string content = string.Empty;

            Console.WriteLine("Please enter an integer number");
            String userInput = Console.ReadLine();
            if (int.TryParse(userInput, out _))
      {
        string numberFactURL = $"http://numbersapi.com/{userInput}?format=json";
        using (var wc = new WebClient())
        {
          content = wc.DownloadString(numberFactURL);
        }

        Console.WriteLine("Random fact for number " + userInput + ":");
        Console.WriteLine(content);
      }
      else
      {
        Console.WriteLine("Input was not an integer");
      }
    }

    #endregion

    #region Random Advice
    static void RandomAdvice()
    {
      string content = string.Empty;
      string adviceUrl = "https://api.adviceslip.com/advice";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(adviceUrl);
      }

      AdviceModel randomAdvice = JsonSerializer.Deserialize<AdviceModel>(content);
      Console.WriteLine("Here's your advice:");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine(randomAdvice.Slip.Advice);
      Console.WriteLine();
    }
    #endregion

    #region Space Station Location
    static void SpaceStationLocation()
    {
      string content = string.Empty;
      string adviceUrl = "http://api.open-notify.org/iss-now.json";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(adviceUrl);
      }

      SpaceStationLocation spaceStation = JsonSerializer.Deserialize<SpaceStationLocation>(content);
      Console.WriteLine("Woah! The International Space Station is currently at:");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine($@"{spaceStation.Location.Latitude} Latitude and {spaceStation.Location.Longitude} Longitude");
      Console.Write("That's so cool!");
      Console.WriteLine();
    }
    #endregion

    #region Random quote

    static void RandomQuoteGarden()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string randomQuoteUrl = "https://quote-garden.herokuapp.com/api/v2/quotes/random";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(randomQuoteUrl);
      }

      RandomQuoteModel quote = JsonSerializer.Deserialize<RandomQuoteModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Quote -- {quote.Quote.Text}");
      Console.WriteLine(@$"From -- {quote.Quote.Author}");

      Console.WriteLine();
    }

        #endregion

        #region Console Calculator
        static void ConsoleCalculator()
        {

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            // Ask the user to type the first number.
            Console.WriteLine("Type a number, and then press Enter");
            // Declare variables and then initialize to zero.
            int num1 = Convert.ToInt32(Console.ReadLine());

            // Ask the user to type the second number.
            Console.WriteLine("Type another number, and then press Enter");
            int num2 = Convert.ToInt32(Console.ReadLine());

            // Ask the user to choose an option.
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            // Use a switch statement to do the math.
            switch (Console.ReadLine())
            {
                case "a":
                    Console.WriteLine($"Your result: {num1} + {num2} = " + (num1 + num2));
                    break;
                case "s":
                    Console.WriteLine($"Your result: {num1} - {num2} = " + (num1 - num2));
                    break;
                case "m":
                    Console.WriteLine($"Your result: {num1} * {num2} = " + (num1 * num2));
                    break;
                case "d":
                    Console.WriteLine($"Your result: {num1} / {num2} = " + (num1 / num2));
                    break;
            }
            // Wait for the user to respond before closing.
            Console.Write("Press any key to close the Calculator console app...");
            Console.ReadKey();
        }
        #endregion

        #region Gender from name
        static void GenderFromName()
    {
      Console.Write("Type the name, and then press Enter: ");
      string enteredName = Console.ReadLine().ToLower();

      if (string.IsNullOrEmpty(enteredName))
      {
        Console.WriteLine("No name provided!");
        Console.WriteLine();
        return;
      }

      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string genderizatorUrl = $"https://api.genderize.io?name={enteredName}";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(genderizatorUrl);
      }

      GenderizatorModel genderResult = JsonSerializer.Deserialize<GenderizatorModel>(content);
      Console.WriteLine();
      Console.WriteLine(@$"The gender is {genderResult.Gender} with a probability of {genderResult.Probability}");

      Console.WriteLine();
    }
    #endregion

    #region Dad Joke
    static void DadJoke()
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
    #endregion



    #region Random pada fact

    static void RandomPandaFact()
    {
      string content;
      string url = "https://some-random-api.ml/facts/panda";
      using (var wc = new WebClient())
      {
        content = wc.DownloadString(url);
      }

      PandaFactModel pandaFact = JsonSerializer.Deserialize<PandaFactModel>(content);
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(pandaFact.Fact);
      Console.WriteLine();
    }

    #endregion

    #region HostToIp

    private static void HostToIp()
    {
      while (true)
      {
        Console.Write("Please enter a hostname: ");
        var hostname = Console.ReadLine();
        if (string.IsNullOrEmpty(hostname) && Uri.CheckHostName(hostname) != UriHostNameType.Unknown)
        {
          continue;
        }

        Console.WriteLine($"Hostname: {hostname}");
        try
        {
          var ipAddresses = Dns.GetHostAddresses(hostname);
          for (var i = 0; i < ipAddresses.Length; i++)
          {
            Console.WriteLine($"IP[{i + 1}]: {ipAddresses[i]}");
          }

          break;
        }
        catch (SocketException e)
        {
          Console.WriteLine("Invalid hostname - " + e.Message);
          break;
        }
        catch (Exception e)
        {
          Console.WriteLine("An error occurred: " + e.Message);
        }
      }
    }

    #endregion


    #region Ghost Game
    private static void GhostGame()
    {
      Random rNumber = new Random();
      int score = 0;
      int randomNum = rNumber.Next(1, 4);
      int numberInput;
      bool validNumber;

      while (true)
      {
        Console.WriteLine("\nThere are three doors ahead! \n");
        Console.WriteLine("A ghost is behind one of them :O! \n");
        Console.WriteLine("Which door do you open?!?! \n");
        Console.WriteLine("1, 2 or 3?\n");

                bool validDoor;
                do
                {
                    validNumber = int.TryParse(Console.ReadLine(), out numberInput);

                    if (Enumerable.Range(1, 3).Contains(numberInput) && validNumber)//If user selected a valid door ...
                    {
                        validDoor = true;
                    }
                    else
                    {
                        Console.WriteLine("");
                        if (validNumber)
                        {
                            Console.WriteLine("\nThere are only three doors ahead. \n");
                        }
                        else
                        {
                            Console.WriteLine("Input was not an integer.");
                        }
                        Console.WriteLine("Please select door 1, 2, or 3.\n");
                        validDoor = false;
                    }
                } while (validDoor == false);

                if (numberInput == randomNum)
        {
          Console.WriteLine("\nBoo!! GHOST!");
          break;
        }
        else
        {
          Console.WriteLine("\nNo ghost, Phew!");
          Console.WriteLine("\nPress any key to enter the next room!");
          Console.ReadKey();
          randomNum = rNumber.Next(1, 4);
          score += 1;
        }
      }
      Console.WriteLine("Run!!!!!");
      Console.WriteLine("\nGame Over! Score: " + score);
    }
    #endregion

    #region Breaking Bad Quotes
    static void BreakingBadQuotes()
    {
      IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
      defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

      string content = string.Empty;
      string breakingBadQuoteUrl = $"https://breaking-bad-quotes.herokuapp.com/v1/quotes";
      using (var wc = new WebClient() { Proxy = defaultWebProxy })
      {
        content = wc.DownloadString(breakingBadQuoteUrl);
      }

      List<BreakingBadQuoteModel> quote = JsonSerializer.Deserialize<List<BreakingBadQuoteModel>>(content);
      Console.WriteLine();
      Console.WriteLine(@$"Quote -- {quote.FirstOrDefault().Quote}");
      Console.WriteLine(@$"From -- {quote.FirstOrDefault().Author}");

      Console.WriteLine();
    }
    #endregion
    #endregion

    #region Random User Generator
    static void RandomUserGenerator()
    {  
        string content = string.Empty;
        string randomUserUrl = @"https://randomuser.me/api";
        using (var wc = new WebClient())
        {
            content = wc.DownloadString(randomUserUrl);
        }

        RandomUser randomUser = JsonSerializer.Deserialize<RandomUser>(content);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;

        foreach(var user in randomUser.results)
        {
            Console.WriteLine($"Gender: {user.Gender}");
            Console.WriteLine($"Name: {user.Name.First} {user.Name.Last}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Phone: {user.Phone}");
        }

        Console.WriteLine();
    }
    #endregion

    #region Utility
    internal class WebHookContent
    {
      public string Content { get; set; }
    }

    enum BooleanAliases
    {
      YES = 1,
      AYE = 1,
      COOL = 1,
      TRUE = 1,
      Y = 1,
      YEAH = 1,
      NAW = 0,
      NO = 0,
      FALSE = 0,
      N = 0
    }

    static bool FromString(string str)
    {
      return Convert.ToBoolean(Enum.Parse(typeof(BooleanAliases), str.ToUpper()));
    }

    #endregion

  }
}