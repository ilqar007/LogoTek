using LogoTek.Application.Infrastructure.DatabaseManager;
using LogoTek.Application.Infrastructure.Dto;
using LogoTek.Application.Infrastructure.Enums;
using LogoTek.Application.Infrastructure.Extensions;
using LogoTek.Application.Infrastructure.Services;
using LogoTek.Persistance.Database;
using TCP_Server;

namespace LogoTek.Presentation
{
    public partial class Form1 : Form
    {
        private MyTcpServer? _server;
        private bool _serverConnected = false;
        private readonly IDatabaseManager _databaseManager;
        private readonly ITelegramService _telegramService;

        public Form1(IDatabaseManager databaseManager, ITelegramService telegramService)
        {
            InitializeComponent();
            btnStartServer.Click += async (sender, e) => await btnStartServer_Click(sender, e).ConfigureAwait(false);
            btnListTelegrams.Click += async (sender, e) => await btnListTelegrams_Click(sender, e).ConfigureAwait(false);
            btnCreateSqlTable.Click += async (sender, e) => await btnCreateSqlTable_Click(sender, e).ConfigureAwait(false);
            _databaseManager = databaseManager;
            _telegramService = telegramService;
        }

        private async Task TelegramReceived(object sender, byte[] message)
        {
            StatusTelegramDto statusTelegramDto = message.ExtractStatusTelegramDto();
            int seqNum = statusTelegramDto.TelegramHeader.SequenceNumber;
            try
            {
                _server!.ReceivedTelegramsStatuses[seqNum] = TelegramProcessStatus.InProcess;
                await _telegramService.SaveAsync(statusTelegramDto).ConfigureAwait(false);
                _server.ReceivedTelegramsStatuses[seqNum] = TelegramProcessStatus.Success;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
                _server!.ReceivedTelegramsStatuses[seqNum] = TelegramProcessStatus.Failure;
            }
        }

        private async Task btnStartServer_Click(object sender, EventArgs e)
        {
            if (!_serverConnected)
            {
                try
                {
                    SetDBConnectionString();
                    _server = new MyTcpServer(txtBoxServerIpAddress.Text, int.TryParse(txtBoxServerPort.Text, out int port) ? port : 8083);
                    _server.TelegramReceived += async (sender, message) => await TelegramReceived(sender, message).ConfigureAwait(false);
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
                SetDBConnectionString();
                IEnumerable<StatusTelegramDto> telegrams = await _telegramService.GetAsync().ConfigureAwait(false);

                listBoxTelegrams.Invoke((MethodInvoker)(() =>
                {
                    listBoxTelegrams.Items.Clear();
                    foreach (var telegram in telegrams)
                    {
                        listBoxTelegrams.Items.Add(telegram);
                    }
                }));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void listBoxTelegrams_Click(object sender, EventArgs e)
        {
            StatusTelegramDto statusTelegram = (listBoxTelegrams.SelectedItem as StatusTelegramDto)!;
            if (statusTelegram == null) return;

            txtBoxPosX.Text = statusTelegram.PosX.ToString();
            txtBoxPosY.Text = statusTelegram.PosY.ToString();
            txtBoxPosZ.Text = statusTelegram.PosZ.ToString();
        }

        private async Task btnCreateSqlTable_Click(object sender, EventArgs e)
        {
            try
            {
                SetDBConnectionString();
                await _databaseManager.CreateTableAsync().ConfigureAwait(false);
                MessageBox.Show("Table created.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void SetDBConnectionString()
        {
            (_databaseManager as DatabaseManager)!.SqlConnection = txtBoxDbConnectionString.Text;
        }
    }
}