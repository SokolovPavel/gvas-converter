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
        private readonly static byte ExtendedHeader = 2;
        public AstroneerObject read(BinaryReader reader)
        {
            AstroneerObject astroObject = new AstroneerObject();
            astroObject.ClassName =  reader.ReadUEString();
            astroObject.InnerIndex = reader.ReadInt32();
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

            /*long nextObjectPosition = reader.BaseStream.Position;
            string possibleNextString = reader.ReadUEString();
            if (possibleNextString == null || !possibleNextString.StartsWith("/"))
            {
                reader.BaseStream.Position = position;
                astroObject.HeaderPostfix = reader.ReadBytes(4);
                size = reader.ReadInt32();
                astroObject.Body = reader.ReadBytes(size);
            }
            else
            {
                reader.BaseStream.Position = nextObjectPosition;
            }*/
            
            return astroObject;
        }
    }
}