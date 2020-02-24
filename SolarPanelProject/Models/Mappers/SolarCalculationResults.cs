namespace SolarPanelProject.Models.Logic
{
    internal class SolarCalculationResults
    {
        internal double Altitude { get; set; }

        internal double Azimuth { get; set; }

        internal double MagneticDeclination { get; set; }

        internal double SunRise { get; set; }

        internal double SunSet { get; set; }

        internal double EquationOfTime { get; set; }

        internal double TimeCorrectionFactor { get; set; }

        internal double LocalSolarTime { get; set; }

        internal double HourAngle { get; set; }
    }
}