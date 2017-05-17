using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoOperations.Communication
{
    public class Streams
    {
        // class fields
        private string _path;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public Streams(string path)
        {
            this._path = path;
            try
            {
                using (FileStream fileStream = File.Create(path))
                {
                    string myValue = "MyValue";
                    byte[] data = Encoding.UTF8.GetBytes(myValue);
                    fileStream.Write(data, 0, data.Length);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Exception thrown: {0}", ex);
                Console.WriteLine("Path not accessible!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DecodeStringBytes()
        {
            using (FileStream fileStream = File.OpenRead(_path))
            {
                byte[] data = new byte[fileStream.Length];

                for (int index = 0; index < fileStream.Length; index++)
                {
                    data[index] = (byte) fileStream.ReadByte();
                }

                Console.WriteLine(Encoding.UTF8.GetString(data));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="uncompressedFile"></param>
        /// <param name="compressedFile"></param>
        public void GZipStreamWrite(string folder, string uncompressedFile, string compressedFile)
        {
            string uncompressedFilePath = Path.Combine(folder, uncompressedFile); // uncompressed.dat
            string compressedFilePath = Path.Combine(folder, compressedFile);   // compressed.gz

            // create byte array of chars
            byte[] dataToCompress = Enumerable.Repeat((byte) 'a', 1024*1024).ToArray();

            // write using file stream
            using (FileStream uncompressedFileStream = File.Create(uncompressedFilePath))
            {
                uncompressedFileStream.Write(dataToCompress, 0, dataToCompress.Length);
            }

            // write using GZipStream
            using (FileStream compressedFileStream = File.Create(compressedFilePath))
            {
                using (GZipStream compressionStream = new GZipStream(
                    compressedFileStream, CompressionMode.Compress))
                {
                    compressionStream.Write(dataToCompress, 0, dataToCompress.Length);
                }
            }

            // write file info lengths to command line
            FileInfo uncompressedFileInfo = new FileInfo(uncompressedFilePath);
            FileInfo compressedFileInfo = new FileInfo(compressedFilePath);

            Console.WriteLine(uncompressedFileInfo.Length); // displays 1048576
            Console.WriteLine(compressedFileInfo.Length); // 1052
        }

        /// <summary>
        /// 
        /// </summary>
        public void BufferedStreamWrite()
        {
            using (FileStream fileStream = File.Create(_path))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamWriter streamWriter = new StreamWriter(bufferedStream))
                    {
                        streamWriter.Write("Text");
                    }
                }
            }
        }
    }
}
