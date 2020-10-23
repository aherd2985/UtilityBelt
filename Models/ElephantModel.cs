using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  class ElephantModel
  {
    [JsonPropertyName("_id")]
    public string Id { get; set; }
    [JsonPropertyName("index")]
    public int Index { get; set; }
    [JsonPropertyName("name")]
    public string ElephantName { get; set; }
    [JsonPropertyName("affiliation")]
    public string Affiliation { get; set; }
    [JsonPropertyName("species")]
    public string Species { get; set; }
    [JsonPropertyName("sex")]
    public string Sex { get; set; }
    [JsonPropertyName("fictional")]
    public string Fictional { get; set; }
    [JsonPropertyName("dob")]
    public string DateOfBirth { get; set; }
    [JsonPropertyName("dod")]
    public string DateOfDeath { get; set; }
    [JsonPropertyName("wikilink")]
    public string Wikilink { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("note")]
    public string Note { get; set; }

  }
}
