using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.JobObject;
using Backups.RestorePoint;

namespace Backups.BackupAlgorithm
{
    public class FileSplitStorages : IBackupAlgorithm<FileInfo, DirectoryInfo>
    {
        public void Run(IRestorePoint<FileInfo> restorePoint, DirectoryInfo destinationDirectory)
        {
            string startDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(destinationDirectory.FullName);
            foreach (IJobObject<FileInfo> jobObject in restorePoint.JobObjects)
            {
                destinationDirectory.CreateSubdirectory(jobObject.Name);
                jobObject.Get().CopyTo($"./{jobObject.Name}/{jobObject.Name + jobObject.Get().Extension}");
                ZipFile.CreateFromDirectory($"./{jobObject.Name}", $"./{jobObject.Name}.zip");
                Directory.Delete($"./{jobObject.Name}", true);
            }

            Directory.SetCurrentDirectory(startDir);
        }
    }
}