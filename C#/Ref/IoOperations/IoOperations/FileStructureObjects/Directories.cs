using System;
using System.IO;
using System.Security.AccessControl;

namespace IoOperations
{
    public class Directories
    {
        // class fields
        public string _directoryPath;

        /// <summary>
        /// Class constructor, creates new directory based on parameter
        /// </summary>
        /// <param name="directoryPath"></param>
        public Directories(string directoryPath)
        {
            this._directoryPath = directoryPath;

            try
            {
                var directory = Directory.CreateDirectory(directoryPath);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Exception thrown! - {0}", ex);
                Console.WriteLine("Not allowed to write in this directory!");
            }
        }

        /// <summary>
        /// Creates directory info sub-directory, adds user access control based
        /// on parameter boolean
        /// </summary>
        public void CreateDirectoryInfo(bool accessControl)
        {
            // var directory = Directory.CreateDirectory(@"C:\Temp\CSharp\Directory");

            var directoryInfo = new DirectoryInfo(_directoryPath + @"\DirectoryInfo");
            directoryInfo.Create();

            if (accessControl)
            {
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
                directorySecurity.AddAccessRule(
                    new FileSystemAccessRule("everyone",
                        FileSystemRights.ReadAndExecute,
                        AccessControlType.Allow));
                directoryInfo.SetAccessControl(directorySecurity);
            }
        }

        /// <summary>
        /// Deletes constructor-driven directory
        /// </summary>
        public void DeleteDirectory()
        {
            if (Directory.Exists(_directoryPath))
            {
                Directory.Delete(_directoryPath);
            }
        }

        /// <summary>
        /// Recursive function that build directory tree based on given search pattern,
        /// and the levels of depth searched
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="searchPattern"></param>
        /// <param name="maxLevel"></param>
        /// <param name="currentLevel"></param>
        public void BuildDirectoryTree(DirectoryInfo directoryInfo, string searchPattern,
            int maxLevel, int currentLevel)
        {
            if (currentLevel >= maxLevel)
            {
                return;
            }

            string indent = new string('-', currentLevel);

            try
            {
                DirectoryInfo[] subDirectories = directoryInfo.GetDirectories(searchPattern);

                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    Console.WriteLine(indent + subDirectory.Name);
                    BuildDirectoryTree(subDirectory, searchPattern, maxLevel, currentLevel + 1);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // don't have access to the folder
                Console.WriteLine("Exception thrown! - {0}", ex);
                Console.WriteLine(indent + "Can't access: " + directoryInfo.Name);
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                // folder is removed while iterating
                Console.WriteLine("Exception thrown! - {0}", ex);
                Console.WriteLine(indent + "Can't find: " + directoryInfo.Name);
                return;
            }
        }

        public void MoveDirectory(string sourceDirectory, string destinationDirectory)
        {
            Directory.Move(sourceDirectory, destinationDirectory);
        }
    }
}
