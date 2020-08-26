using System;
using System.Threading;

namespace UtilityBelt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Loading the Utility Belt");

            MenuOptions();

            
        }

        static void MenuOptions()
        {
            Console.WriteLine("");
            Console.WriteLine("Select the Tool");
            Console.WriteLine("1) Port Scanner");
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
            
                default:
                    Console.WriteLine("Please make a valid option");
                    MenuOptions();
                    break;

            }
        }
    }
}
