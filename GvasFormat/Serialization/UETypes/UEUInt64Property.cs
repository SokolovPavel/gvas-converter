using System;
using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEUInt64Property : UEProperty
    {
        public UEUInt64Property(BinaryReader reader, long valueLength)
        {
            byte[] zeros = reader.ReadBytes(5);
            Value = reader.ReadUInt64();
        }

        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public ulong Value;
    }
}