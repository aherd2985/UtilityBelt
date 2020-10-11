using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using UtilityBelt;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;
using UtilityBelt.Utilities;
using Xunit;

namespace UtilityBeltxUnitTests
{
  public class CatFactsTests
  {
    private readonly Mock<IWebClient> webClient = new Mock<IWebClient>();

    public CatFactsTests()
    {
      var mockCatQuote = getCatFact();
      var jsonresult = JsonSerializer.Serialize(mockCatQuote);
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonresult);
    }

    [Fact]
    public void CatFactsReturnsPrice()
    {
      var catClient = new TestableCatFacts(webClient.Object);
      catClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void CatFactsDoesNotThrowsIfNoResult()
    {
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("");

      var catClient = new TestableCatFacts(webClient.Object);
      catClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    private CatFactModel getCatFact()
    {
      var fileName = Path.Combine("responseSamples", "catFactResponse.json");
      string jsonString = File.ReadAllText(fileName);
 
      return JsonSerializer.Deserialize<CatFactModel>(jsonString);
    }

    private class TestableCatFacts : CatFact
    {
      public IWebClient webClient { get; set; }

      public TestableCatFacts(IWebClient webClient) : base()
      {
        this.webClient = webClient;
      }

      protected override IWebClient GetWebClient() => this.webClient;
    }
  }
}