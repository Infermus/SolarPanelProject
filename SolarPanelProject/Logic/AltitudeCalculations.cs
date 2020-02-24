using System;

namespace SolarPanelProject.Logic
{
    internal class AltitudeCalculations
    {
        Helpers helpers = new Helpers();

        internal double CountCurrentSunAltitude(float latitude, double magneticDeclination, double hourAngle)
        {
            return helpers.ConvertRadiansToDegree(Math.Asin(Math.Sin(helpers.ConvertDegreeToRadians(magneticDeclination)) * 
                                                            Math.Sin(helpers.ConvertDegreeToRadians(latitude)) + 
                                                            Math.Cos(helpers.ConvertDegreeToRadians(magneticDeclination)) * 
                                                            Math.Cos(helpers.ConvertDegreeToRadians(latitude)) * 
                                                            Math.Cos(helpers.ConvertDegreeToRadians(hourAngle))));
        }

        internal double CountMaximumSunAltitude(float latitude)
        {
            double maxAltitude = 90 + latitude - new Others().CountEarthMagneticDeclination();

            return Math.Abs(maxAltitude > 90 ? maxAltitude - 180 : maxAltitude);
        }
    }
}   