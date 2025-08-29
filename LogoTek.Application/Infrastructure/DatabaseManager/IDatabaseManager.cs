using LogoTek.Domain.Models;

namespace LogoTek.Application.Infrastructure.DatabaseManager
{
    public interface IDatabaseManager
    {
        Task<IEnumerable<Telegram>> GetDataAsync();

        Task SaveDataAsync(Telegram data);

        Task CreateTableAsync();
    }
}