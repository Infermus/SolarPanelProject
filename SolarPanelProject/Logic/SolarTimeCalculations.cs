using System;
using System.Collections.Generic;

namespace SolarPanelProject.Logic
{
    internal class SolarTimeCalculations
    {
        Helpers helpers = new Helpers();
        internal List<KeyValuePair<string, double>> CalculateSolarTimes (float longitude)
        {
            List<KeyValuePair<string, double>> solarTimeCalculations = new List<KeyValuePair<string, double>>();
            {
                solarTimeCalculations.Add(new KeyValuePair<string, double>("EoT", EquationOfTime()));
                solarTimeCalculations.Add(new KeyValuePair<string, double>("TC", TimeCorrectionFactor(longitude, solarTimeCalculations[0].Value, 15 * TimeZoneInfo.Local.BaseUtcOffset.Hours)));
                solarTimeCalculations.Add(new KeyValuePair<string, double>("LST", LocalSolarTime(solarTimeCalculations[1].Value)));
                solarTimeCalculations.Add(new KeyValuePair<string, double>("HRA", HourAngle(solarTimeCalculations[2].Value)));
            }

            return solarTimeCalculations;
        }

        internal double EquationOfTime()
        {
            double b = (360.00 / 365.00) * (DateTime.Now.DayOfYear - 81);
            return 9.87 * Math.Sin(helpers.ConvertDegreeToRadians(2 * b)) - 7.53 * Math.Cos(helpers.ConvertDegreeToRadians(b)) - 1.5 * Math.Sin(helpers.ConvertDegreeToRadians(b));
        }

        internal double TimeCorrectionFactor(float longitude, double equationOftime, int localStandardTimeMeridian)
        {
            return 4 * (longitude - localStandardTimeMeridian) + equationOftime;
        }

        internal double LocalSolarTime(double timeCorrection)
        {
           return DateTime.Now.IsDaylightSavingTime() ? ((DateTime.Now.Hour - 1) * 60 + DateTime.Now.Minute + (timeCorrection / 60)) / 60 : 
                                                      ((DateTime.Now.Hour) * 60 + DateTime.Now.Minute + (timeCorrection / 60)) / 60;
        }

        internal double HourAngle(double localSolarTime)
        {
            return 15 * (localSolarTime - 12);
        }
    }
}