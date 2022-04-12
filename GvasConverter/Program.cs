using System;
using System.IO;
using System.Text;
using GvasFormat;
using GvasFormat.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UESavefileRecompressor;

namespace GvasConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("  gvas-converter path_to_save_file|path_to_json");
                return;
            }

            var ext = Path.GetExtension(args[0]).ToLower();
            string filename = Path.GetFileNameWithoutExtension(args[0]);
            string path = Path.GetDirectoryName(args[0]);
            if (ext == ".json")
            {
                Console.WriteLine("Not implemented atm");
            }
            else
            {
                if (ext == ".savegame")
                {
                    Console.WriteLine("Decompressing savefile...");
                    using var zipStream = File.Open(args[0], FileMode.Open, FileAccess.Read, FileShare.Read);
                    var decompressedStream = Recompressor.Decompress(zipStream);
                    BinaryReader reader = new BinaryReader(decompressedStream);
                    var readRestBytes = reader.ReadRestBytes();
                    File.WriteAllBytes(path  + "\\" + filename + ".gvas", readRestBytes);
                }
                Console.WriteLine("Parsing UE4 save file structure...");
                
                using var stream = File.Open(path + "\\" + filename + ".gvas", FileMode.Open, FileAccess.Read, FileShare.Read);
                Gvas save;
                save = UESerializer.Read(stream);
                
                Console.WriteLine("Converting to json...");
                var json = JsonConvert.SerializeObject(save,
                    new JsonSerializerSettings {Formatting = Formatting.Indented});

                Console.WriteLine("Saving json...");
                FileStream outStream;
                using (outStream = File.Open(path + "\\" + filename + ".json", FileMode.Create, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(outStream, new UTF8Encoding(false)))
                    writer.Write(json);
            }

            Console.WriteLine("Done.");
        }
    }
}