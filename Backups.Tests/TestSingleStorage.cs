using System.IO;
using Backups.BackupAlgorithm;
using Backups.RestorePoint;

namespace Backups.Tests
{
    public class TestSingleStorage : IBackupAlgorithm<FileInfo, DirectoryInfo>
    {
        public void Run(IRestorePoint<FileInfo> restorePoint, DirectoryInfo destination)
        {
            restorePoint.JobObjects.Clear();
            restorePoint.JobObjects.Add(new TestJobObject("files.zip"));
        }
    }
}