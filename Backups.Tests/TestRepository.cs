using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.RestorePoint;

namespace Backups.Tests
{
    public class TestRepository : Backups.Repository.IRepository<FileInfo, DirectoryInfo>
    {
        public TestRepository(IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm)
        {
            BackupAlgorithm = backupAlgorithm;
            RestorePoints = new List<IRestorePoint<FileInfo>>();
        }

        public IBackupAlgorithm<FileInfo, DirectoryInfo> BackupAlgorithm { get; set; }
        public List<IRestorePoint<FileInfo>> RestorePoints { get; set; }
        public void Save(IRestorePoint<FileInfo> restorePoint)
        {
            RestorePoints.Add(restorePoint);
            BackupAlgorithm.Run(restorePoint, null);
        }
    }
}