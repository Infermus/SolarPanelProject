
using System;

namespace SolarPanelProject.Logic
{
    internal class Others
    {
        Helpers helpers = new Helpers();
        internal double CountEarthMagneticDeclination()
        {
            return helpers.ConvertRadiansToDegree((helpers.ConvertDegreeToRadians(23.45)) *
                                                   Math.Sin(helpers.ConvertDegreeToRadians((360.00 / 365.00) * (DateTime.Now.DayOfYear - 81))));
        }
    }
}