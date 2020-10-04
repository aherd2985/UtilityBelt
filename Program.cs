using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using UtilityBelt.Models;
using UtilityBelt.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

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
            bool showMenu = true;
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

            string testUserInput = null;
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
            Console.WriteLine("");

            Console.Write("Your choice:");
            string optionPicked = Console.ReadLine().ToLower();
            switch (optionPicked)
            {

                case "1":
                case "port":
                case "port scanner":
                    portScanner();
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
            Console.WriteLine("Temperature: " + Weather.KtoF(wr.main.temp) + "°F or " + Weather.KtoC(wr.main.temp) + "°C. Feels like: " + Weather.KtoF(wr.main.temp) + "°F or " + Weather.KtoC(wr.main.temp) + "°C");
            Console.WriteLine("Wind speed: " + wr.wind.speed + " m/s. Air pressure is " + wr.main.pressure + "mmHg or " + Math.Round(wr.main.pressure * 133.322, 1) + " Pascals.");
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            bool SunWhat = currentTime > wr.sys.sunrise;
            long whatNext = SunWhat ? wr.sys.sunset : wr.sys.sunrise;
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
        static void portScanner()
        {
            Console.Write("Please enter a domain:");
            string domain = Console.ReadLine().ToLower();
            Console.Write("Please enter a starting Port Number:");
            int lowPort = int.Parse(Console.ReadLine());
            Console.Write("Please enter an ending Port Number:");
            int highPort = int.Parse(Console.ReadLine());

            PortScanner.Scanner(domain, lowPort, highPort);

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
            Console.WriteLine(chuckJoke.value);
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
            Console.WriteLine("As Of - " + bitFact.time.updated);
            Console.WriteLine("USD - $ " + bitFact.bpi.USD.rate);
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
            if (catFact.status != null && catFact.status.verified)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(catFact.text);
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
            Console.WriteLine("There are " + spacePeopleFact.people.Count + " in space right now!");
            foreach (SpacePerson spacePerson in spacePeopleFact.people)
            {
                Console.WriteLine(spacePerson.name + " is in " + spacePerson.craft);
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
                    Console.WriteLine($"Country Name: {item.name}");
                    Console.WriteLine($"Capital: {item.capital}");
                    Console.WriteLine($"Region: {item.region}");
                    Console.WriteLine($"Population: {item.population}");
                    Console.WriteLine("currencies");
                    foreach (var moneda in item.currencies)
                    {
                        Console.WriteLine($"*Code:\t\t{moneda.code}");
                        Console.WriteLine($"*Name:\t\t{moneda.name}");
                        Console.WriteLine($"*Symbol:\t{moneda.symbol}");
                    }
                    Console.WriteLine("===============================================");
                }
            }
            catch (WebException e)
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

        #endregion

        #region Utility
        internal class WebHookContent
        {
            public string content { get; set; }
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