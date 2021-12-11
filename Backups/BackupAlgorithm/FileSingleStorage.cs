using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.JobObject;

namespace Backups.BackupAlgorithm
{
    public class FileSingleStorage : IBackupAlgorithm<FileInfo, DirectoryInfo>
    {
        public void Run(IEnumerable<IJobObject<FileInfo>> jobObjects, DirectoryInfo destinationDirectory)
        {
            string startDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(destinationDirectory.FullName);
            destinationDirectory.CreateSubdirectory("temp");
            foreach (IJobObject<FileInfo> jobObject in jobObjects)
            {
                jobObject.Get().CopyTo($"./temp/{jobObject.Get().Name}");
            }

            ZipFile.CreateFromDirectory(@"./temp", $"./files.zip");
            Directory.Delete("./temp", true);
            Directory.SetCurrentDirectory(startDir);
        }
    }
}