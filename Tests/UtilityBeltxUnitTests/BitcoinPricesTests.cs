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
  public class BitCoinPricesTests
  {
    private readonly Mock<IWebClient> webClient = new Mock<IWebClient>();

    public BitCoinPricesTests()
    {
      var mockBitCoinResult = getBitCoinPrice();
      var jsonresult = JsonSerializer.Serialize(mockBitCoinResult);
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonresult);
    }

    [Fact]
    public void BitCoinPricesReturnsPrice()
    {
      var bitCoinClient = new TestableBitcoinPrices(webClient.Object);
      bitCoinClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void BitCoinPricesDoesNotThrowsIfNoResult()
    {
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("");

      var bitCoinClient = new TestableBitcoinPrices(webClient.Object);
      bitCoinClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    private BitcoinPrice getBitCoinPrice()
    {
      var mockBitCoinTime = new BitcoinTime();
      mockBitCoinTime.Updated = "Just now";

      var mockbitCoinBpi = new BitcoinBpi()
      {
        USD = new Currency()
        {
          Rate = "1000"
        }
      };

      var mockBitCoinPrice = new BitcoinPrice
      {
        Bpi = mockbitCoinBpi,
        Time = mockBitCoinTime
      };

      return mockBitCoinPrice;
    }

    private class TestableBitcoinPrices : BitcoinPrices
    {
      public IWebClient webClient { get; set; }

      public TestableBitcoinPrices(IWebClient webClient) : base()
      {
        this.webClient = webClient;
      }

      protected override IWebClient GetWebClient() => this.webClient;
    }
  }
}