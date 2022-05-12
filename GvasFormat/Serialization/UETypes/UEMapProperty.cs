using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GvasFormat.Utils;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("Count = {Map.Count}", Name = "{Name}")]
    public sealed class UEMapProperty : UEProperty
    {
        public UEMapProperty()
        {
        }

        public UEMapProperty(BinaryReader reader, long valueLength, StringPool stringPool)
        {
            byte[] restLength = reader.ReadBytes(4);
            var keyType = stringPool.GetString(reader.ReadInt32());
            var valueType = stringPool.GetString(reader.ReadInt32());
            var unknown = reader.ReadBytes(5);
            if (unknown.Any(b => b != 0))
                throw new InvalidOperationException(
                    $"Offset: 0x{reader.BaseStream.Position - 5:x8}. Expected ??? to be 0, but was 0x{unknown.AsHex()}");

            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                UEProperty key, value;
                if (keyType == "StructProperty")
                    key = Read(reader, stringPool);
                else
                    key = UESerializer.Deserialize(null, keyType, -1, reader, stringPool);
                var values = new List<UEProperty>();
                do
                {
                    if (valueType == "StructProperty")
                        value = Read(reader, stringPool);
                    else
                        value = UESerializer.Deserialize(null, valueType, -1, reader, stringPool);
                    values.Add(value);
                } while (!(value is UENoneProperty));

                Map.Add(new UEKeyValuePair {Key = key, Values = values});
            }
        }

        public override void Serialize(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public List<UEKeyValuePair> Map = new List<UEKeyValuePair>();

        public class UEKeyValuePair
        {
            public UEProperty Key;
            public List<UEProperty> Values;
        }
    }
}