namespace SolarPanelProject.OutputMessages
{
    public class InternalMessages
    {
        //Mode selection
        public const string SelectedPhotoresistorsMode = "Photoresistor mode selected";

        public const string SelectedTrackerMode= "Tracker mode selected";

        // Logger output
        public const string PortStatus = "Port open status: " ;

        public const string ArduinoNotReady = "Arduino is not ready. Please check COM port or restart the board";

        public const string PariedSuccessful = "Arduino paired successful";

        //Message boxes

        public const string NoDataFromCordsTextboxes = "Please fill tracker user input data";

        public const string PortIsClosed = "Port object is not created or port is not open";
    }
}
