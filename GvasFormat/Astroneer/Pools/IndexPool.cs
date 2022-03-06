using System.IO;

namespace GvasFormat.Serialization
{
    public class IndexPool
    {
        public readonly int[] Indexes;

        public IndexPool(BinaryReader reader)
        {
            var size = reader.ReadInt32();
            Indexes = new int[size];
            for (var i = 0; i < size; i++)
            {
                Indexes[i] = reader.ReadInt32();
            }
        }
    }
}