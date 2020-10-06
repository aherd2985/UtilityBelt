using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class Name    
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Result    
    {
        public string gender { get; set; }
        public Name name { get; set; }
        public string email { get; set; }
        public string phone { get; set; } 
        public string cell { get; set; }
        public string nat { get; set; }
    }

    public class Info    
    {
        public string seed { get; set; } 
        public int results { get; set; } 
        public int page { get; set; }
        public string version { get; set; }
    }

    public class RandomUser    
    {
        [JsonPropertyName("results")]
        public List<Result> results { get; set; }
        [JsonPropertyName("info")]
        public Info info { get; set; }
    }

    
}