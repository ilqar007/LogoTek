using LogoTek.Infrastructure.Dto;
using System.Text;

namespace LogoTek.Infrastructure.Utilities
{
    public static class TelegramUtilities
    {
        public static byte[] ToByteArray(this TelegramHeaderDto telegramHeaderDto)
        {
            byte[] telegramHeader = new byte[42];
            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes($"#{telegramHeaderDto.TelegramLength}"), 5), 0, telegramHeader, 1, 5);
            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes(telegramHeaderDto.MessageId), 10), 0, telegramHeader, 6, 10);
            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes(telegramHeaderDto.Date), 8), 0, telegramHeader, 16, 8);
            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes(telegramHeaderDto.Time), 6), 0, telegramHeader, 24, 6);
            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes(telegramHeaderDto.Sender), 4), 0, telegramHeader, 30, 4);
            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes(telegramHeaderDto.Receiver), 4), 0, telegramHeader, 34, 4);
            Array.Copy(CompleteArrayToTheLength(BitConverter.GetBytes(telegramHeaderDto.SequenceNumber), 4), 0, telegramHeader, 38, 4);

            return telegramHeader;
        }

        public static byte[] RemoveZeros(byte[] arr)
        {
            return arr.Where(b => b != 0).ToArray();
        }

        public static TelegramHeaderDto ExtractTelegramHeaderDto(this byte[] telegram)
        {
            byte[] telegramLengthArr = new byte[6];
            Array.Copy(telegram, 0, telegramLengthArr, 0, 6);
            string telegramLen = UTF8Encoding.Default.GetString(RemoveZeros(telegramLengthArr));

            byte[] messageIdArr = new byte[10];
            Array.Copy(telegram, 6, messageIdArr, 0, 10);
            string messageIdStr = UTF8Encoding.Default.GetString(RemoveZeros(messageIdArr));

            byte[] dateArr = new byte[8];
            Array.Copy(telegram, 16, dateArr, 0, 8);
            string dateStr = UTF8Encoding.Default.GetString(RemoveZeros(dateArr));

            byte[] timeArr = new byte[6];
            Array.Copy(telegram, 24, timeArr, 0, 6);
            string timeStr = UTF8Encoding.Default.GetString(RemoveZeros(timeArr));

            byte[] senderArr = new byte[4];
            Array.Copy(telegram, 30, senderArr, 0, 4);
            string senderStr = UTF8Encoding.Default.GetString(RemoveZeros(senderArr));

            byte[] receiverArr = new byte[4];
            Array.Copy(telegram, 34, receiverArr, 0, 4);
            string receiverStr = UTF8Encoding.Default.GetString(RemoveZeros(receiverArr));

            byte[] sequeceNumberArr = new byte[4];
            Array.Copy(telegram, 38, sequeceNumberArr, 0, 4);
            int sequeceNumber = BitConverter.ToInt32(sequeceNumberArr, 0);

            TelegramHeaderDto telegramHeader = new TelegramHeaderDto(telegramLen[0], int.TryParse(telegramLen.Substring(1), out int telegramL) ? telegramL : -1, messageIdStr, dateStr, timeStr, senderStr, receiverStr, sequeceNumber);

            return telegramHeader;
        }

        private static byte[] CompleteArrayToTheLength(byte[] array, int length)
        {
            byte[] buffer = new byte[length];
            for (int i = 0; i < array.Length; i++)
            {
                buffer[i] = array[i];
            }

            return buffer;
        }

        public static byte[] ToByteArray(this StatusTelegramDto statusTelegramDto)
        {
            byte[] telegramHeader = ToByteArray(statusTelegramDto.TelegramHeader);
            byte[] payload = new byte[40];

            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes(statusTelegramDto.PlaceId), 20), 0, payload, 0, 20);
            Array.Copy(CompleteArrayToTheLength(BitConverter.GetBytes(statusTelegramDto.PosX), 7), 0, payload, 20, 7);
            Array.Copy(CompleteArrayToTheLength(BitConverter.GetBytes(statusTelegramDto.PosY), 7), 0, payload, 27, 7);
            Array.Copy(CompleteArrayToTheLength(BitConverter.GetBytes(statusTelegramDto.PosZ), 5), 0, payload, 34, 5);
            payload[39] = 0x0a;

            return telegramHeader.Concat(payload).ToArray();
        }

        public static StatusTelegramDto ExtractStatusTelegramDto(this byte[] telegram)
        {
            TelegramHeaderDto telegramHeaderDto = ExtractTelegramHeaderDto(telegram);

            byte[] placeIdArr = new byte[20];
            Array.Copy(telegram, 42, placeIdArr, 0, 20);
            string placeIdStr = UTF8Encoding.Default.GetString(RemoveZeros(placeIdArr));

            byte[] posXArr = new byte[7];
            Array.Copy(telegram, 62, posXArr, 0, 7);
            int posX = BitConverter.ToInt32(posXArr, 0);

            byte[] posYArr = new byte[7];
            Array.Copy(telegram, 69, posYArr, 0, 7);
            int posY = BitConverter.ToInt32(posYArr, 0);

            byte[] posZArr = new byte[5];
            Array.Copy(telegram, 76, posZArr, 0, 5);
            int posZ = BitConverter.ToInt32(posZArr, 0);

            char endChar = Convert.ToChar(telegram[81]);

            StatusTelegramDto statusTelegramDto = new StatusTelegramDto(telegramHeaderDto, placeIdStr, posX, posY, posZ, endChar);

            return statusTelegramDto;
        }

        public static byte[] ToByteArray(this AcknowledgeTelegramDto acknowledgeTelegramDto)
        {
            byte[] telegramHeader = ToByteArray(acknowledgeTelegramDto.TelegramHeader);
            byte[] payload = new byte[85];
            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes(acknowledgeTelegramDto.Acknowledge), 4), 0, payload, 0, 4);
            Array.Copy(CompleteArrayToTheLength(UTF8Encoding.Default.GetBytes(acknowledgeTelegramDto.InformationText), 80), 0, payload, 4, 80);
            payload[84] = 0x0a;

            return telegramHeader.Concat(payload).ToArray();
        }

        public static AcknowledgeTelegramDto ExtractAcknowledgeTelegramDto(this byte[] telegram)
        {
            TelegramHeaderDto telegramHeaderDto = ExtractTelegramHeaderDto(telegram);

            byte[] ackowledgeArr = new byte[4];
            Array.Copy(telegram, 42, ackowledgeArr, 0, 4);
            string ackowledgeArrStr = UTF8Encoding.Default.GetString(RemoveZeros(ackowledgeArr));

            byte[] informationTextArr = new byte[80];
            Array.Copy(telegram, 46, informationTextArr, 0, 80);
            string informationTextStr = UTF8Encoding.Default.GetString(RemoveZeros(informationTextArr));

            char endChar = Convert.ToChar(telegram[126]);

            AcknowledgeTelegramDto acknowledgeTelegramDto = new AcknowledgeTelegramDto(telegramHeaderDto, ackowledgeArrStr, informationTextStr, endChar);

            return acknowledgeTelegramDto;
        }
    }
}