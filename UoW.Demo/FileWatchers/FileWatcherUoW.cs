using System;
using System.IO;
using System.Linq;

namespace UoW.Demo.FileWatchers
{
    public static class FileWatcherUoW
    {

        public static Utility.ObservableQueue<string> MyQueue = new Utility.ObservableQueue<string>();

        public static void Run(string inFolder)
        {

            MyQueue.CollectionChanged += MyQueue_CollectionChanged;

            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = inFolder;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                Utility.FileUtility.MakeFiles(inFolder, Utility.FileUtility.FilesCount);

                int fc = 0;
                do
                {
                    fc = Directory.EnumerateFiles(inFolder).Count();
                } while (fc > 0);
            }
        }

        private static void MyQueue_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (MyQueue.Count > 0)
            {
                var filename = MyQueue.Dequeue();
                Console.WriteLine($"Dequeue: {filename}");
                File.Delete(filename);
            }
        }


        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"Enqueue: {e.FullPath}");
            MyQueue.Enqueue(e.FullPath);
        }

    }
}
