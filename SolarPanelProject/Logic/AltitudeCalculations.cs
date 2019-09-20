using System;

namespace SolarPanelProject.Logic
{
    internal class AltitudeCalculations
    {
        Helpers helpers = new Helpers();
        /// <summary>
        /// Returns current sun altitude/elevation in degrees.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        internal double CountCurrentSunAltitude(float latitude, float longitude, double magneticDeclination, double hourAngle)
        {
            return helpers.ConvertRadiansToDegree(Math.Asin(Math.Sin(helpers.ConvertDegreeToRadians(magneticDeclination)) * 
                                                            Math.Sin(helpers.ConvertDegreeToRadians(latitude)) + 
                                                            Math.Cos(helpers.ConvertDegreeToRadians(magneticDeclination)) * 
                                                            Math.Cos(helpers.ConvertDegreeToRadians(latitude)) * 
                                                            Math.Cos(helpers.ConvertDegreeToRadians(hourAngle))));
        }

        internal double CountMaximumSunAltitude(float latitude)
        {
            double maxAltitude = 90 + latitude - CountEarthMagneticDeclination();

            return Math.Abs(maxAltitude > 90 ? maxAltitude - 180 : maxAltitude);
        }

        /// <summary>
        /// Returns earth magnetic declination in degrees.
        /// Note: At equinoxes declination should be 0°, at winter solstice -23.45°, at summer solstice 23.45°.
        /// </summary>
        /// <returns></returns>
        internal double CountEarthMagneticDeclination()
        {
            return helpers.ConvertRadiansToDegree((helpers.ConvertDegreeToRadians(23.45)) *
                                                   Math.Sin(helpers.ConvertDegreeToRadians((360.00 / 365.00) * (DateTime.Now.DayOfYear - 81))));
        }
    }
}   