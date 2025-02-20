﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEEnumProperty : UEProperty
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false);

        public UEEnumProperty() { }
        public UEEnumProperty(BinaryReader reader, long valueLength, StringPool stringPool)
        {
            byte[] restLength = reader.ReadBytes(4);
            EnumType = stringPool.GetString(reader.ReadInt32());

            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            // valueLength starts here

            Value = stringPool.GetString(reader.ReadInt32());
        }

        public override void Serialize(BinaryWriter writer) => throw new NotImplementedException();

        public string EnumType;
        public string Value;
    }
}