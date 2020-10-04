using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityBelt.Models
{
    class CountryInformation
    {
        public string name { get; set; }
        public string capital { get; set; }
        public string region { get; set; }
        public int population { get; set; }
        public decimal area { get; set; }

        public List<Currencies> currencies { get; set; }
        public List<Languages> languages { get; set; }

    }

    class Currencies
    {
        public string code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
    }
    
    class Languages
    {
        public string name { get; set; }
        public string nativeName { get; set; }
    }

}
