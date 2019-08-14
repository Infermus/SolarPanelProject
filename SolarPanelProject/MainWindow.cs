using Newtonsoft.Json;
using SolarPanelProject.Data;
using SolarPanelProject.Enums;
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
                RequestLinkFormater requestLinkFormater = new RequestLinkFormater();
                HttpClient client = new HttpClient();

                List<KeyValuePair<string,float>> userLocalization = LatitudeTextBox.Text == string.Empty | LongitudeTextBox.Text == string.Empty ? 
                                                                    arduinoRequests.GetDataFromArduinoGPSModule(portConnector) :
                                                                    arduinoRequests.SetLocalizationDataByUserInput(LatitudeTextBox.Text, LongitudeTextBox.Text, portConnector);

                var processedUriAdress = requestLinkFormater.GenerateLocationIQLink(userLocalization);

                var jsonApiData = client.GetStringAsync(processedUriAdress).Result;
                var parsedApiData = JsonConvert.DeserializeObject<Localization>(jsonApiData.ToString());

                //portConnector.SendDataToCom("ResetServo");
                //DialogResult result = MessageBox.Show("Please calibrate compass to the south. When you are ready press 'OK' button.", "Configuration",
                //                             MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

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