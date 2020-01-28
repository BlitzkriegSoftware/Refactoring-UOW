using System;
using System.IO;

namespace UoW.Demo.Utility
{
    public static class FileUtility
    {
        public const int FilesCount = 20;

        public static void MakeFiles(string inFolder, int filesCount)
        {
            for (int i = 0; i < filesCount; i++)
            {
                var filename = CreateFile(inFolder);
                Console.WriteLine($"Created: {filename}");
            }
        }

        public static string CreateFile(string inFolder)
        {
            var filename = Guid.NewGuid().ToString();
            filename = Path.ChangeExtension(filename, ".txt");
            filename = Path.Combine(inFolder, filename);

            var text = Faker.Lorem.Paragraph(3);

            File.WriteAllText(filename, text);

            return filename;
        }


    }
}
