using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Text.Json;
using UtilityBelt;
using UtilityBelt.Models;
using UtilityBelt.Utilities;
using Xunit;

namespace UtilityBeltxUnitTests
{
  public class BitCoinPricesTests
  {
    private readonly Mock<WebClient> webClient = new Mock<WebClient>();

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
      public WebClient webClient { get; set; }

      public TestableBitcoinPrices(WebClient webClient) : base()
      {
        this.webClient = webClient;
      }

      protected override WebClient GetWebClient() => this.webClient;
    }
  }
}