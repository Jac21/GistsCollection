using System.IO;
using IoOperations.FileStructureObjects;

namespace IoOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             *  Drives class declaration and use
             */

            Drives drives = new Drives();
            drives.ListDriveInfo();

            /*
             *  Directories class declaration and use
             */

            Directories directories = new Directories(@"C:\Temp\CSharp\Directory");
            directories.CreateDirectoryInfo(true);

            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Program Files");
            directories.BuildDirectoryTree(directoryInfo, "*", 2, 0);

            /*
             *  Files class declaration and use
             */

            Files files = new Files(@"C:\Temp\CSharp\Directory", "test.txt");
            files.ParsePath();
        }
    }
}
