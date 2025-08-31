namespace LogoTek.Infrastructure.Dto
{
    public record StatusTelegramDto(TelegramHeaderDto TelegramHeader, string PlaceId, string PosX, string PosY, string PosZ, char EndChar);
}