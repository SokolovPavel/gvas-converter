// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace GvasConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Searching savegames");
            string userFilesPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%");
            string pathToSaveFiles = "\\AppData\\Local\\Astro\\Saved\\SaveGames\\";
            var directory = new DirectoryInfo(userFilesPath + pathToSaveFiles);
            if (!directory.Exists)
            {
                Console.WriteLine("Savegame directory does not exist");
                return;
            }
            DateTime lastModifiedDate = DateTime.MinValue;
            while (true)
            {
                var savegameFile = directory.GetFiles()
                    .Where(s => ".savegame".Equals(s.Extension) && lastModifiedDate < s.LastWriteTime)
                    .OrderByDescending(f => f.LastWriteTime)
                    .FirstOrDefault();
                if (savegameFile != null)
                {
                    lastModifiedDate = savegameFile.LastWriteTime;
                    Console.WriteLine("Found last savegame " + savegameFile.Name);
                    Process.Start("..\\..\\..\\..\\GvasConverter\\bin\\Debug\\net6.0\\GvasConverter.exe",
                        savegameFile.FullName);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}