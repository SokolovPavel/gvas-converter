using System.IO;

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
        
        private readonly static byte ExtendedHeader = 2;

        public AstroneerObjectReader(StringPool stringPool)
        {
            _stringPool = stringPool;
        }

        public AstroneerObject read(BinaryReader reader)
        {
            AstroneerObject astroObject = new AstroneerObject();
            astroObject.ClassName =  reader.ReadUEString();
            astroObject.InstanceName = _stringPool.GetString(reader.ReadInt32());
            astroObject.preHeader = reader.ReadBytes(4);
            /*
             * bits:
             * 0 - unknown
             * 1- unknown
             * 2 - possible extended header
             */
            astroObject.FlagByte = reader.ReadByte();//possible flags 
            astroObject.indexOrAddress = reader.ReadBytes(4);//possible offset or index of some object

            if (astroObject.HasFlag(ExtendedHeader))
            {
                astroObject.HeaderPostfix = reader.ReadBytes(4);
            }
            long position = reader.BaseStream.Position;
            int size = reader.ReadInt32();
            astroObject.Body = reader.ReadBytes(size);
            
            return astroObject;
        }
    }
}