using System.Collections.Generic;

namespace SolarPanelProject.Data
{
    public class ArduinoRequests
    {
        internal List<KeyValuePair<string, float>> SetLocalizationDataByUserInput(string longitude, string latitude)
        {
            List<KeyValuePair<string, float>> gpsData = new List<KeyValuePair<string, float>>();
            try
            {
                gpsData.Add(new KeyValuePair<string, float>("Satellites", 0));
                gpsData.Add(new KeyValuePair<string, float>("Longitude", float.TryParse(longitude.Replace(".", ","), out float parsedLongitude) ?
                                                                         parsedLongitude
                                                                         : throw new System.InvalidCastException()));

                gpsData.Add(new KeyValuePair<string, float>("Latitude", float.TryParse(latitude.Replace(".", ","), out float parsedLatitude) ?
                                                                        parsedLatitude
                                                                        : throw new System.InvalidCastException()));
            }
            catch (System.InvalidCastException)
            {
                return new List<KeyValuePair<string, float>>();
            }

            return gpsData;
        }
    }
}
