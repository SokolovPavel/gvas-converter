using System.IO;
using System.Text;

namespace GvasFormat.Serialization
{
    public static class BinaryReaderEx
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false);

        public static string ReadUEString(this BinaryReader reader)
        {
            if (reader.PeekChar() < 0)
                return null;

            var length = reader.ReadInt32();
            if (length == 0)
                return null;

            if (length == 1)
                return "";

            var valueBytes = reader.ReadBytes(length);
            return Utf8.GetString(valueBytes, 0, valueBytes.Length - 1);
        }

        public static string ReadUEString(this BinaryReader reader, long length)
        {
            var valueBytes = reader.ReadBytes((int)length);
            return Utf8.GetString(valueBytes, 0, valueBytes.Length - 1);
        }

        public static void WriteUEString(this BinaryWriter writer, string value)
        {
            if (value == null)
            {
                writer.Write(0);
                return;
            }

            var valueBytes = Utf8.GetBytes(value);
            writer.Write(valueBytes.Length + 1);
            if (valueBytes.Length > 0)
                writer.Write(valueBytes);
            writer.Write((byte) 0);
        }

        public static byte[] ReadRestBytes(this BinaryReader reader)
        {
            const int bufferSize = 4096;
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                    ms.Write(buffer, 0, count);
                }

                return ms.ToArray();
            }
        }
    }
}