using System;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    public abstract class UEProperty
    {
        public string Name;
        public string Type;

        public abstract void Serialize(BinaryWriter writer);

        public static UEProperty Read(BinaryReader reader, StringPool stringPool)
        {
            if (reader.PeekChar() < 0)
                return null;

            int nameIndex = reader.ReadInt32();
            var name = stringPool.GetString(nameIndex - 1);
            if (name == null)
                return null;

            if (name == "None" )
                return new UENoneProperty { Name = name };
            var type = stringPool.GetString(reader.ReadInt32() - 1);
            var valueLength = reader.ReadInt32();
            return UESerializer.Deserialize(name, type, valueLength, reader, stringPool);
            
        }

        public static UEProperty[] Read(BinaryReader reader, int count, StringPool stringPool)
        {
            if (reader.PeekChar() < 0)
                return null;
            
            var name = stringPool.GetString(reader.ReadInt32() - 1);
            var type = stringPool.GetString(reader.ReadInt32() - 1);
            var valueLength = reader.ReadInt64();
            return UESerializer.Deserialize(name, type, valueLength, count, reader, stringPool);
        }
    }
}