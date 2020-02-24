using SolarPanelProject.Models.Port;
using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using SolarPanelProject.OutputMessages;
using SolarPanelProject.Configuration;

namespace SolarPanelProject.Port
{
    internal class PortConnector
    {
        public static SerialPort myport;

        internal void CreatePort()
        {
            if (myport == null)
            {
                myport = new SerialPort();
            }
        }

        internal void ConfigureCOMPort(PortConfiguration portConfiguration)
        {
            try
            {
                myport.BaudRate = portConfiguration.BaudRate;
                myport.Parity = portConfiguration.Parity;
                myport.PortName = portConfiguration.PortName;
                myport.StopBits = portConfiguration.StopBits;
                myport.ReadTimeout = portConfiguration.ReadTimeout;
                myport.WriteTimeout = portConfiguration.WriteTimeout;
                myport.DataBits = portConfiguration.DataBits;
                myport.RtsEnable = portConfiguration.Rts;
            }
            catch (Exception)
            {

            }
        }

        internal bool OpenPort()
        {
            try
            {
                if (myport.IsOpen == false)
                {
                    myport.Open();
                    Thread.Sleep(100);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MainWindow mainWindow = (MainWindow)Application.OpenForms["MainWindow"];
                mainWindow.DisplayPortDataInLogger(ex.Message);
                return false;
            }
        }

        internal bool IsPortOpen()
        {
            return myport.IsOpen;
        }

        internal string GetCurrentDataFromSerialPort()
        {
            try
            {
                if (myport.IsOpen && myport != null)
                {
                    Thread.Sleep(100);
                    var receivedData = myport.ReadLine();
                    return receivedData.Replace("\r", string.Empty);
                }
                else
                {
                    return InternalMessages.PortIsClosed;
                }
            }
            catch (TimeoutException)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        internal void SendDataToCom(string dataToSend)
        {
            if (myport != null && myport.IsOpen)
            {
                Thread.Sleep(TimeConfigurations.PortSendingDataDelay);
                myport.Write(dataToSend);
            }
        }
    }
}
