using System;
using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.Repository;
using Backups.RestorePoint;
using BackupsExtra.CleanerAlgorithm;
using BackupsExtra.DeleterAlgorithm;
using BackupsExtra.RecoverAlgorithm;

namespace BackupsExtra.ExtraRepository
{
    public class FileExtraRepository : IExtraRepository<FileInfo, DirectoryInfo>
    {
        private DirectoryInfo destinationDirectory;
        public FileExtraRepository(IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm, DirectoryInfo newDestinationDirectory, ICleanerAlgorithm<FileInfo> cleanerAlgorithm, IDeleterAlgorithm<FileInfo, DirectoryInfo> deleterAlgorithm)
        {
            CleanerAlgorithm = cleanerAlgorithm;
            DeleterAlgorithm = deleterAlgorithm;
            destinationDirectory = newDestinationDirectory;
            BackupAlgorithm = backupAlgorithm;
            RestorePoints = new List<IRestorePoint<FileInfo>>();
        }

        public ICleanerAlgorithm<FileInfo> CleanerAlgorithm { get; private set; }
        public IDeleterAlgorithm<FileInfo, DirectoryInfo> DeleterAlgorithm { get; private set; }
        public IRecoverAlgorithm<FileInfo, DirectoryInfo> RecoverAlgorithm { get; private set; }
        public IBackupAlgorithm<FileInfo, DirectoryInfo> BackupAlgorithm { get; set; }
        public List<IRestorePoint<FileInfo>> RestorePoints { get; set; }

        public void ChangeCleanerAlgorithm(ICleanerAlgorithm<FileInfo> cleanerAlgorithm)
        {
            CleanerAlgorithm = cleanerAlgorithm;
        }

        public void ChangeDeleterAlgorithm(IDeleterAlgorithm<FileInfo, DirectoryInfo> deleterAlgorithm)
        {
            DeleterAlgorithm = deleterAlgorithm;
        }

        public void ChangeRecoverAlgorithm(IRecoverAlgorithm<FileInfo, DirectoryInfo> recoverAlgorithm)
        {
            RecoverAlgorithm = recoverAlgorithm;
        }

        public DirectoryInfo GetDestination()
        {
            return destinationDirectory;
        }

        public void ClearRestorePoints()
        {
            List<IRestorePoint<FileInfo>> pointsToRemove = CleanerAlgorithm.Run(RestorePoints);
            if (pointsToRemove.Count == RestorePoints.Count)
                throw new Exception("All points can't be deleted");
            DeleterAlgorithm.Run(this, pointsToRemove);
        }

        public void RecoverFiles(IRestorePoint<FileInfo> restorePoint, DirectoryInfo destination = default)
        {
            RecoverAlgorithm.Run(restorePoint, destinationDirectory, destination);
        }

        public void Save(IRestorePoint<FileInfo> restorePoint)
        {
            RestorePoints.Add(restorePoint);
            DirectoryInfo newRestorePointDirectory = destinationDirectory.CreateSubdirectory(restorePoint.Name);
            BackupAlgorithm.Run(restorePoint, newRestorePointDirectory);
        }
    }
}