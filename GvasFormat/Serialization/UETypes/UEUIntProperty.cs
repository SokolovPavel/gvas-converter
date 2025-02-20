﻿using System;
using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEUIntProperty : UEProperty
    {
        public UEUIntProperty() { }
        public UEUIntProperty(BinaryReader reader, long valueLength)
        {
            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            if (valueLength != sizeof(uint))
                throw new FormatException($"Expected int value of length {sizeof(uint)}, but was {valueLength}");
            Value = reader.ReadUInt32();
        }

        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public uint Value;
    }
}