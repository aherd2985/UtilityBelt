using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using UtilityBelt;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;
using UtilityBelt.Utilities;
using Xunit;

namespace UtilityBeltxUnitTests
{
  public class BreakingBadQuotesTests
  {
    private readonly Mock<IWebClient> webClient = new Mock<IWebClient>();

    public BreakingBadQuotesTests()
    {
      var mockQuoteResult = getBreakingBadQuotes();
      var jsonresult = JsonSerializer.Serialize(mockQuoteResult);
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonresult);
    }

    [Fact]
    public void BreakingBadQuotesReturnsPrice()
    {
      var quoteClient = new TestableBreakingBadQuotes(webClient.Object);
      quoteClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void BreakingBadQuotesDoesNotThrowsIfNoResult()
    {
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("");

      var quoteClient = new TestableBreakingBadQuotes(webClient.Object);
      quoteClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    private List<BreakingBadQuoteModel> getBreakingBadQuotes()
    {
      var mockQuoteList = new List<BreakingBadQuoteModel>();

      mockQuoteList.Add(
        new BreakingBadQuoteModel() { Author = "Jessie", Quote = "Frantically drinks water" }
        );

      return mockQuoteList;
    }

    private class TestableBreakingBadQuotes : BreakingBadQuotes
    {
      public IWebClient webClient { get; set; }

      public TestableBreakingBadQuotes(IWebClient webClient) : base()
      {
        this.webClient = webClient;
      }

      protected override IWebClient GetWebClient() => this.webClient;
    }
  }
}