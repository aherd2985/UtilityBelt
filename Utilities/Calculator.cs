using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class Calculator : IUtility
  {
    public IList<string> Commands => new List<string> { "calc", "calculator" };

    public string Name => "Calculator";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      // Ask the user to type the first number.
      Console.WriteLine("Type a number, and then press Enter");
      if (!int.TryParse(Console.ReadLine(), out var num1)) return;

      // Ask the user to type the second number.
      Console.WriteLine("Type another number, and then press Enter");
      if (!int.TryParse(Console.ReadLine(), out var num2)) return;

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
          if (num2 == 0) Console.WriteLine("Cannot divide by 0");
          else Console.WriteLine($"Your result: {num1} / {num2} = " + (num1 / num2));
          break;

        default:
          Console.WriteLine("Invalid operation");
          break;
      }
    }
  }
}
