using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace UoW.Demo.FileRoller
{
    public static class FileRollerUoW
    {
        public static Utility.ObservableQueue<string> MyQueue = new Utility.ObservableQueue<string>();

        public static void Run(string inFolder)
        {
            Utility.FileUtility.MakeFiles(inFolder, Utility.FileUtility.FilesCount);

            MyQueue.CollectionChanged += MyQueue_CollectionChanged;

            var files = Directory.GetFiles(inFolder);

            for(int i=0; i<files.Length; i++)
            {
                MyQueue.Enqueue(files[i]);
            }

            Thread.Sleep(1000);

            int fc = 0;
            do
            {
                fc = Directory.EnumerateFiles(inFolder).Count();
            } while (fc > 0);

        }

        private static void MyQueue_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (MyQueue.Count > 0)
            {
                var filename = MyQueue.Dequeue();
                Console.WriteLine($"Dequeue2: {filename}");
                File.Delete(filename);
            }
        }
    }
}
