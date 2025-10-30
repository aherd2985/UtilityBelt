using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using UtilityBelt.Logging;
using UtilityBelt.Models;

namespace UtilityBelt
{
  internal class Program
  {
    private static ILogger logger;

    [ImportMany]
    private static IEnumerable<IUtility> utilities { get; set; }

    private static Dictionary<string, IUtility> menu;
    private static Dictionary<string, IUtility> commands;

    private static void Compose()
    {
      var configuration = new ContainerConfiguration()
          .WithAssembly(typeof(Program).GetTypeInfo().Assembly);
      using (var container = configuration.CreateContainer())
      {
        utilities = container.GetExports<IUtility>();
      }
    }

    private static void GenerateMenu()
    {
      menu = new Dictionary<string, IUtility>();
      foreach (var item in utilities)
      {
        menu.Add(item.Name.ToLower(), item);
      }
    }

    private static void GenerateCommands()
    {
      commands = new Dictionary<string, IUtility>();
      foreach (var item in utilities)
      {
        foreach (var c in item.Commands)
        {
          commands.Add(c, item);
        }
      }
    }

    private static void Main(string[] args)
    {
      // Logger
      logger = Logger.InitLogger<Program>();

      logger.LogInformation("Starting Application");
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine(Properties.Resources.ASCIIart);
      Console.WriteLine("Loading...");

      IServiceProvider services = ServiceProviderBuilder.GetServiceProvider(args);
      IOptions<SecretsModel> options = services.GetRequiredService<IOptions<SecretsModel>>();

      // load all utilities from the current assembly into Utilities list
      // and generate future menu and command list
      Compose();
      GenerateMenu();
      GenerateCommands();

      bool showMenu = true;
      do
      {
        logger.LogInformation("Load Menu Options");
        MenuOptions(options);
        showMenu = RecursiveOptions();
      } while (showMenu);
    }

    #region Choice Handler

    private static bool RecursiveOptions()
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
          logger.LogWarning("Answer could not be translated to a yes/no");
          Console.WriteLine("");
          Console.WriteLine("I am sorry, your answer could not be translated to a yes/no.");
          Console.WriteLine("Please try to reformat your answer.\n");
          testUserInput = null;
        }
      } while (testUserInput == null);

      try
      {
        logger.LogInformation($"Get user Input : {testUserInput}");
        return FromString(testUserInput);
      }
      catch
      {
        logger.LogWarning($"Answer could not be translated to a yes/no : {testUserInput}");
        Console.WriteLine("");
        Console.WriteLine("I am sorry, your answer could not be translated to a yes/no.");
        Console.WriteLine("Please try to reformat your answer.");
        return RecursiveOptions();
      }
    }

    private static void MenuOptions(IOptions<SecretsModel> options)
    {
      logger.LogInformation("Showing Menu Options");

      var itemNumber = 1;
      foreach (var item in menu.Keys)
      {
        Console.WriteLine($"{itemNumber}) {menu[item].Name}");
        itemNumber++;
      }
      Console.WriteLine("0) Exit");

      Console.WriteLine("");

      Console.Write("Your choice:");
      string optionPicked = Console.ReadLine().ToLower();
      logger.LogInformation($"User Choice : {optionPicked}");

      var isNumberPicked = int.TryParse(optionPicked, out int optionNumber);

      IUtility util = null;
      if (isNumberPicked)
      {
        if (optionNumber == 0)
          Environment.Exit(0);

        if (optionNumber > 0 && optionNumber <= menu.Keys.ToList().Count)
        {
          var key = menu.Keys.ToList()[optionNumber - 1];
          util = menu[key];
        }
      }
      else
      {
        if (menu.ContainsKey(optionPicked))
        {
          util = menu[optionPicked];
        }
        else
        {
          if (commands.ContainsKey(optionPicked))
          {
            util = commands[optionPicked];
          }
        }
      }

      if (util != null)
      {
        logger.LogInformation($"User Choice : {util.Name}");
        util.Configure(options);
        util.Run();
      }
      else
      {
        Console.WriteLine("Please make a valid option");
        MenuOptions(options);
      }
    }

    #endregion Choice Handler

    #region Utility

    private enum BooleanAliases
    {
      YES = 1,
      AYE = 1,
      COOL = 1,
      TRUE = 1,
      Y = 1,
      SI = 1,
      YEAH = 1,
      YEP = 1,
      YUP = 1,
      NAW = 0,
      NO = 0,
      FALSE = 0,
      N = 0,
      NOPE = 0,
    }

    private static bool FromString(string str)
    {
      return Convert.ToBoolean(Enum.Parse(typeof(BooleanAliases), str.ToUpper()));
    }

    #endregion Utility
  }
}