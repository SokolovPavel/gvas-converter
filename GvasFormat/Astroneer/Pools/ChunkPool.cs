using System;
using System.IO;
using System.Text;

namespace GvasFormat.Serialization
{
    public class ChunkPool
    {
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
            int size = reader.ReadInt32();
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