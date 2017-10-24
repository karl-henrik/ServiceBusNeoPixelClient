using System;
using System.Collections.Generic;
using RJCP.IO.Ports;
using System.Threading;
using System.Drawing;

namespace WhiteBoardAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var listener = new ServiceBusListener();

                SerialPort.init();
                Thread.Sleep(1000);

                listener.Listen();
            }
            finally
            {
                SerialPort.Close();
            }
            
        }
        
    }
}