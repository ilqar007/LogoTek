using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoTek.Application.Infrastructure.Dto
{
    public record TelegramHeaderDto(char StartChar, int TelegramLength, string MessageId, string Date, string Time, string Sender, string Receiver, int SequenceNumber);
}