using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Whiteboard.API
{
    class ServiceBusSender
    {
        private static IQueueClient queueClient;
        private static string ServiceBusConnectionString = Configuration.GetConnectionstring();
        private static string QueueName = Configuration.GetQueueName();


        public void Send(string messageText)
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            var m = new MessageHandlerOptions(ExceptionHandler)
            {
                AutoComplete = true
            };

            var message = new Message(Encoding.UTF8.GetBytes(messageText));

            queueClient.SendAsync(message);
            
        }



        private async static Task ExceptionHandler(ExceptionReceivedEventArgs ex)
        {
            await Task.Run(() => Console.WriteLine($"Exception: {ex.Exception.Message }"));
        }

    }
}
