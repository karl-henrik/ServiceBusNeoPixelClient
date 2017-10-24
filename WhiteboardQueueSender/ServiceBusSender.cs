using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhiteboardQueueSender
{
    class ServiceBusSender
    {
        private static IQueueClient queueClient;
        private static string ServiceBusConnectionString = Configuration.GetConnectionstring();
        private static string QueueName = Configuration.GetQueueName();
        
        
        public void Send()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            var m = new MessageHandlerOptions(ExceptionHandler)
            {
                AutoComplete = true
            };

            int i = 0;
            var change = 1;

            while(!Console.KeyAvailable)
            {
                var message = new Message(Encoding.UTF8.GetBytes($"{i}:255&{i -change}:0"));
                queueClient.SendAsync(message);

                if (i == 9)
                    change = -1;
                if (i == 0)
                    change = 1;

                i += change;

                Thread.Sleep(500);
            }
            
            
               
            Console.WriteLine("Queue Listening process started! Press any key to exit the program.");
            Console.ReadKey();

        }
        


        private async static Task ExceptionHandler(ExceptionReceivedEventArgs ex)
        {
            await Task.Run(() => Console.WriteLine($"Exception: {ex.Exception.Message }"));
        }
        
    }
}
