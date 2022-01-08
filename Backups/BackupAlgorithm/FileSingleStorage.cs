using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.JobObject;
using Backups.RestorePoint;

namespace Backups.BackupAlgorithm
{
    public class FileSingleStorage : IBackupAlgorithm<FileInfo, DirectoryInfo>
    {
        public AlgorithmType AlgorithmType { get; } = AlgorithmType.SingleStorage;

        public void Run(IRestorePoint<FileInfo> restorePoint, DirectoryInfo destinationDirectory)
        {
            string startDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(destinationDirectory.FullName);
            destinationDirectory.CreateSubdirectory("temp");
            foreach (IJobObject<FileInfo> jobObject in restorePoint.JobObjects)
            {
                jobObject.Get().CopyTo($"./temp/{jobObject.Get().Name}");
            }

            ZipFile.CreateFromDirectory(@"./temp", $"./files.zip");
            Directory.Delete("./temp", true);
            Directory.SetCurrentDirectory(startDir);
        }
    }
}