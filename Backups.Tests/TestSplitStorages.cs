using System;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.BackupJob;
using Backups.JobObject;
using Backups.RestorePoint;

namespace Backups.Tests
{
    public class TestSplitStorages : IBackupAlgorithm<FileInfo, DirectoryInfo>
    {
        public void Run(IRestorePoint<FileInfo> restorePoint, DirectoryInfo destination)
        {
            foreach (IJobObject<FileInfo> jobObject in restorePoint.JobObjects)
            {
                jobObject.Name = $"{Path.GetFileNameWithoutExtension(jobObject.Get().Name)}.zip";
            }
        }
    }
}