using System;
using System.IO;
using CommandLine;
using UoW.Demo.Models;

namespace UoW.Demo
{
    class Program
    {

        static void Main(string[] args)
        {
            string inFolder = "in";
            string outFolder = "out";


            string rootFolder = string.Empty;

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       rootFolder = o.RootFolder;
                   });

            if(!Directory.Exists(rootFolder))
            {
                Directory.CreateDirectory(rootFolder);
            }

            inFolder = Path.Combine(rootFolder, inFolder);
            if (!Directory.Exists(inFolder)) Directory.CreateDirectory(inFolder);

            FileWatchers.FileWatchIt.Run(inFolder);

            FileWatchers.FileWatcherUoW.Run(inFolder);

            FileRoller.FileRollerIt.Run(inFolder);

            FileRoller.FileRollerUoW.Run(inFolder);

            if (Directory.Exists(inFolder)) Directory.Delete(inFolder,true);
        }

        

    }
}
