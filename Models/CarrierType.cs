using System;

namespace UtilityBelt
{
    public class CarrierMetaAttribute : Attribute
    {
        public string Name { get; set; }
        public string Domain { get; set; }
    }
    public enum CarrierType
    {
        [CarrierMeta(Name = "ATT", Domain = "txt.att.net")]
        ATT = 1,
        [CarrierMeta(Name = "Verizon", Domain = "vtext.com")]
        Verizon,
        [CarrierMeta(Name = "Sprint", Domain = "messaging.sprintpcs.com")]
        Sprint,
        [CarrierMeta(Name = "TMobile", Domain = "tmomail.net")]
        TMobile,
        [CarrierMeta(Name = "Virgin Mobile", Domain = "vmobl.com")]
        VirginMobile,
        [CarrierMeta(Name = "Nextel", Domain = "messaging.nextel.com")]
        Nextel,
        [CarrierMeta(Name = "Boost", Domain = "myboostmobile.com")]
        Boost,
        [CarrierMeta(Name = "Alltel", Domain = "message.alltel.com")]
        Alltel,
        [CarrierMeta(Name = "EE", Domain = "mms.ee.co.uk")]
        EE,
    }
}
