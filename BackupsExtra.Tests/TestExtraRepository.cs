using System;
using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.RestorePoint;
using BackupsExtra.CleanerAlgorithm;
using BackupsExtra.DeleterAlgorithm;
using BackupsExtra.ExtraRepository;
using BackupsExtra.RecoverAlgorithm;

namespace BackupsExtra.Tests
{
    public class TestExtraRepository : IExtraRepository<FileInfo, DirectoryInfo>
    {
        private DirectoryInfo destinationDirectory;
        public TestExtraRepository(IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm, DirectoryInfo newDestinationDirectory, ICleanerAlgorithm<FileInfo> cleanerAlgorithm, IDeleterAlgorithm<FileInfo, DirectoryInfo> deleterAlgorithm)
        {
            BackupAlgorithm = backupAlgorithm;
            RestorePoints = new List<IRestorePoint<FileInfo>>();
            CleanerAlgorithm = cleanerAlgorithm;
            DeleterAlgorithm = deleterAlgorithm;
            destinationDirectory = newDestinationDirectory;
        }

        public ICleanerAlgorithm<FileInfo> CleanerAlgorithm { get; private set; }
        public IDeleterAlgorithm<FileInfo, DirectoryInfo> DeleterAlgorithm { get; private set; }
        public IRecoverAlgorithm<FileInfo, DirectoryInfo> RecoverAlgorithm { get; private set; }

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