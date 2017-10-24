using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace WhiteBoardAPI
{
    internal class ServiceBusListener
    {
        private static IQueueClient queueClient;
        private static string ServiceBusConnectionString = Configuration.GetConnectionstring();
        private static string QueueName = Configuration.GetQueueName();


        static ISubscriptionClient subClient;
        public void Listen()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName,ReceiveMode.ReceiveAndDelete);

            //subClient = new SubscriptionClient(ServiceBusConnectionString, QueueName, "Whiteboard");

            var m = new MessageHandlerOptions(ExceptionHandler)
            {
                AutoComplete = true
            };

            queueClient.RegisterMessageHandler(MessageHandler, m);
            //subClient.RegisterMessageHandler(MessageHandler, m);
            

            Console.WriteLine("Queue Listening process started! Press any key to exit the program.");
            Console.ReadKey();

        }


        private async static Task ExceptionHandler(ExceptionReceivedEventArgs ex)
        {
            await Task.Run(() => Console.WriteLine($"Exception: {ex.Exception.Message }"));
        }

        private async static Task MessageHandler(Message message, CancellationToken arg2)
        {
            await Task.Run(() => SerialPort.Queue(Encoding.UTF8.GetString(message.Body)));
            await Task.Run(() => SerialPort.Send());

            
        }


    }


}

