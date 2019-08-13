using SolarPanelProject.Configuration;
using SolarPanelProject.Enums;
using System.Linq;
using System.Collections.Generic;
using System;

namespace SolarPanelProject.Data
{
    internal class RequestLinkFormater
    {
        internal Uri GenerateLocationIQLink(List<KeyValuePair<string, float>> gpsData)
        {
            string processingUriAdress = ApiConf.LocationIQAddress;
            List<string> processedGpsData = gpsData.Select(x => x.Value.ToString().Replace(",", ".")).ToList();

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(LocationIQParams.Key.ToString(), ApiConf.LocationIQApiKey),
                new KeyValuePair<string, string>(LocationIQParams.Lat.ToString(), processedGpsData[1]),
                new KeyValuePair<string, string>(LocationIQParams.Lon.ToString(), processedGpsData[2]),
                new KeyValuePair<string, string>(LocationIQParams.Format.ToString(), "json")
            };

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                processingUriAdress = processingUriAdress + AddParameterToUrl(parameter.Key.ToLower() + "=", parameter.Value);
            }

            processingUriAdress = processingUriAdress.EndsWith("&") ? processingUriAdress.Remove(processingUriAdress.Length - 1, 1) : processingUriAdress;
            return new Uri(processingUriAdress, UriKind.Absolute);
        }

        internal string AddParameterToUrl(string baseText, string textToAdd)
        {
            string localText = baseText;

            if (string.IsNullOrEmpty(textToAdd) == false)
            {
                localText = baseText + textToAdd + "&";
            }

            return localText;
        }
    }
}