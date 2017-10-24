using System;
using System.Collections.Generic;
using RJCP.IO.Ports;
using System.Threading;
using System.Drawing;

namespace WhiteBoardAPI
{
    static class SerialPort
    {
        private static SerialPortStream myPort = null;
        private static List<string> _queue = new List<string>();
        

        internal static void Queue(string message)
        {
            _queue.Add(message);
        }

        internal static void init()
        {
            Console.WriteLine("Hello Serial!");
            Console.WriteLine(Environment.Version.ToString());
            string[] ports = GetPortNames();

            if (ports.Length == 0)
            {
                //We are on Windows
                myPort = new SerialPortStream("COM4", 115200, 8, Parity.None, StopBits.One);
                myPort.Open();
                if (!myPort.IsOpen)
                {
                    Console.WriteLine("Error opening serial port");
                    return;
                }
                Console.WriteLine("Port open");

                if (myPort == null)
                {
                    Console.WriteLine("No serial port /dev/ttyS0");
                    return;
                }
            }
            else
            {
                //We are on Linux

                foreach (var port in ports)
                    if (port == "/dev/ttyS0")
                    {
                        myPort = new SerialPortStream("/dev/ttyS0", 115200, 8, Parity.None, StopBits.One);
                        myPort.Open();
                        if (!myPort.IsOpen)
                        {
                            Console.WriteLine("Error opening serial port");
                            return;
                        }
                        Console.WriteLine("Port open");
                    }
                if (myPort == null)
                {
                    Console.WriteLine("No serial port /dev/ttyS0");
                    return;
                }
            }
            myPort.Handshake = Handshake.None;
            myPort.ReadTimeout = 1000;
            myPort.NewLine = "\n";

        }

        internal static void Send()
        {
            if (myPort == null)
                init();

            while(_queue.Count > 0)
            {
                var text = _queue[0];
                _queue.RemoveAt(0);
                myPort.WriteLine(text);
                Console.WriteLine($"Sent message: {text}");
                Thread.Sleep(250);
            }

            
        }

        private static string[] GetPortNames()
        {
            int p = (int)Environment.OSVersion.Platform;
            List<string> serial_ports = new List<string>();

            // Are we on Unix?
            if (p == 4 || p == 128 || p == 6)
            {
                string[] ttys = System.IO.Directory.GetFiles("/dev/", "tty\\*");
                foreach (string dev in ttys)
                {
                    if (dev.StartsWith("/dev/ttyS") || dev.StartsWith("/dev/ttyUSB") || dev.StartsWith("/dev/ttyACM") || dev.StartsWith("/dev/ttyAMA"))
                    {
                        serial_ports.Add(dev);
                        Console.WriteLine("Serial list: {0}", dev);
                    }
                }
            }
            return serial_ports.ToArray();
        }
    }
}