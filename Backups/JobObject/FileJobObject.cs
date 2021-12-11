using System;
using System.IO;

namespace Backups.JobObject
{
    public class FileJobObject : IJobObject<FileInfo>
    {
        private FileInfo file;
        public FileJobObject(string directory)
        {
            file = new FileInfo(directory);
            Name = Path.GetFileNameWithoutExtension(directory);
            if (!file.Exists)
                throw new ArgumentException("File not found: " + directory);
        }

        public string Name { get; set; }

        public bool IsAvailable()
        {
            return file.Exists;
        }

        public FileInfo Get()
        {
            return file;
        }
    }
}