using System;

namespace WhiteboardQueueSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var s = new ServiceBusSender();
            s.Send();

            Console.ReadLine();
        }
    }
}
