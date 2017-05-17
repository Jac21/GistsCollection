using System;
using System.IO;
using IoOperations.Communication;
using IoOperations.FileStructureObjects;
using System.Configuration;

namespace IoOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            string functionalitySwitch = ConfigurationManager.AppSettings["Functions"];

            switch (functionalitySwitch)
            {
                case "FileStructureObjects":

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

                    break;
                case "Communication":

                    /*
                     *  Streams class declaration and use
                     */

                    Streams streams = new Streams(@"C:\Temp\Csharp");

                    /*
                     *  WebObjects class declaration and use
                     */

                    WebObjects webObjects = new WebObjects("https://news.ycombinator.com/");
                    webObjects.DisplayHtml();

                    /*
                     *  Async class declaration and use
                     */

                    Async async = new Async();
                    async.CreateAndWriteAsyncToFile(@"C:\Temp\Csharp\test.dat");
                    async.ReadAsyncHttpRequest();
                    async.ExecuteMultipleRequestsInParallel();

                    break;
                default:
                    Console.WriteLine("AppSetting invalid!");
                    break;
            }


        }
    }
}
