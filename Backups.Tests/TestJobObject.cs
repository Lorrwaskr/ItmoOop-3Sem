using System.IO;
using Backups.JobObject;

namespace Backups.Tests
{
    public class TestJobObject : IJobObject<FileInfo>
    {
        private FileInfo file;
        public TestJobObject(string directory)
        {
            file = new FileInfo(directory);
            Name = Path.GetFileNameWithoutExtension(directory);
        }

        public string Name { get; set; }

        public bool IsAvailable()
        {
            return true;
        }

        public FileInfo Get()
        {
            return file;
        }
    }
}