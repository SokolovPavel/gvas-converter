using System.IO;

namespace GvasFormat.Serialization.Factories
{
    public interface SaveGameReader
    {
        object Read(BinaryReader reader);
    }
}