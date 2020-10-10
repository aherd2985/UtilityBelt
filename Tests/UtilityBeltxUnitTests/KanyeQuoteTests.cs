using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Text.Json;
using UtilityBelt;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;
using UtilityBelt.Utilities;
using Xunit;

namespace UtilityBeltxUnitTests
{
  public class KanyeQuoteTests
  {
    private readonly Mock<IWebClient> webClient = new Mock<IWebClient>();

    public KanyeQuoteTests()
    {
      var mockBitCoinResult = getKanyeQuote();
      var jsonresult = JsonSerializer.Serialize(mockBitCoinResult);
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonresult);
    }

    [Fact]
    public void KanyeQuoteReturnsInsightfulQuote()
    {
      var kanyeClient = new TestableKanyeQuote(webClient.Object);
      kanyeClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void KanyeQuoteDoesNotThrowsIfNoResult()
    {
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("");

      var kanyeClient = new TestableKanyeQuote(webClient.Object);
      kanyeClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    private KanyeQuoteModel getKanyeQuote()
    {
      var mockKanyeQuote = new KanyeQuoteModel
      {
        Quote = "I aint saying shes a golddigger"
      };

      return mockKanyeQuote;
    }

    private class TestableKanyeQuote : KanyeQuote
    {
      public IWebClient webClient { get; set; }

      public TestableKanyeQuote(IWebClient webClient) : base()
      {
        this.webClient = webClient;
      }

      protected override IWebClient GetWebClient() => this.webClient;
    }
  }
}