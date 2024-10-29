using System;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class NextMarvelMovieModel
  {
      [JsonPropertyName("days_until")]
      public int DaysUntil { get; set; }

      [JsonPropertyName("overview")]
      public string Overview { get; set; }

      [JsonPropertyName("release_date")]
      public DateTime ReleaseDate { get; set; }

      [JsonPropertyName("title")]
      public string Title { get; set; }

      [JsonPropertyName("type")]
      public string Type { get; set; }

  }
}