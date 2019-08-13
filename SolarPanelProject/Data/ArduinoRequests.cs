using System.Collections.Generic;
using System.Globalization;
using SolarPanelProject.Configuration;
using SolarPanelProject.Port;

namespace SolarPanelProject.Data
{
    public class ArduinoRequests
    {
        internal List<KeyValuePair<string, float>> SetLocalizationDataByUserInput(string longitude, string latitude, PortConnector portConnector)
        {
            List<KeyValuePair<string, float>> gpsData = new List<KeyValuePair<string, float>>();
            { 

                gpsData.Add(new KeyValuePair<string, float>("Satellites", 0));
                gpsData.Add(new KeyValuePair<string, float>("Longitude", float.TryParse(longitude.Replace(".", ","), out float parsedLongitude) ? 
                                                                         parsedLongitude 
                                                                         : throw new System.InvalidCastException()));

                gpsData.Add(new KeyValuePair<string, float>("Latitude", float.TryParse(latitude.Replace(".", ","), out float parsedLatitude) ? 
                                                                        parsedLatitude 
                                                                        : throw new System.InvalidCastException()));
            }

            return gpsData;
        }

        internal List<KeyValuePair<string, float>> GetDataFromArduinoGPSModule(PortConnector portConnector)
        {
            string dataFromCom = string.Empty;
            List<KeyValuePair<string, float>> gpsDatas = new List<KeyValuePair<string, float>>();

            List<string> operations = new List<string>()
            {
                OperationTypes.SatellitesArduinoMessage,
                OperationTypes.LatitudeArduinoMessage,
                OperationTypes.LongitudeArduinoMessage,
            };

            foreach (string operation in operations)
            {
                gpsDatas.Add(new KeyValuePair<string, float>(operation,
                                                            float.Parse(portConnector.GetDataFromCom(operation), CultureInfo.InvariantCulture.NumberFormat)));
            }

            return gpsDatas;
        }
    }
}
