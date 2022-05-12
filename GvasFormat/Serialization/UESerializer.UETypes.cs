using System;
using System.IO;
using GvasFormat.Serialization.Exceptions;
using GvasFormat.Serialization.UETypes;

namespace GvasFormat.Serialization
{
    public static partial class UESerializer
    {
        internal static UEProperty Deserialize(string name, string type, long valueLength, BinaryReader reader, StringPool stringPool)
        {
            UEProperty result;
            var itemOffset = reader.BaseStream.Position;
            switch (type)
            {
                case "BoolProperty":
                    result = new UEBoolProperty(reader, valueLength);
                    break;
                case "UInt32Property":
                    result = new UEUIntProperty(reader, valueLength);
                    break;
                case "IntProperty":
                    result = new UEIntProperty(reader, valueLength);
                    break;
                case "UInt64Property":
                    result = new UEUInt64Property(reader, valueLength);
                    break;
                case "FloatProperty":
                    result = new UEFloatProperty(reader, valueLength);
                    break;
                case "NameProperty":
                case "StrProperty":
                    result = new UEStringProperty(reader, valueLength, stringPool);
                    break;
                case "TextProperty":
                    result = new UETextProperty(reader, valueLength);
                    break;
                case "EnumProperty":
                    result = new UEEnumProperty(reader, valueLength, stringPool);
                    break;
                case "StructProperty":
                    result = UEStructProperty.Read(reader, valueLength, stringPool);
                    break;
                case "ArrayProperty":
                    result = new UEArrayProperty(reader, valueLength, stringPool);
                    break;
                case "MapProperty":
                    result = new UEMapProperty(reader, valueLength, stringPool);
                    break;
                case "ByteProperty":
                    result = UEByteProperty.Read(reader, valueLength);
                    break;
                case "EntityRef":
                    result = new UEEntityRefProperty(reader, valueLength);
                    break;
                case "ObjectProperty":
                    result = new UEObjectProperty(reader, valueLength);
                    break;
                case "MissionVersion":
                    result = new MissionVersion(reader, valueLength, stringPool);
                    break;
                default:
                    throw new FormatException($"Offset: 0x{itemOffset:x8}. Unknown value type '{type}' of item '{name}'");
            }
            result.Name = name;
            result.Type = type;
            return result;
        }

        internal static UEProperty[] Deserialize(string name, string type, long valueLength, int count, BinaryReader reader, StringPool stringPool)
        {
            UEProperty[] result;
            switch (type)
            {
                case "StructProperty":
                    result = UEStructProperty.Read(reader, valueLength, count, stringPool);
                    break;
                case "ByteProperty":
                    result = UEByteProperty.Read(reader, valueLength, count);
                    break;
                default:
                    throw new FormatException($"Unknown value type '{type}' of item '{name}'");
            }
            foreach (var item in result)
            {
                item.Name = name;
                item.Type = type;
            }
            return result;
        }
    }
}