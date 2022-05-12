using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEEntityRefProperty : UEProperty
    {
        public string bytes;
        public UEEntityRefProperty(BinaryReader reader, long valueLength)
        {
            byte[] restBytes = reader.ReadBytes(4); 
            bytes = System.BitConverter.ToString(reader.ReadBytes((int)valueLength)).Replace('-', ' ');
        }
        public override void Serialize(BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}