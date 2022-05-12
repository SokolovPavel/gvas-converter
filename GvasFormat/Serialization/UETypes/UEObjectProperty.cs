using System.IO;
using GvasFormat.Serialization.UETypes;

public class UEObjectProperty : UEProperty
{
    public byte[] objectIndex;
    public UEObjectProperty(BinaryReader reader, long valueLength)
    {
        if (valueLength > 0)
        {
            byte[] zeros = reader.ReadBytes(4 + 1);
        }
        objectIndex = reader.ReadBytes((int)4);
    }

    public override void Serialize(BinaryWriter writer)
    {
        throw new System.NotImplementedException();
    }
}