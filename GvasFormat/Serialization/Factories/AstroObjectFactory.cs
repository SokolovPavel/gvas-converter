using GvasFormat.Serialization.UETypes;

namespace GvasFormat.Serialization.Factories
{
    public class AstroObjectFactory
    {
        private readonly StringPool _stringPool;

        public AstroObjectFactory(StringPool stringPool)
        {
            _stringPool = stringPool;
        }

        public AstroneerObject getObject(UEClassName className)
        {
            AstroneerObject astroObject;
            switch (className.Name)
            {
                case "VoxelVolumeComponent":
                    astroObject = new Terrain2.VoxelVolumeComponent();
                    break;
                default:
                    astroObject = new AstroneerObject();
                    break;
            }

            astroObject.ClassName = className;
            return astroObject;
        }
    }
}