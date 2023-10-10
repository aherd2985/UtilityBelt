using System;
using System.Collections.Generic;

namespace UtilityBelt.Models
{
  public class SpaceXModel
  {
    public class Core
    {
      public string core { get; set; }
      public int? flight { get; set; }
      public bool gridfins { get; set; }
      public bool legs { get; set; }
      public bool reused { get; set; }
      public bool landing_attempt { get; set; }
      public bool landing_success { get; set; }
      public string landing_type { get; set; }
      public string landpad { get; set; }
    }

    public class Crew
    {
      public string crew { get; set; }
      public string role { get; set; }
    }

    public class Flickr
    {
      public List<object> small { get; set; }
      public List<string> original { get; set; }
    }

    public class Links
    {
      public Patch patch { get; set; }
      public Reddit reddit { get; set; }
      public Flickr flickr { get; set; }
      public string presskit { get; set; }
      public string webcast { get; set; }
      public string youtube_id { get; set; }
      public string article { get; set; }
      public string wikipedia { get; set; }
    }

    public class Patch
    {
      public string small { get; set; }
      public string large { get; set; }
    }

    public class Reddit
    {
      public string campaign { get; set; }
      public string launch { get; set; }
      public string media { get; set; }
      public string recovery { get; set; }
    }

    public object fairings { get; set; }
    public Links links { get; set; }
    public DateTime? static_fire_date_utc { get; set; }
    public int? static_fire_date_unix { get; set; }
    public bool tdb { get; set; }
    public bool net { get; set; }
    public int? window { get; set; }
    public string rocket { get; set; }
    public bool success { get; set; }
    public List<object> failures { get; set; } = new List<object>();
    public string details { get; set; }
    public List<Crew> crew { get; set; } = new List<Crew>();
    public List<string> ships { get; set; } = new List<string>();
    public List<string> capsules { get; set; } = new List<string>();
    public List<string> payloads { get; set; } = new List<string>();
    public string launchpad { get; set; }
    public bool auto_update { get; set; }
    public int? flight_number { get; set; }
    public string name { get; set; }
    public DateTime? date_utc { get; set; }
    public int? date_unix { get; set; }
    public DateTime? date_local { get; set; }
    public string date_precision { get; set; }
    public bool upcoming { get; set; }
    public List<Core> cores { get; set; }
    public string id { get; set; }
  }
}
