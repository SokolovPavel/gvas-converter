using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    public class AstroDatumRef:UEStructProperty
    {
        private int a, b, c;
        public AstroDatumRef(BinaryReader reader)
        {
            a = reader.ReadInt32();
            b = reader.ReadInt32();
            c = reader.ReadInt32();
        }
        public override void Serialize(BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}