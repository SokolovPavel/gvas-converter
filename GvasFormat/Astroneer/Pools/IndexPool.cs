using System.IO;

namespace GvasFormat.Serialization
{
    public class IndexPool
    {
        public int[] indexes;

        public IndexPool(BinaryReader reader)
        {
            int size = reader.ReadInt32();
            indexes = new int[size];
            for (int i = 0; i < size; i++)
            {
                indexes[i] = reader.ReadInt32();
            }
        }
    }
}