using SolarPanelProject.Models;
using System.Collections.Generic;

namespace SolarPanelProject.Data
{
    internal class Wrappers
    {
        internal static List<Cities> citiesWrapper = new List<Cities>()
        { 
            new Cities { CityName = "Głogów", Latitude = 51.66, Longitude = 16.08},
            new Cities { CityName = "Zielona Góra", Latitude = 51.93, Longitude = 15.50},
            new Cities { CityName = "Wrocław", Latitude = 51.10, Longitude = 17.03},
            new Cities { CityName = "Nowa Sól", Latitude = 51.80, Longitude = 15.71},
            new Cities { CityName = "Warszawa", Latitude = 52.23, Longitude = 21.00}
        };
    }
}