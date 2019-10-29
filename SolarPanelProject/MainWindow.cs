using CoordinateSharp;
using Newtonsoft.Json;
using SolarPanelProject.Data;
using SolarPanelProject.Enums;
using SolarPanelProject.Logic;
using SolarPanelProject.Models.LocationIQ;
using SolarPanelProject.Models.Logic;
using SolarPanelProject.Models.Port;
using SolarPanelProject.Port;
using SolarPanelProject.ServoControlManager;
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

        private void Tracker(object sender, EventArgs e)
        {
            try
            {
                //temporary solutions
                ArduinoRequests arduinoRequests = new ArduinoRequests();
                Others others = new Others();
                
                List<KeyValuePair<string,float>> userLocalization = LatitudeTextBox.Text == string.Empty | LongitudeTextBox.Text == string.Empty ? 
                                                                    arduinoRequests.GetDataFromArduinoGPSModule(portConnector) :
                                                                    arduinoRequests.SetLocalizationDataByUserInput(LatitudeTextBox.Text, LongitudeTextBox.Text, portConnector);

                Uri processedUriAdress = new RequestLinkFormater().GenerateLocationIQLink(userLocalization);

                string jsonApiData = new HttpClient().GetStringAsync(processedUriAdress).Result;
                Localization parsedApiData = JsonConvert.DeserializeObject<Localization>(jsonApiData.ToString());

                List<KeyValuePair<string,double>> solarTimeCalculations = new SolarTimeCalculations().CalculateSolarTimes(parsedApiData.Longitude);
                SolarCalculationResults solarCalculationResults = new SolarCalculationResults()
                {
                    MagneticDeclination = others.CountEarthMagneticDeclination(),
                    EquationOfTime = solarTimeCalculations[0].Value,
                    TimeCorrectionFactor = solarTimeCalculations[1].Value,
                    LocalSolarTime = solarTimeCalculations[2].Value,              
                    HourAngle = solarTimeCalculations[3].Value,
                };
                solarCalculationResults.Altitude = new AltitudeCalculations().CountCurrentSunAltitude(parsedApiData.Latitude, parsedApiData.Longitude, solarCalculationResults.MagneticDeclination, solarCalculationResults.HourAngle);
                solarCalculationResults.Azimuth = new AzimuthCalculations().CalculateSunAzimuth(parsedApiData.Latitude, solarCalculationResults.Altitude, solarCalculationResults.MagneticDeclination, solarCalculationResults.HourAngle);
                solarCalculationResults.SunRise = others.SunRiseTimeCalculation(parsedApiData.Latitude, parsedApiData.Longitude, solarCalculationResults.MagneticDeclination, solarCalculationResults.EquationOfTime);
                solarCalculationResults.SunSet = others.SunSetTimeCalculation(parsedApiData.Latitude, parsedApiData.Longitude, solarCalculationResults.MagneticDeclination, solarCalculationResults.EquationOfTime);
                new ServoController().SetServoPosition("2", 100);

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

        private void Photoresistors(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}