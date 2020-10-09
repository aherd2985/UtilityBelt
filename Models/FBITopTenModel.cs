using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class FBITopTenModel
  {

    
  }
  public class FBITopTenSuspect
  {
    [JsonPropertyName("age_range")]
    public string AgeRange { get; set; }
    [JsonPropertyName("weight")]
    public string Weight { get; set; }
    [JsonPropertyName("occupations")]
    public string Occupations { get; set; }
    [JsonPropertyName("locations")]
    public string Locations { get; set; }
    [JsonPropertyName("reward_text")]
    public string Reward { get; set; }
    [JsonPropertyName("hair")]
    public string Hair { get; set; }
    [JsonPropertyName("")]
    public string DatesOfBirthUsed { get; set; }
    public string Nationality { get; set; }
    public string ScarsAndMarks { get; set; }
    public string Aliases { get; set; }
    public string FieldOffice { get; set; }
    
  }
}
