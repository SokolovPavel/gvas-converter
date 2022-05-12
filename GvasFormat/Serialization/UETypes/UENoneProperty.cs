using System;
using System.Diagnostics;
using System.IO;
using GvasFormat.Utils;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("", Name = "{Name}")]
    public sealed class UENoneProperty : UEProperty
    {
        public override void Serialize(BinaryWriter writer) { throw new System.NotImplementedException(); }
    }
}