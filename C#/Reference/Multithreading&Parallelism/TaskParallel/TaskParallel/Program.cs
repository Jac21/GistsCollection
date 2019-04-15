using System;
using System.Collections.Generic;
using System.Diagnostics;
using TaskParallel.ForEach.ScannerExample;
using TaskParallel.For.DirectoryExample;

namespace TaskParallel
{
    public class Program
    {
        static void Main()
        {
            // Parallel.For example
            Console.WriteLine("------ Parallel.For ------");

            ParallelDirectorySizeReader parallelDirectorySizeReader =
                new ParallelDirectorySizeReader();

            parallelDirectorySizeReader.DirectoryToRead = "C:\\Users\\jcant\\Documents\\Books";

            parallelDirectorySizeReader.Process();

            // Parallel.ForEach example
            Console.WriteLine("------ Parallel.ForEach ------");

            var images = new List<Image>
            {
                new Image
                {
                    Size = 1
                },
                new Image
                {
                    Size = 2
                },
                new Image
                {
                    Size = 4
                },
                new Image
                {
                    Size = 8
                },
                new Image
                {
                    Size = 16
                }
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ParallelScanner.Run(images);

            stopwatch.Stop();
            Console.WriteLine($"Imperative Scanner results call (in seconds): {stopwatch.Elapsed.TotalSeconds}");

            stopwatch.Reset();
            stopwatch.Start();

            ParallelScanner.RunParallel(images);

            stopwatch.Stop();
            Console.WriteLine($"Parallel Scanner results call (in seconds): {stopwatch.Elapsed.TotalSeconds}");

            // fin
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }
    }
}