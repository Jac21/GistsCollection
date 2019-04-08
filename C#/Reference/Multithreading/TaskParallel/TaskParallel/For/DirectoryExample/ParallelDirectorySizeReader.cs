using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallel.For.DirectoryExample
{
    public class ParallelDirectorySizeReader
    {
        public string DirectoryToRead { get; set; }

        public void Process()
        {
            long totalSize = 0;

            if (!Directory.Exists(DirectoryToRead))
            {
                Console.WriteLine("The directory does not exist!");
                return;
            }

            string[] files = Directory.GetFiles(DirectoryToRead);

            Parallel.For(0, files.Length,
                index =>
                {
                    FileInfo fi = new FileInfo(files[index]);
                    long size = fi.Length;
                    Interlocked.Add(ref totalSize, size);
                });


            Console.WriteLine($"Directory '{DirectoryToRead}':");
            Console.WriteLine($"{files.Length} files, {totalSize} bytes");
        }
    }
}