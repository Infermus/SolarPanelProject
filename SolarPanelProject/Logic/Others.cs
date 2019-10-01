
using System;

namespace SolarPanelProject.Logic
{
    internal class Others
    {
        Helpers helpers = new Helpers();

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

        //TODO Figure out how to simply those 2 methods
        internal double SunRiseTimeCalculation(float latitude, float longitude, double magneticDeclination, double equationOfTime)
        {
            return 12.00 - (1.00 / 15.00) * (helpers.ConvertRadiansToDegree(Math.Acos(Math.Tan(-helpers.ConvertDegreeToRadians(latitude)) *
                                            Math.Tan(helpers.ConvertDegreeToRadians(magneticDeclination))))) - 
                                            new SolarTimeCalculations().TimeCorrectionFactor(longitude, equationOfTime, 15 * TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours) / 60;
        }

        internal double SunSetTimeCalculation(float latitude, float longitude, double magneticDeclination, double equationOfTime)
        {
            return 12.00 + (1.00 / 15.00) * (helpers.ConvertRadiansToDegree(Math.Acos(Math.Tan(-helpers.ConvertDegreeToRadians(latitude)) *
                                            Math.Tan(helpers.ConvertDegreeToRadians(magneticDeclination))))) -
                                            new SolarTimeCalculations().TimeCorrectionFactor(longitude, equationOfTime, 15 * TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours) / 60;
        }
    }
}