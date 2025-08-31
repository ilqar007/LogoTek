using LogoTek.Application.Infrastructure.Extensions;

namespace LogoTek.Application.Infrastructure.Dto
{
    public record StatusTelegramDto(TelegramHeaderDto TelegramHeader, string PlaceId, string PosX, string PosY, string PosZ, char EndChar)
    {
        public override string ToString()
        {
            return System.Text.Encoding.ASCII.GetString(this.ToByteArray()).Replace('\0', ' ');
        }
    };
}