using System.IO;

namespace GvasFormat.Serialization
{
    /*
     * string array with all object types
    */
    public class StringPool
    {
        public readonly string[] _stringPool;
        public StringPool(BinaryReader reader)
        {
            int magicInt = reader.ReadInt32();//44? why? is that 2nd dimension size?
            long poolSize = reader.ReadInt64();
            _stringPool = new string[(int)poolSize];
            for (int i = 0; i < (poolSize - 1); i++)//less by one
            {
                _stringPool[i] = reader.ReadUEString();
            }
        }

        public string GetString(int index)
        {
            return _stringPool[index];
        }
    }
}