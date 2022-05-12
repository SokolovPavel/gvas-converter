using System;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    public class MissionVersion: UEProperty
    {
        public MissionVersion(BinaryReader reader, long propertyLength, StringPool pool)
        {
            byte[] array = reader.ReadBytes(100);
            Console.WriteLine("sdf");
        }

        public override void Serialize(BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}