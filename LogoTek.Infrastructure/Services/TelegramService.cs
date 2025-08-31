using LogoTek.Application.Infrastructure.DatabaseManager;
using LogoTek.Application.Infrastructure.Dto;
using LogoTek.Application.Infrastructure.Extensions;
using LogoTek.Application.Infrastructure.Services;
using LogoTek.Domain.Models;

namespace LogoTek.Infrastructure.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly IDatabaseManager _databaseManager;

        public TelegramService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<IEnumerable<StatusTelegramDto>> GetAsync()
        {
            return (await _databaseManager.GetDataAsync().ConfigureAwait(false)).Select(t => t.Payload.ExtractStatusTelegramDto());
        }

        public async Task SaveAsync(StatusTelegramDto statusTelegram)
        {
            Telegram telegram = new Telegram { Idrcvr = statusTelegram.TelegramHeader.Receiver, Idsndr = statusTelegram.TelegramHeader.Sender, Payload = statusTelegram.ToByteArray(), Process = "CTS", SeqNum = (short)statusTelegram.TelegramHeader.SequenceNumber, Status = true, Teltype = "WMS_MATINF" };
            telegram.Tellen = (short)telegram.Payload.Length;
            telegram.Teldt = DateTime.TryParseExact($"{statusTelegram.TelegramHeader.Date}{statusTelegram.TelegramHeader.Time}", "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture,
                           System.Globalization.DateTimeStyles.None, out DateTime dateTime) ? dateTime : DateTime.MinValue;

            await _databaseManager.SaveDataAsync(telegram).ConfigureAwait(false);
        }
    }
}