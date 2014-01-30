using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace App2
{
    class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof (Program));

        static void Main(string[] args)
        {
            try
            {
                XmlConfigurator.Configure();

                var ipAddress = IPAddress.Parse("127.0.0.1");

                var listener = new TcpListener(ipAddress, 8001);

                listener.Start();

                _logger.Info("The server is running at port 8001");
                _logger.InfoFormat("The local endpoint is: {0}", listener.LocalEndpoint);

                var clientConnected = listener.AcceptTcpClientAsync();

                ProcessConnectedClient(listener, clientConnected);

                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                _logger.Error("FAILED", ex);
            }
        }

        static void ProcessConnectedClient(TcpListener listener, Task<TcpClient> clientConnected)
        {
            HandleError(clientConnected.ContinueWith(x =>
            {
                var client = x.Result;
                var stream = client.GetStream();

                _logger.InfoFormat("Connected Client: {0}", client.Client.LocalEndPoint);

                ReadLine(Guid.Empty, new StreamReader(stream));

                ProcessConnectedClient(listener, listener.AcceptTcpClientAsync());
            }, TaskContinuationOptions.OnlyOnRanToCompletion));
        }

        static void ReadLine(Guid clientId, StreamReader stream)
        {
            HandleError(stream.ReadLineAsync()
                .ContinueWith(x =>
                {
                    var line = x.Result;

                    if (clientId == Guid.Empty)
                    {
                        clientId = Guid.Parse(line);
                    }
                    else
                    {
                        _logger.InfoFormat("Received: {0} [From: {1}]", line, clientId);
                    }

                    if (stream.BaseStream.CanRead)
                    {
                        ReadLine(clientId, stream);
                    }
                }, TaskContinuationOptions.OnlyOnRanToCompletion));
        }

        static void HandleError(Task task)
        {
            task.ContinueWith(x => _logger.Error("FAILED", x.Exception), TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
