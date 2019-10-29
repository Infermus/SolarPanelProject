using SolarPanelProject.Port;

namespace SolarPanelProject.ServoControlManager
{
    internal class ServoController
    {
        internal void SetServoPosition(string servoNumber, double degreeValue)
        {
            string mergedCommand = servoNumber + "." + degreeValue.ToString();

            new PortConnector().SendDataToCom(mergedCommand);
        }
    }
}