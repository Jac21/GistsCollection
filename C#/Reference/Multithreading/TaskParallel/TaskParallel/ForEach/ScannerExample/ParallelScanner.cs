using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallel.ForEach.ScannerExample
{
    public class ParallelScanner
    {
        /// <summary>
        /// Imperative Run method
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public static List<CodedImage> Run(List<Image> images)
        {
            var codedImages = new List<CodedImage>();

            foreach (var image in images)
            {
                // simulate operation that might take 4-5 seconds per image
                byte[] imageData = ReadImageData(image);

                codedImages.Add(new CodedImage(imageData, image));
            }

            return codedImages;
        }

        /// <summary>
        /// Parallel version of the Run method, using Task Parallel ForEach method
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public static List<CodedImage> RunParallel(List<Image> images)
        {
            var codedImages = new ConcurrentBag<CodedImage>();

            Parallel.ForEach(images, image =>
            {
                // simulate operation that might take 4-5 seconds per image
                byte[] imageData = ReadImageData(image);

                codedImages.Add(new CodedImage(imageData, image));
            });

            return codedImages.ToList();
        }

        /// <summary>
        /// Simulates an operation that might take 4-5 seconds
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static byte[] ReadImageData(Image image)
        {
            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 5)));

            return new byte[image.Size];
        }
    }
}