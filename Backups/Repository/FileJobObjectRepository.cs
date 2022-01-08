using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.RestorePoint;

namespace Backups.Repository
{
    public class FileJobObjectRepository : IRepository<FileInfo, DirectoryInfo>
    {
        private List<IJobObject<FileInfo>> jobObjects;
        private DirectoryInfo destinationDirectory;

        public FileJobObjectRepository(IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm, DirectoryInfo newDestinationDirectory)
        {
            destinationDirectory = newDestinationDirectory;
            if (!destinationDirectory.Exists)
                destinationDirectory.Create();
            jobObjects = new List<IJobObject<FileInfo>>();
            BackupAlgorithm = backupAlgorithm;
            RestorePoints = new List<IRestorePoint<FileInfo>>();
        }

        public IBackupAlgorithm<FileInfo, DirectoryInfo> BackupAlgorithm { get; set; }
        public List<IRestorePoint<FileInfo>> RestorePoints { get; set; }

        public void Save(string restorePointName = "")
        {
            var newRestorePoint = new FileRestorePoint(restorePointName, jobObjects, BackupAlgorithm.AlgorithmType);
            RestorePoints.Add(newRestorePoint);
            DirectoryInfo newRestorePointDirectory = destinationDirectory.CreateSubdirectory(newRestorePoint.Name);
            BackupAlgorithm.Run(jobObjects, newRestorePointDirectory);
        }

        public void Add(IJobObject<FileInfo> newObject)
        {
            jobObjects.Add(newObject);
        }

        public void AddRange(IEnumerable<IJobObject<FileInfo>> newObjects)
        {
            jobObjects.AddRange(newObjects);
        }

        public void Remove(IJobObject<FileInfo> objectToRemove)
        {
            jobObjects.Remove(objectToRemove);
        }
    }
}