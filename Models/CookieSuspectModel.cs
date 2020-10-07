using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityBelt.Models
{
  public class CookieSuspectModel
  {
    public List<CookieSuspect> Results { get; set; }
  }
  public class CookieSuspect
  {
    public CookieSuspectName Name { get; set; }
  }
  public class CookieSuspectName
  {
    public string First { get; set; }

    public string Last { get; set; }
  }
}
