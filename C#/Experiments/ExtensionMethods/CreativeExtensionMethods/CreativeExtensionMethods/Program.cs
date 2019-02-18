using System;
using System.Collections.Generic;
using System.IO;
using CreativeExtensionMethods.Enums;
using CreativeExtensionMethods.Interfaces;
using CreativeExtensionMethods.Models;

namespace CreativeExtensionMethods
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("--------- Enums ----------");

            var format = FileFormat.Markdown;
            var fileExt = format.GetFileExtensions(); // "md"
            Console.WriteLine($"output.{fileExt}");

            Console.WriteLine("--------- Models ----------");

            var closedCaptionTrack = new ClosedCaptionTrack("en-US", new List<ClosedCaption>
            {
                new ClosedCaption("First Caption", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new ClosedCaption("Second Caption", TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2)),
                new ClosedCaption("Third Caption", TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))
            });

            Console.WriteLine(closedCaptionTrack.GetByTime(TimeSpan.FromMinutes(1)).Text);

            Console.WriteLine("--------- Interfaces ----------");

            var exportService = new ExportService();

            var model = new Model
            {
                Name = "Model One"
            };

            // exportService.SaveToFile(model, Directory.GetCurrentDirectory());
            exportService.SaveToMemeory(model);

            Console.ReadLine();
        }
    }
}