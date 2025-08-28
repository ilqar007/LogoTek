namespace LogoTek.Infrastructure.Dto
{
    public record StatusTelegramDto(TelegramHeaderDto TelegramHeader, string PlaceId, int PosX, int PosY, int PosZ, char EndChar);
}