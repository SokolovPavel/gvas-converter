using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GvasFormat.Serialization.UETypes;

namespace GvasFormat.Serialization
{
    [DebuggerDisplay("class: {ClassName.Name}, body size:{_body.Length}", Name = "{InstanceName}")]
    public class AstroneerObject
    {
        public UEClassName ClassName;
        public string InstanceName { get; set; }
        public byte[] _body;
        public string BodyString;
        public int InnerIndex { get; set; }
        public byte[] preHeader;
        public byte FlagByte;
        public uint parentIndex;
        public byte[] HeaderPostfix { get; set; }
        public List<AstroneerObject> Components;
        public bool propertyFailed = false;
        public List<UEProperty> Properties { get; set; }

        public bool HasFlag(byte bitIndex)
        {
            return (FlagByte & 1 << bitIndex) > 0;
        }

        public virtual void SetBody(BinaryReader reader, int size, StringPool stringPool)
        {
            _body = reader.ReadBytes(size);
            BodyString = BitConverter.ToString(_body).Replace('-', ' ');
        }

        public bool HasParent()
        {
            return parentIndex != UInt32.MaxValue && parentIndex != 0;
        }

        public void AddComponent(AstroneerObject entity)
        {
            if (Components == null)
            {
                Components = new List<AstroneerObject>();
            }

            Components.Add(entity);
        }

        public void AddProperty(UEProperty property)
        {
            if (Properties == null)
            {
                Properties = new List<UEProperty>();
            }

            Properties.Add(property);
        }
    }
}