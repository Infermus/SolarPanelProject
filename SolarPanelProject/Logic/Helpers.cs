using System;

namespace SolarPanelProject.Logic
{
    internal class Helpers
    {
        internal double ConvertDegreeToRadians(double angle)
        {
            return (Math.PI * angle) / 180;
        }

        internal double ConvertRadiansToDegree(double angle)
        {
            return (angle * (180.0 / Math.PI));
        }
    }
}
