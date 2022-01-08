using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.RestorePoint;

namespace Backups.Repository
{
    public class FileJobObjectRepository : IRepository<FileInfo, DirectoryInfo>
    {
        private DirectoryInfo destinationDirectory;

        public FileJobObjectRepository(IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm, DirectoryInfo newDestinationDirectory)
        {
            destinationDirectory = newDestinationDirectory;
            if (!destinationDirectory.Exists)
                destinationDirectory.Create();
            BackupAlgorithm = backupAlgorithm;
            RestorePoints = new List<IRestorePoint<FileInfo>>();
        }

        public IBackupAlgorithm<FileInfo, DirectoryInfo> BackupAlgorithm { get; set; }
        public List<IRestorePoint<FileInfo>> RestorePoints { get; set; }
        public void Save(IRestorePoint<FileInfo> restorePoint)
        {
            RestorePoints.Add(restorePoint);
            DirectoryInfo newRestorePointDirectory = destinationDirectory.CreateSubdirectory(restorePoint.Name);
            BackupAlgorithm.Run(restorePoint, newRestorePointDirectory);
        }
    }
}