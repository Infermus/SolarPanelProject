using Newtonsoft.Json;
using SolarPanelProject.Configuration;
using SolarPanelProject.Data;
using SolarPanelProject.Enums;
using SolarPanelProject.Logic;
using SolarPanelProject.Models.LocationIQ;
using SolarPanelProject.Models.Logic;
using SolarPanelProject.Models.Port;
using SolarPanelProject.OutputMessages;
using SolarPanelProject.Port;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace SolarPanelProject
{

    public partial class MainWindow : Form
    {
        PortConnector portConnector = new PortConnector();
        private BackgroundWorker bw;

        public MainWindow()
        {
            InitializeComponent();
            portConnector.CreatePort();
            PortTextBox.DataSource = SerialPort.GetPortNames();
            ParityTextBox.DataSource = Enum.GetValues(typeof(Parity));
            StopBitsTextBox.DataSource = Enum.GetValues(typeof(StopBits));
            DataBitsTextBox.DataSource = new string[] {
                                                        ((int)COMDataBits.Five).ToString(),
                                                        ((int)COMDataBits.Six).ToString(),
                                                        ((int)COMDataBits.Seven).ToString(),
                                                        ((int)COMDataBits.Eight).ToString()
                                                      };
            SetDefaultSettings();
            SetLocalizationBySelectedCity();
        }

        private void OpenPortButton_Click(object sender, EventArgs e)
        {
            portConnector.ConfigureCOMPort(new PortConfiguration
            {
                BaudRate = int.Parse(BaudRateTextBox.Text),
                Parity = (Parity)Enum.Parse(typeof(Parity), ParityTextBox.Text),
                PortName = PortTextBox.Text,
                StopBits = (StopBits)Enum.Parse(typeof(StopBits), StopBitsTextBox.Text),
                ReadTimeout = 2500,
                WriteTimeout = 2500,
                DataBits = int.Parse(DataBitsTextBox.Text),
                Rts = Rts.Checked
            });

            portConnector.OpenPort();

            var receivedData = portConnector.GetCurrentDataFromSerialPort();
            DisplayPortDataInLogger($"{InternalMessages.PortStatus} {portConnector.IsPortOpen()}");

            if (receivedData.Equals(ApplicationProtocol.ArduinoReadyCommand))
            {
                DisplayPortDataInLogger(receivedData);
                portConnector.SendDataToCom(ApplicationProtocol.DotNetReadyCommand);
            }
            else
            {
                DisplayPortDataInLogger(InternalMessages.ArduinoNotReady);
            }

            for (int i = 0; i <= 5; i++)
            {
                receivedData = portConnector.GetCurrentDataFromSerialPort();
                DisplayPortDataInLogger(receivedData);

                if (receivedData.Equals(ApplicationProtocol.StartParriedCommand))
                {
                    DisplayPortDataInLogger(InternalMessages.PariedSuccessful);
                    bw = new BackgroundWorker();
                    bw.DoWork += Bw_DoWork;
                    bw.RunWorkerAsync();
                    break;
                }
            }
        }

        private void SetDefaultSettings()
        {
            ParityTextBox.SelectedIndex = (int)Parity.None;
            StopBitsTextBox.SelectedIndex = (int)StopBits.One;
            DataBitsTextBox.SelectedIndex = 3;
        }

        private void TrackerMode_Click(object sender, EventArgs e)
        {
            try
            {
                BreakCurrentMode();

                ArduinoRequests arduinoRequests = new ArduinoRequests();
                List<KeyValuePair<string, float>> userLocalization = arduinoRequests.SetLocalizationDataByUserInput(LatitudeTextBox.Text, LongitudeTextBox.Text);

                if (userLocalization.Count == 0)
                {
                    MessageBox.Show(InternalMessages.NoDataFromCordsTextboxes, MessageCaptions.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                portConnector.SendDataToCom(ApplicationProtocol.FirstModeCommand);
                DisplayPortDataInLogger(InternalMessages.SelectedTrackerMode);

                Uri processedUriAdress = new RequestLinkFormater().GenerateLocationIQLink(userLocalization);

                string jsonApiData = new HttpClient().GetStringAsync(processedUriAdress).Result;
                Localization parsedApiData = JsonConvert.DeserializeObject<Localization>(jsonApiData.ToString());

                List<KeyValuePair<string, double>> solarTimeCalculations = new SolarTimeCalculations().CalculateSolarTimes(parsedApiData.Longitude);
                Others others = new Others();
                SolarCalculationResults solarCalculationResults = new SolarCalculationResults()
                {
                    MagneticDeclination = others.CountEarthMagneticDeclination(),
                    EquationOfTime = solarTimeCalculations[0].Value,
                    TimeCorrectionFactor = solarTimeCalculations[1].Value,
                    LocalSolarTime = solarTimeCalculations[2].Value,
                    HourAngle = solarTimeCalculations[3].Value,
                };
                solarCalculationResults.Altitude = new AltitudeCalculations().CountCurrentSunAltitude(parsedApiData.Latitude, solarCalculationResults.MagneticDeclination, solarCalculationResults.HourAngle);
                solarCalculationResults.Azimuth = new AzimuthCalculations().CalculateSunAzimuth(parsedApiData.Latitude, solarCalculationResults.Altitude, solarCalculationResults.MagneticDeclination, solarCalculationResults.HourAngle);
                solarCalculationResults.SunRise = others.SunRiseTimeCalculation(parsedApiData.Latitude, parsedApiData.Longitude, solarCalculationResults.MagneticDeclination, solarCalculationResults.EquationOfTime);
                solarCalculationResults.SunSet = others.SunSetTimeCalculation(parsedApiData.Latitude, parsedApiData.Longitude, solarCalculationResults.MagneticDeclination, solarCalculationResults.EquationOfTime);

                var limitedAltitude = solarCalculationResults.Altitude < 10 ? 10 : solarCalculationResults.Altitude;
                var mappedAzimuth = (270.00 / 360.00) * solarCalculationResults.Azimuth;
                mappedAzimuth = mappedAzimuth > 180 ? 180: mappedAzimuth;
                portConnector.SendDataToCom($"{ApplicationProtocol.BottomServoCommand} {Math.Abs(mappedAzimuth - 180)}");
                portConnector.SendDataToCom($"{ApplicationProtocol.TopServoCommand} {limitedAltitude}");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void DisplayPortDataInLogger(string message)
        {
            PortDataLoggerTextBox.ScrollToCaret();
                PortDataLoggerTextBox.AppendText(message + "\n");
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var receivedData = portConnector.GetCurrentDataFromSerialPort();

                if (!receivedData.Equals(string.Empty))
                {
                    Action action = () => DisplayPortDataInLogger(receivedData);
                    Invoke(action);
                }
            }
        }

        private void PhotoresistorsMode_Click(object sender, EventArgs e)
        {
            BreakCurrentMode();
            portConnector.SendDataToCom(ApplicationProtocol.SecondModeCommand);
            DisplayPortDataInLogger(InternalMessages.SelectedPhotoresistorsMode);
        }

        private void OnPort_DropDown(object sender, EventArgs e)
        {
            PortTextBox.DataSource = SerialPort.GetPortNames();
        }

        private void BreakCurrentMode()
        {
            portConnector.SendDataToCom(ApplicationProtocol.BreakModeCommand);
            Thread.Sleep(200);
        }

        private void SetLocalizationBySelectedCity()
        {
            CitySelectTextBox.DataSource = Wrappers.citiesWrapper.Select(city => city.CityName).ToList();
        }

        private void SetLocalizationBySelectedCity_DropDown(object sender, EventArgs e)
        {
            int dataIndex = Wrappers.citiesWrapper.FindIndex(x => x.CityName.Equals(CitySelectTextBox.Text, StringComparison.Ordinal));
            LatitudeTextBox.Text = Wrappers.citiesWrapper[dataIndex].Latitude.ToString();
            LongitudeTextBox.Text = Wrappers.citiesWrapper[dataIndex].Longitude.ToString();
        }

        private void VoltageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (voltageCheckBox.Checked)
            {
                portConnector.SendDataToCom(ApplicationProtocol.StartVoltageMeasurments);
            }
            else
                portConnector.SendDataToCom(ApplicationProtocol.StopVoltageMeasurments);
        }
    }
}