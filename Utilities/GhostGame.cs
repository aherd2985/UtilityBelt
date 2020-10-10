using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class GhostGame : IUtility
  {
    public IList<string> Commands => new List<string> { };

    public string Name => "Ghost Game";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      Random rNumber = new Random();
      int score = 0;
      int randomNum = rNumber.Next(1, 4);
      int numberInput;
      bool validDoor = false;
      bool validNumber;

      while (true)
      {
        Console.WriteLine("\nThere are three doors ahead! \n");
        Console.WriteLine("A ghost is behind one of them :O! \n");
        Console.WriteLine("Which door do you open?!?! \n");
        Console.WriteLine("1, 2 or 3?\n");

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
  }
}