using System.Net.Sockets;

namespace TCP_Client
{
    public class MyTcpClient
    {
        private TcpClient _client;

        public event EventHandler<byte[]> MessageReceived;

        /// <summary>
        /// Connect to the TCP server
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void Connect(string ipAddress, int port)
        {
            _client = new TcpClient();
            _client.Connect(ipAddress, port);

            // Start a separate task to receive data
            _ = ReceiveDataAsync();
        }

        /// <summary>
        /// Send message to the connected server
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(byte[] message)
        {
            NetworkStream stream = _client.GetStream();
            stream.Write(message, 0, message.Length);
        }

        /// <summary>
        /// Handle incoming data from the server
        /// </summary>
        private async Task ReceiveDataAsync()
        {
            NetworkStream stream = _client.GetStream();
            byte[] buffer = new byte[127];

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    break; // Server or client disconnected
                }

                OnMessageReceived(buffer);
            }

            _client.Close();
        }

        public void Disconnect()
        {
            _client?.Close();
        }

        protected virtual void OnMessageReceived(byte[] message)
        {
            MessageReceived?.Invoke(this, message);
        }
    }
}