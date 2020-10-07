using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class Name    
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Result    
    {
        public string Gender { get; set; }
        public Name Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } 
        public string Cell { get; set; }
        public string Nat { get; set; }
    }

    public class Info    
    {
        public string Seed { get; set; } 
        public int Results { get; set; } 
        public int Page { get; set; }
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