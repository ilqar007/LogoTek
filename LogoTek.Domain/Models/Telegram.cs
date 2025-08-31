namespace LogoTek.Domain.Models
{
    public class Telegram
    {
        public string Process { get; set; }
        public short SeqNum { get; set; }
        public DateTime Teldt { get; set; }
        public string Teltype { get; set; }
        public short Tellen { get; set; }
        public string Idsndr { get; set; }
        public string Idrcvr { get; set; }
        public bool Status { get; set; }
        public byte[] Payload { get; set; }
        public int Pkid { get; set; }

        public override string ToString()
        {
            return System.Text.Encoding.ASCII.GetString((Payload)).Replace('\0', ' ');
        }
    }
}