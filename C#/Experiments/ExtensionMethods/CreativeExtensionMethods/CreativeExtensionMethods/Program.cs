using System;
using System.Collections.Generic;
using System.Linq;
using CreativeExtensionMethods.Enums;
using CreativeExtensionMethods.Interfaces;
using CreativeExtensionMethods.Models;
using CreativeExtensionMethods.Enumerables;
using CreativeExtensionMethods.Strings;

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

            var closedCaptionTracks = new ClosedCaptionTrack("en-US", new List<ClosedCaption>
            {
                new ClosedCaption("First Caption", TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new ClosedCaption("Second Caption", TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2)),
                new ClosedCaption("Third Caption", TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))
            });

            Console.WriteLine(closedCaptionTracks.GetByTime(TimeSpan.FromMinutes(1)).Text);

            Console.WriteLine("--------- Interfaces ----------");

            var exportService = new ExportService();

            var model = new Model
            {
                Name = "Model One"
            };

            exportService.SaveToMemeory(model);

            Console.WriteLine("--------- Enumerables ----------");

            List<Model> models = new List<Model>
            {
                new Model
                {
                    Name = string.Empty
                }
            };

            models.ForEachInList(t => t.Name = "New Model");

            Console.WriteLine(models.FirstOrDefault()?.Name);

            Console.WriteLine("--------- Strings ----------");

            Console.WriteLine($"Is 192.168.0.0 a valid IP address? {"192.168.0.0".IsValidIpAddress()}");

            Console.ReadLine();
        }
    }
}