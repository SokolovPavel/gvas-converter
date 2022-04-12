using System;
using System.IO;
using GvasFormat.Serialization;

public class Terrain2
{
    public class VoxelVolumeComponent : AstroneerObject
    {
        public string type;
        public ByteArrayTail[] stringBytesArray;
        public string[] lastArray;
        public Vector3Bytes2[] floatArray;
        public int int1;
        public int int2;

        public override void SetBody(BinaryReader reader, int size, StringPool stringPool)
        {
            long startPosition = reader.BaseStream.Position;
            int typeIndex = reader.ReadInt32();
            type = stringPool.GetString(typeIndex);
            int zero = reader.ReadInt32();
            int1 = reader.ReadInt32();
            int2 = reader.ReadInt32();
            int byteArraySize = reader.ReadInt32();
            if (byteArraySize > 0)
            {
                stringBytesArray = new ByteArrayTail[byteArraySize];
                for (int i = 0; i < byteArraySize; i++)
                {
                    byte[] t = reader.ReadBytes(8);
                    int subArraySize = reader.ReadInt32();
                    stringBytesArray[i] = new ByteArrayTail(
                        BitConverter.ToString(t).Replace('-', ' '),
                        BitConverter.ToString(reader.ReadBytes(subArraySize)).Replace('-', ' '),
                        subArraySize);
                }
            }

            int lastArraySize = reader.ReadInt32();
            if (lastArraySize > 0)
            {
                lastArray = new string[lastArraySize];
                for (int i = 0; i < lastArraySize; i++)
                {
                    lastArray[i] = BitConverter.ToString(reader.ReadBytes(8)).Replace('-', ' ');
                }
            }

            int floatArraySize = reader.ReadInt32();
            if (floatArraySize > 0)
            {
                floatArray = new Vector3Bytes2[floatArraySize];
                for (int i = 0; i < floatArraySize; i++)
                {
                    floatArray[i] = new Vector3Bytes2(reader.ReadSingle(),
                        reader.ReadSingle(),
                        reader.ReadSingle(),
                        reader.ReadSingle(),
                        reader.ReadByte(),
                        reader.ReadByte());
                }
            }

            int restSize = size - (int) (reader.BaseStream.Position - startPosition);
            byte[] body = reader.ReadBytes(restSize);
            BodyString = BitConverter.ToString(body).Replace('-', ' ');
        }

        public class ByteArrayTail
        {
            public int size;
            public string s;
            public string t;

            public ByteArrayTail(string t, string s, int size)
            {
                this.t = t;
                this.s = s;
                this.size = size;
            }
        }

        public class ByteArray
        {
            public int size;
            public string s;

            public ByteArray(string s, int size)
            {
                this.s = s;
                this.size = size;
            }
        }

        public class Vector3Bytes2
        {
            public float X;
            public float Y;
            public float Z;
            public float W;
            public byte byte1;
            public byte byte2;

            public Vector3Bytes2(float x, float y, float z, float w, byte byte1, byte byte2)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
                this.byte1 = byte1;
                this.byte2 = byte2;
            }
        }
    }
}