using CoordinateSharp;
using Newtonsoft.Json;
using SolarPanelProject.Data;
using SolarPanelProject.Enums;
using SolarPanelProject.Logic;
using SolarPanelProject.Models.LocationIQ;
using SolarPanelProject.Models.Port;
using SolarPanelProject.Port;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net.Http;
using System.Windows.Forms;

namespace SolarPanelProject
{

    public partial class MainWindow : Form
    {
        PortConnector portConnector = new PortConnector();


        public MainWindow()
        {
            InitializeComponent();
            portConnector.CreatePort();
            PortTextBox.DataSource = Enum.GetValues(typeof(COMPorts));
            ParityTextBox.DataSource = Enum.GetValues(typeof(Parity));
            StopBitsTextBox.DataSource = Enum.GetValues(typeof(StopBits));
            DataBitsTextBox.DataSource = new string[] {
                                                        ((int)COMDataBits.Five).ToString(),
                                                        ((int)COMDataBits.Six).ToString(),
                                                        ((int)COMDataBits.Seven).ToString(),
                                                        ((int)COMDataBits.Eight).ToString()
                                                      };
            SetDefaultSettings();
        }

        private void OpenPortButton_Click(object sender, EventArgs e)
        {
            portConnector.ConfigureCOMPort(new PortConfiguration
            {
                BaudRate = Int32.Parse(BaudRateTextBox.Text),
                Parity = (Parity)Enum.Parse(typeof(Parity), ParityTextBox.Text),
                PortName = PortTextBox.Text,
                StopBits = (StopBits)Enum.Parse(typeof(StopBits), StopBitsTextBox.Text),
                ReadTimeout = 5000,
                WriteTimeout = 5000,
                DataBits = Int32.Parse(DataBitsTextBox.Text),
                Rts = Rts.Checked
            });

            if (!portConnector.OpenPort())
            {
                MessageBox.Show("Port is already opened.", "Information");
            }
        }

        private void SetDefaultSettings()
        {
            PortTextBox.SelectedIndex = (int)COMPorts.COM8;
            ParityTextBox.SelectedIndex = (int)Parity.None;
            StopBitsTextBox.SelectedIndex = (int)StopBits.One;
            DataBitsTextBox.SelectedIndex = 3;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                ArduinoRequests arduinoRequests = new ArduinoRequests();
                
                List<KeyValuePair<string,float>> userLocalization = LatitudeTextBox.Text == string.Empty | LongitudeTextBox.Text == string.Empty ? 
                                                                    arduinoRequests.GetDataFromArduinoGPSModule(portConnector) :
                                                                    arduinoRequests.SetLocalizationDataByUserInput(LatitudeTextBox.Text, LongitudeTextBox.Text, portConnector);

                Uri processedUriAdress = new RequestLinkFormater().GenerateLocationIQLink(userLocalization);

                string jsonApiData = new HttpClient().GetStringAsync(processedUriAdress).Result;
                Localization parsedApiData = JsonConvert.DeserializeObject<Localization>(jsonApiData.ToString());

                double magneticDeclination = new Others().CountEarthMagneticDeclination();
                double hourAngle = new SolarTimeCalculations().HourAngle(parsedApiData.Longitude);
                double sunAltitude = new AltitudeCalculations().CountCurrentSunAltitude(parsedApiData.Latitude, parsedApiData.Longitude, magneticDeclination, hourAngle);   
                double sunAzimuth = new AzimuthCalculations().CalculateSunAzimuth(parsedApiData.Latitude, sunAltitude, magneticDeclination, hourAngle);
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void DisplayPortDataInLogger(string inputData, string outputData)
        {
            PortDataLoggerTextBox.AppendText("InputData: " + inputData + "/OutputData: " + outputData + "\n");
        }
    }
}