using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GvasFormat.Serialization
{
    public class InstancePool
    {
        public readonly AstroneerObject[] _objectPool;

        public InstancePool(StringPool stringPool, BinaryReader reader)
        {
            int size = reader.ReadInt32(); //pool size
            _objectPool = new AstroneerObject[size];
            AstroneerObjectReader objectReader = new AstroneerObjectReader(stringPool);

            Dictionary<string, int> map = new Dictionary<string, int>();
            for (int i = 0; i < size; i++)
            {
                var entity = objectReader.read(reader);
                _objectPool[i] = entity;
                if (entity.HasParent())
                {
                    _objectPool[entity.parentIndex].AddComponent(entity);
                }
            }

            for (int i = 0; i < size; i++)
            {
                
            }
        }
    }
}