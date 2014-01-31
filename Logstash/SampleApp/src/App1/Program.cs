using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using log4net;
using log4net.Config;

namespace App1
{
    class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof (Program));

        static void Main(string[] args)
        {
            try
            {
                XmlConfigurator.Configure();

                var clientId = Guid.NewGuid();

                var client = new TcpClient();
                client.Connect(IPAddress.Parse("127.0.0.1"), 8001);

                _logger.InfoFormat("Connected [Client: {0}]", clientId);

                string line;

                var streamWriter = new StreamWriter(client.GetStream());
                streamWriter.WriteLine(clientId);

                if (args != null && args[0] == "-a")
                {
                    var random = new Random();
                    int index = 0;

                    while (true)
                    {
                        line = string.Format("application event {0}", index++);
                        _logger.InfoFormat("Sending: {0} [Client: {1}]", line, clientId);
                        streamWriter.WriteLine(line);
                        streamWriter.Flush();

                        Thread.Sleep(random.Next(300, 5000));
                    }
                }
                else
                {
                    while (!String.IsNullOrEmpty(line = Console.ReadLine()))
                    {
                        _logger.InfoFormat("Sending: {0} [Client: {1}]", line, clientId);
                        streamWriter.WriteLine(line);
                        streamWriter.Flush();
                    }
                }

                _logger.InfoFormat("Disconnecting [Client: {0}]", clientId);
            }
            catch (Exception ex)
            {
                _logger.Error("FAILED", ex);
            }
        }
    }
}
