using System.IO.Ports;

namespace SolarPanelProject.Models.Port
{
    internal class PortConfiguration
    {
        internal int BaudRate { get; set; }

        internal Parity Parity { get; set; }

        internal string PortName { get; set; }

        internal StopBits StopBits { get; set; }

        internal int ReadTimeout { get; set;  }

        internal int WriteTimeout { get; set; }

        internal int DataBits { get; set; }

        internal bool Rts { get; set; }
    }
}