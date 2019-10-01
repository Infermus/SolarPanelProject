namespace SolarPanelProject.Models.Logic
{
    public class SolarCalculationResults
    {
        public double Altitude { get; set; }

        public double Azimuth { get; set; }

        public double MagneticDeclination { get; set; }

        public double SunRise { get; set; }

        public double SunSet { get; set; }

        public double EquationOfTime { get; set; }

        public double TimeCorrectionFactor { get; set; }

        public double LocalSolarTime { get; set; }

        public double HourAngle { get; set; }
    }
}