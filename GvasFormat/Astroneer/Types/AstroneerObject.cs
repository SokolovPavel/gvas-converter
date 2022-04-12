using System;
using System.Diagnostics;
using System.IO;
using GvasFormat.Serialization.UETypes;

namespace GvasFormat.Serialization
{
    [DebuggerDisplay("class: {ClassName}, body size:{_body.Length}", Name = "{InstanceName}")]
    public class AstroneerObject
    {
        public UEClassName ClassName;
        public string InstanceName { get; set; }
        private byte[] _body;
        public string BodyString;
        public int InnerIndex { get; set; }
        public byte[] preHeader;
        public byte FlagByte;
        public byte[] indexOrAddress;
        public byte[] HeaderPostfix { get; set; }

        public bool HasFlag(byte bitIndex)
        {
            return (FlagByte & 1 << bitIndex) > 0;
        }

        public virtual void SetBody(BinaryReader reader, int size, StringPool stringPool)
        {
            _body = reader.ReadBytes(size);
            BodyString = BitConverter.ToString(_body).Replace('-', ' ');
        }
    }
}