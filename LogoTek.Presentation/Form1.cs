using LogoTek.Infrastructure.Dto;
using LogoTek.Infrastructure.Utilities;
using TCP_Server;

namespace LogoTek.Presentation
{
    public partial class Form1 : Form
    {
        private MyTcpServer? _server;
        private bool _serverConnected = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void TelegramReceived(object sender, byte[] message)
        {
            StatusTelegramDto statusTelegramDto = message.ExtractStatusTelegramDto();
        }

        private async Task btnStartServer_Click(object sender, EventArgs e)
        {
            if (!_serverConnected)
            {
                try
                {
                    _server = new MyTcpServer(txtBoxServerIpAddress.Text, int.TryParse(txtBoxServerPort.Text, out int port) ? port : 8083);
                    _server.TelegramReceived += TelegramReceived;
                    btnStartServer.Text = "Stop server";
                    _serverConnected = true;
                    await _server.Start();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message + Environment.NewLine);
                    _serverConnected = false;
                }
            }
            else
            {
                _server?.Stop();
                _serverConnected = false;

                btnStartServer.Text = "Start server";
            }
        }
    }
}