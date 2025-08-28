using LogoTek.Infrastructure.Dto;
using LogoTek.Infrastructure.Utilities;
using TCP_Client;

namespace LogoTek.Presentation.TcpClient
{
    public partial class Form1 : Form
    {
        private MyTcpClient _client;
        private bool _isTcpClientDisposed;
        private int sequenceNumber;
        private IList<int> sequences = new List<int>(); //sequenceNumbers

        public Form1()
        {
            InitializeComponent();
            _client = new MyTcpClient();
            _client.MessageReceived += Server_MessageReceived;
        }

        private async Task btnConnect_Click(object sender, EventArgs e)
        {
            _isTcpClientDisposed = false;
            var ipAddress = txtBoxServerHostIp.Text;
            var port = int.Parse(txtBoxServerPort.Text);
            try
            {
                _client.Connect(ipAddress, port);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtBoxServerHostIp.Enabled = false;
            txtBoxServerPort.Enabled = false;
            btnDisconnect.Enabled = true;
            btnConnect.Enabled = false;
            sequenceNumber = 1;

            while (!_isTcpClientDisposed)
            {
                sequences.Add(sequenceNumber);
                TelegramHeaderDto telegramHeader = new TelegramHeaderDto('#', 82, "CTS_TRANSP", DateTime.Now.Date.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"), "C501", "WMS", sequenceNumber);
                StatusTelegramDto statusTelegram = new StatusTelegramDto(telegramHeader, "field row place", 999999, 999999, 9999, '\n');
                _client.SendMessage(statusTelegram.ToByteArray());
                await Task.Delay(5000);

                var key = sequences.FirstOrDefault();
                if (key != 0)
                {
                    sequenceNumber = key;
                }
                else
                {
                    sequenceNumber++;
                }
            }
        }

        private void Server_MessageReceived(object sender, byte[] message)
        {
            AcknowledgeTelegramDto acknowledgeTelegramDto = message.ExtractAcknowledgeTelegramDto();
            if (acknowledgeTelegramDto.Acknowledge == "ACK")
            {
                sequences.Remove(acknowledgeTelegramDto.TelegramHeader.SequenceNumber);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _client.Disconnect();
            _isTcpClientDisposed = true;
            txtBoxServerHostIp.Enabled = true;
            txtBoxServerPort.Enabled = true;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }
    }
}