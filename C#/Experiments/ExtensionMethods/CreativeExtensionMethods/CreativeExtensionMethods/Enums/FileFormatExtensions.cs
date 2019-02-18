using System;

namespace CreativeExtensionMethods.Enums
{
    public enum FileFormat
    {
        PlainText,
        OfficeWord,
        Markdown
    }

    /// <summary>
    /// Adding methods to enums
    /// </summary>
    public static class FileFormatExtensions
    {
        public static string GetFileExtensions(this FileFormat fileFormat)
        {
            if (fileFormat == FileFormat.PlainText)
                return "txt";

            if (fileFormat == FileFormat.OfficeWord)
                return "docx";

            if (fileFormat == FileFormat.Markdown)
                return "md";

            // thrown when we add a new file format but forget to add
            // corresponding file extension
            throw new ArgumentNullException(nameof(fileFormat));
        }
    }
}