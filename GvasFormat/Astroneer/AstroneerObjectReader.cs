using System.IO;
using GvasFormat.Serialization.Factories;
using GvasFormat.Serialization.UETypes;

namespace GvasFormat.Serialization
{
    /*
     * Structure:
     * class name(UEString) length(int32) + string + /0
     * header 13 or 17 bytes
     * rest data size
     * data
     */

    public class AstroneerObjectReader
    {
        private readonly StringPool _stringPool;
        private readonly AstroObjectFactory _objectFactory;
        private readonly static byte ExtendedHeader = 2;

        public AstroneerObjectReader(StringPool stringPool)
        {
            _stringPool = stringPool;
            _objectFactory = new AstroObjectFactory(stringPool);
        }

        public AstroneerObject read(BinaryReader reader)
        {
            UEClassName ClassName = new UEClassName(reader.ReadUEString());
            AstroneerObject astroObject = _objectFactory.getObject(ClassName);
            astroObject.InstanceName = _stringPool.GetString(reader.ReadInt32() - 1);
            astroObject.preHeader = reader.ReadBytes(4);
            /*
             * bits:
             * 0 - unknown
             * 1- unknown
             * 2 - possible extended header
             */
            astroObject.FlagByte = reader.ReadByte(); //possible flags 
            astroObject.indexOrAddress = reader.ReadBytes(4); //possible offset or index of some object

            if (astroObject.HasFlag(ExtendedHeader))
            {
                astroObject.HeaderPostfix = reader.ReadBytes(4);
            }

            long position = reader.BaseStream.Position;
            int size = reader.ReadInt32();
            astroObject.SetBody(reader, size, _stringPool);

            return astroObject;
        }
    }
}