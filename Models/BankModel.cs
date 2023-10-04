using System.Text.Json.Serialization;

namespace UtilityBelt.Models
{
  public class BankModel
  {
    [JsonPropertyName("account_number")]
    public string AccountNumber { get; set; }
    [JsonPropertyName("iban")]
    public string IBAN { get; set; }
    [JsonPropertyName("bank_name")]
    public string BankName { get; set; }
    [JsonPropertyName("routing_number")]
    public string RoutingNumber { get; set; }
    [JsonPropertyName("swift_bic")]
    public string SwiftBic { get; set; }
  }

}
