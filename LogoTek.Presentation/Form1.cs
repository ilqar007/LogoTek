using LogoTek.Application.Infrastructure.DatabaseManager;
using LogoTek.Domain.Models;
using LogoTek.Infrastructure.Dto;
using LogoTek.Infrastructure.Utilities;
using LogoTek.Persistance.Database;
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
            btnStartServer.Click += async (sender, e) => await btnStartServer_Click(sender, e);
            btnListTelegrams.Click += async (sender, e) => await btnListTelegrams_Click(sender, e);
            btnCreateSqlTable.Click += async (sender, e) => await btnCreateSqlTable_Click(sender, e);
        }

        private async Task SaveTelegramToDb(StatusTelegramDto statusTelegram)
        {
            IDatabaseManager databaseManager = new DatabaseManager(txtBoxDbConnectionString.Text);
            Telegram telegram = new Telegram { Idrcvr = statusTelegram.TelegramHeader.Receiver, Idsndr = statusTelegram.TelegramHeader.Sender, Payload = statusTelegram.ToByteArray(), Process = "CTS", SeqNum = (short)statusTelegram.TelegramHeader.SequenceNumber, Status = true, Teltype = "WMS_MATINF" };
            telegram.Tellen = (short)telegram.Payload.Length;
            telegram.Teldt = DateTime.TryParseExact($"{statusTelegram.TelegramHeader.Date}{statusTelegram.TelegramHeader.Time}", "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture,
                           System.Globalization.DateTimeStyles.None, out DateTime dateTime) ? dateTime : DateTime.MinValue;
            await databaseManager.SaveDataAsync(telegram);
        }

        private async Task TelegramReceived(object sender, byte[] message)
        {
            try
            {
                StatusTelegramDto statusTelegramDto = message.ExtractStatusTelegramDto();
                await SaveTelegramToDb(statusTelegramDto);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
                throw;
            }
        }

        private async Task btnStartServer_Click(object sender, EventArgs e)
        {
            if (!_serverConnected)
            {
                try
                {
                    _server = new MyTcpServer(txtBoxServerIpAddress.Text, int.TryParse(txtBoxServerPort.Text, out int port) ? port : 8083);
                    _server.TelegramReceived += async (sender, message) => await TelegramReceived(sender, message);
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

        private async Task btnListTelegrams_Click(object sender, EventArgs e)
        {
            try
            {
                IDatabaseManager databaseManager = new DatabaseManager(txtBoxDbConnectionString.Text);
                IEnumerable<Telegram> telegrams = await databaseManager.GetDataAsync();
                listBoxTelegrams.Items.Clear();
                foreach (var telegram in telegrams)
                {
                    listBoxTelegrams.Items.Add(telegram);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void listBoxTelegrams_Click(object sender, EventArgs e)
        {
            Telegram obj = listBoxTelegrams.SelectedItem as Telegram;
            StatusTelegramDto statusTelegram = obj.Payload.ExtractStatusTelegramDto();
            txtBoxPosX.Text = statusTelegram.PosX.ToString();
            txtBoxPosY.Text = statusTelegram.PosY.ToString();
            txtBoxPosZ.Text = statusTelegram.PosZ.ToString();
        }

        private async Task btnCreateSqlTable_Click(object sender, EventArgs e)
        {
            try
            {
                IDatabaseManager databaseManager = new DatabaseManager(txtBoxDbConnectionString.Text);
                await databaseManager.CreateTableAsync();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}