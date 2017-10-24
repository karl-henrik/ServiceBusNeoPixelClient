using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WhiteBoardAPI
{
    class Configuration
    {
        static MySettings settings;

        private static void init()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("connectionstring.secret.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            settings = new MySettings();
            configuration.Bind(settings);

        }

        public static string GetConnectionstring()
        {
            if (settings == null)
            {
                init();
            }

            return settings.ConnectionString;
            
        }

        public static string GetQueueName()
        {
            if (settings == null)
            {
                init();
            }

            return settings.QueueName;

        }

    }

    public class MySettings
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
    
}
