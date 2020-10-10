using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UtilityBelt.Interfaces;

namespace UtilityBelt.Models
{
  /// <summary>
  /// System web client.
  /// </summary>
  public class SystemWebClient : WebClient, IWebClient
  {
  }

  /// <summary>
  /// System web client factory.
  /// </summary>
  public class SystemWebClientFactory : IWebClientFactory
  {
    #region IWebClientFactory implementation

    public IWebClient Create()
    {
      return new SystemWebClient();
    }

    #endregion IWebClientFactory implementation
  }
}