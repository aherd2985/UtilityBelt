
namespace UtilityBelt
{
    public partial class CovidRoot
    {
        public GlobalCovid Global {get; set;}
        public CountriesInfoCovid[] Countries {get; set;}
    }

    public partial class GlobalCovid
    {
        public long NewConfirmed {get; set;}
        public long TotalConfirmed {get; set;}
        public long NewDeaths {get; set;}
        public long TotalDeaths {get; set;}
        public long NewRecovered {get; set;}
        public long TotalRecovered {get; set;}
    }

    public partial class CountriesInfoCovid
    {
        public string Country {get; set;}
        public long NewConfirmed {get; set;}
        public long TotalConfirmed {get; set;}
        public long NewDeaths {get; set;}
        public long TotalDeaths {get; set;}
        public long NewRecovered {get; set;}
        public long TotalRecovered {get; set;}
    }
}
