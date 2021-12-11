using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.RestorePoint;

namespace Backups.Repository
{
    public class FileJobObjectRepository : IRepository<FileJobObject, FileInfo, DirectoryInfo>
    {
        private List<FileJobObject> jobObjects;
        private DirectoryInfo destinationDirectory;

        public FileJobObjectRepository(IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm, DirectoryInfo newDestinationDirectory)
        {
            destinationDirectory = newDestinationDirectory;
            if (!destinationDirectory.Exists)
                destinationDirectory.Create();
            jobObjects = new List<FileJobObject>();
            BackupAlgorithm = backupAlgorithm;
            RestorePoints = new List<IRestorePoint<FileInfo>>();
        }

        public IBackupAlgorithm<FileInfo, DirectoryInfo> BackupAlgorithm { get; set; }
        public List<IRestorePoint<FileInfo>> RestorePoints { get; set; }

        public void Save(string restorePointName = "")
        {
            var newRestorePoint = new FileRestorePoint(restorePointName, jobObjects);
            RestorePoints.Add(newRestorePoint);
            DirectoryInfo newRestorePointDirectory = destinationDirectory.CreateSubdirectory(newRestorePoint.Name);
            BackupAlgorithm.Run(jobObjects, newRestorePointDirectory);
        }

        public void Add(FileJobObject newObject)
        {
            jobObjects.Add(newObject);
        }

        public void AddRange(IEnumerable<FileJobObject> newObjects)
        {
            jobObjects.AddRange(newObjects);
        }

        public void Remove(FileJobObject objectToRemove)
        {
            jobObjects.Remove(objectToRemove);
        }
    }
}