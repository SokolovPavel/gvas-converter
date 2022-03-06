using Newtonsoft.Json;

namespace GvasFormat.Serialization
{
    public class AstroneerData
    {
        [JsonIgnore]
        public StringPool StringPool;
        [JsonProperty(Order = 0)]
        public InstancePool InstancePool;
        [JsonProperty(Order = 1)]
        public ChunkPool ChunkPool;
        [JsonProperty(Order = 2)]
        public IndexPool IndexPool { get; set; }
        [JsonProperty(Order = 3)]
        public byte[] RestBytes;
    }
}