namespace GvasFormat.Serialization
{
    public class AstroneerObject
    {
        public string ClassName;
        public byte[] Body;
        public int InnerIndex { get; set; }
        public byte[] preHeader;
        public byte FlagByte;
        public byte[] indexOrAddress;
        public byte[] HeaderPostfix { get; set; }

        public bool HasFlag(byte bitIndex)
        {
            return (FlagByte & 1 << bitIndex) > 0;
        }
    }
}