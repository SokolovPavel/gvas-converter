using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace UESavefileRecompressor
{
    public static class Recompressor
    {
        private const string HeaderFixedHex = "BE 40 37 4A EE 0B 74 A3 01 00 00 00 ";
        private static readonly byte[] HeaderFixedBytes = StringToByteArray(HeaderFixedHex);
        private static readonly int HeaderFixedLen = HeaderFixedBytes.Length;
        private const string _headerGvasMagic = "GVAS";
        private const string _compressedExt = "savegame";
        private const string _extractedExt = "gvas";

        public static Stream Decompress(Stream input)
        {
            var header = new byte[12];
            input.Read(header, 0, 12);
            byte[] buffer = new byte[4];
            input.Read(buffer, 0, 4);
            int size = BitConverter.ToInt32(buffer, 0);
            if (!HeaderFixedBytes.SequenceEqual(header))
            {
                throw new ArgumentException(
                    "Header bytes do not match: Expected "+HeaderFixedHex + " got " + BitConverter.ToString(header).Replace('-', ' '));
            }

            return new ZLibStream(input, CompressionMode.Decompress);
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 3 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}