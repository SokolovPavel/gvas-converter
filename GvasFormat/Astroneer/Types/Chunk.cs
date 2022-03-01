namespace GvasFormat.Serialization
{
    public class Chunk
    {
        public int index;
        public readonly int[][] Array1;
        public readonly int[][] Array2;
        public readonly Vector4 Normal;
        public readonly Vector3 Position;
        public readonly Vector3 Unknown;

        public Chunk(int index, int[][] array1, int[][] array2, Vector4 normal, Vector3 position, Vector3 unknown)
        {
            this.index = index;
            Array1 = array1;
            Array2 = array2;
            Normal = normal;
            Position = position;
            Unknown = unknown;
        }
    }
}