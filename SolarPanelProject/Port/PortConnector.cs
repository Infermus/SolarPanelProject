using SolarPanelProject.Models.Port;
using System;
using System.IO.Ports;

namespace SolarPanelProject.Port
{
    internal class PortConnector
    {
        private SerialPort myport;

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
            if (myport.IsOpen == false)
            {
                myport.Open();
                return true;
            }
            else
            {
                return false;
            }
        }

        internal string GetDataFromCom(string operationType)
        {
            string dateFromCom = string.Empty;

            try
            {
                SendDataToCom(operationType);
                dateFromCom = myport.ReadLine();
            }

            catch (Exception ex)
            {
                dateFromCom = string.Empty;
            }
            return dateFromCom;
        }

        internal void SendDataToCom(string dataToSend)
        {
            if (myport != null)
            {
                myport.Write(dataToSend);
            }
        }

    }
}
