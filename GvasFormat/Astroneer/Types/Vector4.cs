using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization
{
    [DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}, W = {W}", Name = "{Name}")]
    public class Vector4
    {
        public Vector4(BinaryReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
            W = reader.ReadSingle();
        }

        public float X, Y, Z, W;
    }
}