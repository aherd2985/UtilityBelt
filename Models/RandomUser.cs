using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class Name
  {
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("first")]
    public string First { get; set; }
    [JsonPropertyName("last")]
    public string Last { get; set; }
    }

    public class Result
  {
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("name")]
    public Name Name { get; set; }
    [JsonPropertyName("email")]
        public string Email { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
    [JsonPropertyName("cell")]
    public string Cell { get; set; }
    [JsonPropertyName("nat")]
    public string Nat { get; set; }
    }

    public class Info
  {
    [JsonPropertyName("seed")]
    public string Seed { get; set; }
    [JsonPropertyName("results")]
    public int Results { get; set; }
    [JsonPropertyName("page")]
    public int Page { get; set; }
    [JsonPropertyName("version")]
    public string Version { get; set; }
    }

    public class RandomUser    
    {
        [JsonPropertyName("results")]
        public List<Result> results { get; set; }
        [JsonPropertyName("info")]
        public Info Info { get; set; }
    }

    
}