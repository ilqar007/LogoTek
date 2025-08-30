using LogoTek.Infrastructure.Dto;
using LogoTek.Infrastructure.Enums;
using LogoTek.Infrastructure.Utilities;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace TCP_Server
{
    public class MyTcpServer
    {
        private TcpListener _server;
        private readonly string _ipAddress;
        private readonly int _port;

        public event EventHandler<byte[]> TelegramReceived;

        public IDictionary<int, TelegramProcessStatus> ReceivedTelegramsStatuses = new Dictionary<int, TelegramProcessStatus>();

        public int Port => _port;

        /// <summary>
        /// TCP Server helper class
        /// </summary>
        /// <param name="ipAddress">Default at loopback IP (Cannot be accessed from the outside world)</param>
        /// <param name="port"></param>
        public MyTcpServer(string ipAddress = "127.0.0.1", int port = 8083)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public async Task Start()
        {
            // Abort operation if server is already running
            if (_server != null)
            {
                throw new InvalidOperationException("Server is already running!");
            }

            IPAddress localAddr = IPAddress.Parse(_ipAddress);
            _server = new TcpListener(localAddr, _port);
            _server.Start();

            Debug.WriteLine("Server started. Waiting for a connection...");

            while (true)
            {
                // Handle new client connection
                TcpClient client = await _server.AcceptTcpClientAsync().ConfigureAwait(false);
                _ = HandleClientAsync(client).ConfigureAwait(false);
            }
        }

        public void Stop()
        {
            _server?.Stop();
            _server = null;
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[82];

            // Receive data in a loop until the client disconnects
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

                if (bytesRead == 0)
                {
                    break; // Client disconnected
                }

                AcknowledgeTelegramDto acknowledgeTelegramDto;
                TelegramHeaderDto headerDto = buffer.ExtractTelegramHeaderDto();

                // Raise the MessageReceived event
                var status = TelegramProcessStatus.ToBeProcessed;
                ReceivedTelegramsStatuses[headerDto.SequenceNumber] = status;
                OnMessageReceived(buffer);

                while (status == TelegramProcessStatus.ToBeProcessed || status == TelegramProcessStatus.InProcess)
                {
                    status = ReceivedTelegramsStatuses[headerDto.SequenceNumber];
                }
                if (status == TelegramProcessStatus.Success)
                {
                    acknowledgeTelegramDto = new AcknowledgeTelegramDto(headerDto, "ACK", "All good", '\n');
                }
                else
                {
                    acknowledgeTelegramDto = new AcknowledgeTelegramDto(headerDto, "NACK", $"Error", '\n');
                }
                ReceivedTelegramsStatuses.Remove(headerDto.SequenceNumber);
                byte[] response = acknowledgeTelegramDto.ToByteArray();

                await stream.WriteAsync(response, 0, response.Length);
            }
        }

        /// <summary>
        /// Send message to the UI thread
        /// </summary>
        /// <param name="message"></param>
        protected virtual void OnMessageReceived(byte[] message)
        {
            TelegramReceived?.Invoke(this, message);
        }
    }
}