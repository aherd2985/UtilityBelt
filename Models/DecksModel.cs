using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class DecksModel
  {
    [JsonPropertyName("deck_id")]
    public string Id { get; set; }
    [JsonPropertyName("shuffled")]
    public bool Shuffled { get; set; }
    [JsonPropertyName("remaining")]
    public int Remaining { get; set; }
  }

  public class DecksModelCard
  {
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonPropertyName("suit")]
    public string Suit { get; set; }
    [JsonPropertyName("code")]
    public string Code { get; set; }
  }

  public class DecksModelCardPick
  {
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("cards")]
    public List<DecksModelCard> Cards { get; set; }
    [JsonPropertyName("deck_id")]
    public string Id { get; set; }
    [JsonPropertyName("remaining")]
    public int Remaining { get; set; }
  }
}
