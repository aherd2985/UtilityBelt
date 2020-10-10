using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityBelt.Interfaces
{
  public interface IWebClient : IDisposable
  {
    // Required methods (subset of `System.Net.WebClient` methods).
    byte[] DownloadData(Uri address);

    byte[] UploadData(Uri address, byte[] data);

    string DownloadString(string address);
  }

  public interface IWebClientFactory
  {
    IWebClient Create();
  }
}