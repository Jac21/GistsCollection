using System;
using System.IO;

namespace CreativeExtensionMethods.Interfaces
{
    public class Model
    {
        public string Name { get; set; }
    }

    public interface IExportService
    {
        void Save(Model model, Stream output);
    }

    public class ExportService : IExportService
    {
        public void Save(Model model, Stream output)
        {
            Console.WriteLine($"Saving model {model.Name} with output length {output.Length}");
        }
    }

    /// <summary>
    /// Making interfaces more versatile
    /// </summary>
    public static class ExportServiceExtensions
    {
        public static FileInfo SaveToFile(this IExportService exportService, Model model, string filePath)
        {
            using (var output = File.Create(filePath))
            {
                exportService.Save(model, output);
                return new FileInfo(filePath);
            }
        }

        public static byte[] SaveToMemeory(this IExportService exportService, Model model)
        {
            using (var output = new MemoryStream())
            {
                exportService.Save(model, output);
                return output.ToArray();
            }
        }
    }
}