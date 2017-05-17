using System;
using System.IO;

namespace IoOperations.FileStructureObjects
{
    public class Drives
    {
        // class fields
        public readonly DriveInfo[] _drivesInfo;

        // constructor
        public Drives()
        {
            this._drivesInfo = DriveInfo.GetDrives();
        }

        /// <summary>
        /// List machine drives and relevant information
        /// </summary>
        public void ListDriveInfo()
        {
            foreach (DriveInfo driveInfo in _drivesInfo)
            {
                Console.WriteLine("Drive {0}", driveInfo.Name);
                Console.WriteLine(" File type: {0}", driveInfo.DriveType);

                if (driveInfo.IsReady)
                {
                    Console.WriteLine(" Volume label: {0}", driveInfo.VolumeLabel);
                    Console.WriteLine(" File system: {0}", driveInfo.DriveFormat);
                    Console.WriteLine(
                        " Available space to current user: {0, 15} bytes", driveInfo.AvailableFreeSpace);
                    Console.WriteLine(" Total available space:  {0, 15} bytes", driveInfo.TotalFreeSpace);
                    Console.WriteLine(" Total size of drives:   {0, 15} bytes", driveInfo.TotalSize);
                }
            }
        }
    }
}
