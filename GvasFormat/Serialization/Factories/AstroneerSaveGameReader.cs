using System.IO;
using GvasFormat.Serialization.UETypes;

namespace GvasFormat.Serialization.Factories
{
    public class AstroneerSaveGameReader : SaveGameReader
    {
        public object Read(BinaryReader reader)
        {
            AstroneerData data = new AstroneerData();
            string none = reader.ReadUEString();
            int zero = reader.ReadInt32();//zero for some reason
            int magicNumber1 = reader.ReadInt32();
            string magicString1 = reader.ReadUEString();
            
            data.stringPool = new StringPool(reader);
            data.InstancePool = new InstancePool(reader);
            data.ChunkPool = new ChunkPool(reader);
            data.IndexPool = new IndexPool(reader);
            data.RestBytes = reader.ReadRestBytes();
            return data;
        }
    }
}