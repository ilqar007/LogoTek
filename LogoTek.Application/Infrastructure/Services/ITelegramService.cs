using LogoTek.Application.Infrastructure.Dto;

namespace LogoTek.Application.Infrastructure.Services
{
    public interface ITelegramService
    {
        Task SaveAsync(StatusTelegramDto statusTelegram);

        Task<IEnumerable<StatusTelegramDto>> GetAsync();
    }
}