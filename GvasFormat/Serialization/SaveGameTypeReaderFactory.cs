using System;
using GvasFormat.Serialization.Exceptions;
using GvasFormat.Serialization.Factories;

namespace GvasFormat.Serialization
{
    public class SaveGameTypeReaderFactory
    {
        public static SaveGameReader GetReader(String saveGameType)
        {
            switch (saveGameType)
            {
                case "AstroSave":
                    return new AstroneerSaveGameReader();
                default:
                    throw new UnsupportedSaveGameTypeException();
            }
        }
    }
}