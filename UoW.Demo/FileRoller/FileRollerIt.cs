using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace UoW.Demo.FileRoller
{
    public static class FileRollerIt
    {
        public static void Run(string inFolder)
        {
            Utility.FileUtility.MakeFiles(inFolder, Utility.FileUtility.FilesCount);

            foreach(var f in Directory.EnumerateFiles(inFolder))
            {
                Console.WriteLine($"Delete: {f}");
                File.Delete(f);
            }
        }

    }
}
