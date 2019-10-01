using System;
using System.Collections.Generic;

namespace SolarPanelProject.Logic
{
    internal class SolarTimeCalculations
    {
        Helpers helpers = new Helpers();
        /// <summary>
        /// Returning List<KeyValuePair<string ,double>> with calculated solar times. 
        /// </summary>
        /// <param name="longitude"></param>
        /// <returns></returns>
        internal List<KeyValuePair<string, double>> CalculateSolarTimes (float longitude)
        {
            ///TODO Change method name
            List<KeyValuePair<string, double>> solarTimeCalculations = new List<KeyValuePair<string, double>>();
            {
                solarTimeCalculations.Add(new KeyValuePair<string, double>("EoT",EquationOfTime()));
                solarTimeCalculations.Add(new KeyValuePair<string, double>("TC", TimeCorrectionFactor(longitude, solarTimeCalculations[0].Value, 15 * TimeZoneInfo.Local.BaseUtcOffset.Hours)));
                solarTimeCalculations.Add(new KeyValuePair<string, double>("LST", LocalSolarTime(solarTimeCalculations[1].Value)));
                solarTimeCalculations.Add(new KeyValuePair<string, double>("HRA", HourAngle(longitude, solarTimeCalculations[2].Value)));
            }

            return solarTimeCalculations;
        }
        /// <summary>
        /// Returns Equation of Time in minutes.
        /// </summary>
        /// <returns></returns>
        internal double EquationOfTime()
        {
            double b = (360.00 / 365.00) * (DateTime.Now.DayOfYear - 81);
            return 9.87 * Math.Sin(helpers.ConvertDegreeToRadians(2 * b)) - 7.53 * Math.Cos(helpers.ConvertDegreeToRadians(b)) - 1.5 * Math.Sin(helpers.ConvertDegreeToRadians(b));
        }

        /// <summary>
        /// Returns Time Correction in minutes.
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="equationOftime"></param>
        /// <returns></returns>
        internal double TimeCorrectionFactor(float longitude, double equationOftime, int localStandardTimeMeridian)
        {
            return 4 * (longitude - localStandardTimeMeridian) + equationOftime;
        }

        /// <summary>
        /// Returns inaccurate hour, for ex. value of 18.50 is 18:30.
        /// </summary>
        /// <param name="timeCorrection"></param>
        /// <returns></returns>
        internal double LocalSolarTime(double timeCorrection)
        {
           return DateTime.Now.IsDaylightSavingTime() ? ((DateTime.Now.Hour - 1) * 60 + DateTime.Now.Minute + (timeCorrection / 60)) / 60 : 
                                                      ((DateTime.Now.Hour) * 60 + DateTime.Now.Minute + (timeCorrection / 60)) / 60;
        }

        /// <summary>
        /// Returns value in degree.
        /// Note: At solar noon hour angle should be 0°. In the morning negative, in the afternoon positive.
        /// </summary>
        /// <param name="longitude"></param>
        /// <returns></returns>
        internal double HourAngle(float longitude, double localSolarTime)
        {
            return 15 * (localSolarTime - 12);
        }
    }
}