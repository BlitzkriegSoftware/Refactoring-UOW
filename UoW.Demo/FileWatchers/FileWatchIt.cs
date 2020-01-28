using System;
using System.Linq;
using System.IO;
using System.Threading;

namespace UoW.Demo.FileWatchers
{
    public class FileWatchIt
    {

        public static void Run(string inFolder)
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = inFolder;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.Attributes;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Changed += OnChanged;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                Utility.FileUtility.MakeFiles(inFolder, Utility.FileUtility.FilesCount);

                Thread.Sleep(1000);

                int fc = 0;
                do
                {
                    fc = Directory.EnumerateFiles(inFolder).Count();
                    if(fc > 0) { 
                    var fil2 = Directory.GetFiles(inFolder).FirstOrDefault();
                        if(!string.IsNullOrWhiteSpace(fil2))
                        {
                            File.SetLastAccessTime(fil2, DateTime.Now);
                        }
                    }
                } while (fc > 0);

            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"OnChanged: {e.FullPath}");
            File.Delete(e.FullPath);
        }

    }
}
