using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class PortScanner : IUtility
  {
    public IList<string> Commands => new List<string> { "port", "port scanner" };

    public string Name => "Port Scanner";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
    {
      Console.Write("Please enter a domain:");
      string domain = Console.ReadLine().ToLower();
      Console.Write("Please enter a starting Port Number:");
      int lowPort = int.Parse(Console.ReadLine());
      Console.Write("Please enter an ending Port Number:");
      int highPort = int.Parse(Console.ReadLine());

      PortScannerInternal.Scanner(domain, lowPort, highPort);
    }
  }
}