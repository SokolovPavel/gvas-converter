using System;
using System.IO;
using System.Text;

namespace GvasFormat.Serialization
{
    public class ChunkPool
    {
        public int size;

        public Chunk[] chunks;

        /*
         * possible chunk?
         * int index
         * int first pair array size 
         * pairs [size](int, int)
         * int second pair array size
         * pairs [size](int, int)
         * vector4 float value (normal?)
         * vector3 float value (x,y,z?)
         * vector3 float value
         */
        public ChunkPool(BinaryReader reader)
        {
            size = reader.ReadInt32();
            chunks = new Chunk[size];
            for (int i = 0; i < size; i++)
            {
                int index = reader.ReadInt32();
                int[][] array1 = ReadPairArray(reader);
                int[][] array2 = ReadPairArray(reader);
                Vector4 normal = new Vector4(reader);
                Vector3 position = new Vector3(reader);
                Vector3 unknown = new Vector3(reader);
                chunks[i] = new Chunk(index, array1, array2, normal, position, unknown);
            }
/*
            using (var outputStream = File.Open("restBytesEx.txt", FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (var writer = new StreamWriter(
                           outputStream,
                           Encoding.ASCII))
                {
                    bool isPrevFloat = false;
                    bool isCurrentFloat = false;
                    for (int i = 0; i < size * 12 * 2; i++)
                    {
                        int intValue = reader.ReadInt32();
                        if (intValue > 10000 || intValue < -10000)
                        {
                            isPrevFloat = true;
                            reader.BaseStream.Position -= 4;
                            float floatValue = reader.ReadSingle();
                            writer.Write(String.Format("{0:0.00}", floatValue).PadLeft(10) + " ");
                        }
                        else
                        {
                            if (isPrevFloat)
                            {
                                writer.WriteLine();
                            }

                            isPrevFloat = false;
                            writer.Write(String.Format("{0:0}", intValue).PadLeft(10) + " ");
                        }
                    }
                }
            }*/
        }

        private static int[][] ReadPairArray(BinaryReader reader)
        {
            int arraySize = reader.ReadInt32();
            if (arraySize > 0)
            {
                int[][] array = new int[arraySize][];
                for (int j = 0; j < arraySize; j++)
                {
                    int[] pair = new int[2];
                    pair[0] = reader.ReadInt32();
                    pair[1] = reader.ReadInt32();
                    array[j] = pair;
                }

                return array;
            }
            else
            {
                return null;
            }
        }
    }
}