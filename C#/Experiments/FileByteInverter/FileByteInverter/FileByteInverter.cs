using System;
using System.IO;

namespace FileByteInverter
{
    //Class to provide access to large file
    //as if it were a byte array
    public class FileByteArray : IDisposable
    {
        //holds underlying stream used to access file
        private Stream _stream;

        //Create a new FileByteArray encapsulating
        //a particular file
        public FileByteArray(string fileName)
        {
            _stream = new FileStream(fileName, FileMode.Open);
        }

        //Dispose of the stream
        protected void Dispose(bool safeToDispose)
        {
            if (safeToDispose)
            {
                if (_stream != Stream.Null)
                {
                    _stream.Dispose();
                    _stream = Stream.Null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true); //called from dispose, safe
            GC.SuppressFinalize(this);
        }

        //finalizer
        ~FileByteArray()
        {
            Dispose(false);
        }

        //indexer to provde read/write access to file
        public byte this[long index] //long is 64-bits
        {
            //read one byte at offset index and return it
            get
            {
                byte[] buffer = new byte[1];
                _stream.Seek(index, SeekOrigin.Begin);
                _stream.Read(buffer, 0, 1);
                return buffer[0];
            }
            
            //write one byte at offset index and return it
            set
            {
                // ReSharper disable once RedundantExplicitArraySize
                byte[] buffer = new byte[1] {value};
                _stream.Seek(index, SeekOrigin.Begin);
                _stream.Write(buffer, 0, 1);
            }
        }

        //Get the total length of the file
        public long Length
        {
            get { return _stream.Seek(0, SeekOrigin.End); }
        }
    }

    public class Reverse
    {
        static void Main(string[] args)
        {
            //check for args
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: FileByteInverter <filename>");
                return;
            }

            FileByteArray file = new FileByteArray(args[0]);
            long len = file.Length;

            //swap bytes in file to reverse it
            for (long i = 0; i < len/2; ++i)
            {
                //note that indexing the "file" variable
                //invokes the indexer on the FileByteStream
                //class, which reads and writes bytes in file
                byte t = file[i];
                file[i] = file[len - i - 1];
                file[len - i - 1] = t;
            }

            file.Dispose();
        }
    }
}
