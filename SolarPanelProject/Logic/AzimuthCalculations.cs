using System;

namespace SolarPanelProject.Logic
{
    internal class AzimuthCalculations
    {
        internal double CalculateSunAzimuth(float latitude, double sunAltitude, double magneticDeclination, double hourAngle)
        {
            Helpers helpers = new Helpers();

            var sunAzimuth =  helpers.ConvertRadiansToDegree(Math.Acos((Math.Sin(helpers.ConvertDegreeToRadians(magneticDeclination)) * 
                             Math.Cos(helpers.ConvertDegreeToRadians(latitude)) - 
                             (Math.Cos(helpers.ConvertDegreeToRadians(magneticDeclination)) * 
                             Math.Sin(helpers.ConvertDegreeToRadians(latitude)) * 
                             Math.Cos(helpers.ConvertDegreeToRadians(hourAngle)))) /
                             Math.Cos(helpers.ConvertDegreeToRadians(sunAltitude))));

            return hourAngle < 0 ? sunAzimuth : 360 - sunAzimuth;
        }
    }
}