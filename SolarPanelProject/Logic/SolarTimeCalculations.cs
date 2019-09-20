using System;

namespace SolarPanelProject.Logic
{
    internal class SolarTimeCalculations
    {
        Helpers helpers = new Helpers();
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
        internal double TimeCorrectionFactor(float longitude, double equationOftime)
        {
            int localStandardTimeMeridian = 15 * TimeZoneInfo.Local.BaseUtcOffset.Hours;
            return 4 * (longitude - localStandardTimeMeridian) + equationOftime;
        }

        /// <summary>
        /// Returns inaccurate hour, for ex. value of 18.50 is 18:30.
        /// </summary>
        /// <param name="timeCorrection"></param>
        /// <returns></returns>
        internal double LocalSolarTime(double timeCorrection)
        {
            return ((DateTime.Now.Hour - 1) * 60 + DateTime.Now.Minute + (timeCorrection / 60)) / 60;
        }

        /// <summary>
        /// Returns value in degree.
        /// Note: At solar noon hour angle should be 0°. In the morning negative, in the afternoon positive.
        /// </summary>
        /// <param name="longitude"></param>
        /// <returns></returns>
        internal double HourAngle(float longitude)
        {
            return 15 * (LocalSolarTime(TimeCorrectionFactor(longitude, EquationOfTime())) - 12);
        }
    }
}