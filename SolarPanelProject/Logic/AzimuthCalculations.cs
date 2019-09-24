using System;

namespace SolarPanelProject.Logic
{
    internal class AzimuthCalculations
    {
    /// <summary>
    /// Returns current sun azimuth in degrees
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="sunAltitude"></param>
    /// <param name="magneticDeclination"></param>
    /// <param name="hourAngle"></param>
    /// <returns></returns>
        internal double CalculateSunAzimuth(float latitude, double sunAltitude, double magneticDeclination, double hourAngle)
        {
            Helpers helpers = new Helpers();

            return helpers.ConvertRadiansToDegree(Math.Acos((Math.Sin(helpers.ConvertDegreeToRadians(magneticDeclination)) * 
                             Math.Cos(helpers.ConvertDegreeToRadians(latitude)) - 
                             (Math.Cos(helpers.ConvertDegreeToRadians(magneticDeclination)) * 
                             Math.Sin(helpers.ConvertDegreeToRadians(latitude)) * 
                             Math.Cos(helpers.ConvertDegreeToRadians(hourAngle)))) 
                             / Math.Cos(helpers.ConvertDegreeToRadians(sunAltitude))));
        }
    }
}