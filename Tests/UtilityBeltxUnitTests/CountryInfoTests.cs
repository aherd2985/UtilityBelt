using Moq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;
using UtilityBelt.Utilities;
using Xunit;

namespace UtilityBeltxUnitTests
{
  public class CountryInfoTests
  {
    private readonly Mock<IWebClient> webClient = new Mock<IWebClient>();

    public CountryInfoTests()
    {
      var mockCountryInfo = getCountryInfo();
      var jsonresult = JsonSerializer.Serialize(mockCountryInfo);
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonresult);
    }

    [Fact]
    public void CountryInfoReturnsCountryInfo()
    {
      var countryInfoClient = new TestableCountryInfo(webClient.Object);
      countryInfoClient.Run("Bolivia");
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void CountryInfoDoesNotThrowsIfNoResult()
    {
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("");

      var countryInfoClient = new TestableCountryInfo(webClient.Object);
      countryInfoClient.Run("Bolivia");
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    private List<CountryInformationModel> getCountryInfo()
    {
      var fileName = Path.Combine("responseSamples", "countryInfoResponse.json");
      string jsonString = File.ReadAllText(fileName);
      var returnList = new List<CountryInformationModel>();
      returnList.Add(JsonSerializer.Deserialize<CountryInformationModel>(jsonString));
      return returnList;
    }

    private class TestableCountryInfo : CountryInformation
    {
      public IWebClient webClient { get; set; }

      public TestableCountryInfo(IWebClient webClient) : base()
      {
        this.webClient = webClient;
      }

      protected override IWebClient GetWebClient() => this.webClient;
    }
  }
}