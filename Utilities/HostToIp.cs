using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using UtilityBelt.Models;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  internal class HostToIp : IUtility
  {
    public IList<string> Commands => new List<string> { "hosttoip" };

    public string Name => "Host To Ip";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

    public void Run()
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
  }
}