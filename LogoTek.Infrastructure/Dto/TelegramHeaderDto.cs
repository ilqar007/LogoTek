namespace LogoTek.Infrastructure.Dto
{
    public record TelegramHeaderDto(char StartChar, int TelegramLength, string MessageId, string Date, string Time, string Sender, string Receiver, int SequenceNumber);
}