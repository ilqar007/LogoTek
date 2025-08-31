namespace LogoTek.Application.Infrastructure.Dto
{
    public record AcknowledgeTelegramDto(TelegramHeaderDto TelegramHeader, string Acknowledge, string InformationText, char EndChar);
}