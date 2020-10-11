using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
  public class WeatherForecastTests
  {
    private readonly IServiceProvider services;
    private readonly IOptions<SecretsModel> options;
    private readonly Mock<IWebClient> webClient = new Mock<IWebClient>();

    public WeatherForecastTests()
    {
      this.services = ServiceProviderBuilder.GetServiceProvider(new string[] { "" });
      this.options = services.GetRequiredService<IOptions<SecretsModel>>();
    }

    [Fact]
    public void WeatherForecastReturnsForeCast()
    {
      var mockWeatherRootResult = getWeatherRootObject();
      var jsonresult = JsonSerializer.Serialize(mockWeatherRootResult);
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonresult);
      var weatherForecastClient = new TestableWeatherForecast(webClient.Object);

      weatherForecastClient.Configure(this.options);
      weatherForecastClient.Run("atlanta");

      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void WeatherForecastDoesNotThrowsIfNoResult()
    {
      this.webClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("");

      var weatherForecastClient = new TestableWeatherForecast(webClient.Object);
      weatherForecastClient.Run();

      this.webClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Never);
    }

    private WeatherRoot getWeatherRootObject()
    {
      var mockSyst = new Sys { Country = "Bolivia", Id = 123456, Sunrise = DateTimeOffset.UtcNow.AddHours(-8).ToUnixTimeSeconds(), Sunset = DateTimeOffset.UtcNow.AddHours(4).ToUnixTimeSeconds() };
      var mockMain = new Main { Pressure = 40, FeelsLike = 27, Humidity = 33, Temperature = 24, TempMax = 29, TempMin = 22 };
      var mockWind = new Wind { Degrees = 231, Speed = 22 };
      var mockWeatherRoot = new WeatherRoot
      {
        Main = mockMain,
        Sys = mockSyst,
        Wind = mockWind
      };

      return mockWeatherRoot;
    }

    private class TestableWeatherForecast : WeatherForecastUtility
    {
      public IWebClient webClient { get; set; }

      public TestableWeatherForecast(IWebClient webClient) : base()
      {
        this.webClient = webClient;
      }

      protected override IWebClient GetWebClient() => this.webClient;
    }
  }
}