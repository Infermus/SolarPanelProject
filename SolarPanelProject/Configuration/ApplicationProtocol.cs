namespace SolarPanelProject.Configuration
{
    internal class ApplicationProtocol
    {
        internal const string DotNetReadyCommand = "dotnet.ready";

        internal const string ArduinoReadyCommand = "arduino.ready";

        internal const string FirstModeCommand = "mode.settype.0";

        internal const string SecondModeCommand = "mode.settype.1";

        internal const string BottomServoCommand = "bs.set.degress.";

        internal const string TopServoCommand = "ts.set.degress.";

        internal const string BreakModeCommand = "mode.break";

        internal const string StartParriedCommand = "start.paried";

        internal const string StartVoltageMeasurments = "start.voltage.measurments";

        internal const string StopVoltageMeasurments = "stop.voltage.measurments";
    }
}