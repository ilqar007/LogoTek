using LogoTek.Application.Infrastructure.Dto;
using LogoTek.Application.Infrastructure.Extensions;
using TCP_Client;

namespace LogoTek.Presentation.TcpClient
{
    public partial class Form1 : Form
    {
        private MyTcpClient _client;
        private bool _isTcpClientDisposed;
        private int sequenceNumber;
        private IDictionary<int, StatusTelegramDto> statusesToSend = new Dictionary<int, StatusTelegramDto>(); //sequenceNumber,StatusTelegramDto

        public Form1()
        {
            InitializeComponent();
            btnConnect.Click += async (sender, e) => await btnConnect_Click(sender, e);
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

            while (!_isTcpClientDisposed)
            {
                StatusTelegramDto statusTelegram;
                if (statusesToSend.Any())
                {
                    statusTelegram = statusesToSend.First().Value;
                }
                else
                {
                    sequenceNumber++;
                    TelegramHeaderDto telegramHeader = new TelegramHeaderDto('#', 82, "CTS_TRANSP", DateTime.Now.Date.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"), "C501", "WMS", sequenceNumber);
                    statusTelegram = new StatusTelegramDto(telegramHeader, "field row place", "X999999", "Y999999", "Z9999", '\n');
                    statusesToSend.Add(telegramHeader.SequenceNumber, statusTelegram);
                }
                _client.SendMessage(statusTelegram.ToByteArray());
                await Task.Delay(5000);
            }
        }

        private void Server_MessageReceived(object sender, byte[] message)
        {
            AcknowledgeTelegramDto acknowledgeTelegramDto = message.ExtractAcknowledgeTelegramDto();
            if (acknowledgeTelegramDto.Acknowledge == "ACK")
            {
                statusesToSend.Remove(acknowledgeTelegramDto.TelegramHeader.SequenceNumber);
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