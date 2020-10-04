using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
    public class TacoRecipeModel
    {
        [JsonPropertyName("base_layer")]
        public BaseLayer BaseLayer { get; set; }

        [JsonPropertyName("mixin")]
        public Mixin Mixin { get; set; }

        [JsonPropertyName("seasoning")]
        public Seasoning Seasoning { get; set; }

        [JsonPropertyName("condiment")]
        public Condiment Condiment { get; set; }

        [JsonPropertyName("shell")]
        public Shell Shell { get; set; }
    }

    public class BaseLayer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Mixin
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Seasoning
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Condiment
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Shell
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
