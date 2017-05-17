using System;
using System.IO;

namespace IoOperations.FileStructureObjects
{
    public class Files
    {
        // class fields
        public string _folder;
        public string _fileName;
        public string _fullPath;

        /// <summary>
        /// Class constructor, intakes folder and file name string
        /// parameters, combines them to form full path
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        public Files(string folder, string fileName)
        {
            this._folder = folder;
            this._fileName = fileName;

            // results in combined path from given params
            this._fullPath = Path.Combine(folder, fileName);
        }

        /// <summary>
        /// Display _fullPath's directory structure and file name and extension
        /// </summary>
        public void ParsePath()
        {
            Console.WriteLine(Path.GetPathRoot(_fullPath)); // e.g., displays C:\
            Console.WriteLine(Path.GetDirectoryName(_fullPath)); // e.g., displays C:\Temp\CSharp
            Console.WriteLine(Path.GetFileName(_fullPath)); // e.g., displays test.txt
            Console.WriteLine(Path.GetExtension(_fullPath)); // e.g., displays .txt
        }
    }
}
