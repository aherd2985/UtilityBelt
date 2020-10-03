using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityBelt.Models
{
  public class BitcoinPrice
  {
    public BitcoinTime time { get; set; }
    public string disclaimer { get; set; }
    public string chartName { get; set; }
    public BitcoinBpi bpi { get; set; }
  }
  public class BitcoinTime
  {
    public string updated { get; set; }
    public DateTime updatedISO { get; set; }
    public string updateduk { get; set; }
  }

  public class BitcoinUSD
  {
    public string code { get; set; }
    public string symbol { get; set; }
    public string rate { get; set; }
    public string description { get; set; }
    public double rate_float { get; set; }
  }

  public class BitcoinGBP
  {
    public string code { get; set; }
    public string symbol { get; set; }
    public string rate { get; set; }
    public string description { get; set; }
    public double rate_float { get; set; }
  }

  public class BitcoinEUR
  {
    public string code { get; set; }
    public string symbol { get; set; }
    public string rate { get; set; }
    public string description { get; set; }
    public double rate_float { get; set; }
  }

  public class BitcoinBpi
  {
    public BitcoinUSD USD { get; set; }
    public BitcoinGBP GBP { get; set; }
    public BitcoinEUR EUR { get; set; }
  }
}
