using Moq;
using System.IO;
using System.Text.Json;
using UtilityBelt.Interfaces;
using UtilityBelt.Models;
using UtilityBelt.Utilities;
using Xunit;

namespace UtilityBeltxUnitTests
{
  public class BirdFactTests
  {
    private readonly Mock<IWebClient> webClient = new Mock<IWebClient>();

    public BirdFactTests()
    {
      var mockBirdQuote = getBirdFact();
      var jsonresult = JsonSerializer.Serialize(mockBirdQuote);
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonresult);
    }

    [Fact]
    public void BirdFactReturnsBirdFacts()
    {
      var birdClient = new TestableBirdFact(webClient.Object);
      birdClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void BirdFactDoesNotThrowsIfNoResult()
    {
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("");

      var birdClient = new TestableBirdFact(webClient.Object);
      birdClient.Run();
      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    private BirdFactModel getBirdFact()
    {
      var fileName = Path.Combine("responseSamples", "birdFactResponse.json");
      string jsonString = File.ReadAllText(fileName);

      return JsonSerializer.Deserialize<BirdFactModel>(jsonString);
    }

    private class TestableBirdFact : CatFact
    {
      public IWebClient webClient { get; set; }

      public TestableBirdFact(IWebClient webClient) : base()
      {
        this.webClient = webClient;
      }

      protected override IWebClient GetWebClient() => this.webClient;
    }
  }
}