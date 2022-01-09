using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.BackupJob;
using Backups.JobObject;
using Backups.Repository;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;
using BackupsExtra.Logger;

namespace BackupsExtra.ExtraBackupJob
{
    public class FileExtraBackupJob : IExtraBackupJob<FileInfo, DirectoryInfo>
    {
        public FileExtraBackupJob(string name, IExtraRepository<FileInfo, DirectoryInfo> repository, ILogger logger, ICollection<IJobObject<FileInfo>> jobObjects = default)
        {
            Name = name;
            Repository = repository;
            if (jobObjects == default)
                JobObjects = new List<IJobObject<FileInfo>>();
            else
                JobObjects = jobObjects;
            Logger = logger;
            Logger.Write($"The{this.GetType()} {Name} created");
        }

        public string Name { get; set; }
        public ILogger Logger { get; private set; }
        IRepository<FileInfo, DirectoryInfo> IBackupJob<FileInfo, DirectoryInfo>.Repository { get; set; }
        public IExtraRepository<FileInfo, DirectoryInfo> Repository { get; }
        public ICollection<IJobObject<FileInfo>> JobObjects { get; }
        public void AddObject(IJobObject<FileInfo> jobObject)
        {
            JobObjects.Add(jobObject);
            Logger.Write($"{jobObject.GetType()} {jobObject.Name} added to the backup job");
        }

        public void AddObjects(IEnumerable<IJobObject<FileInfo>> jobObjects)
        {
            foreach (IJobObject<FileInfo> jobObject in jobObjects)
            {
                AddObject(jobObject);
            }
        }

        public void RemoveObject(IJobObject<FileInfo> jobObject)
        {
            JobObjects.Remove(jobObject);
            Logger.Write($"{jobObject.GetType()} {jobObject.Name} removed from the backup job");
        }

        public void ChangeAlgorithm(IBackupAlgorithm<FileInfo, DirectoryInfo> newAlgorithm)
        {
            Repository.BackupAlgorithm = newAlgorithm;
            Logger.Write($"BackupAlgorithm in {this.GetType()} changed to {newAlgorithm.GetType()}");
        }

        public void CreateNewRestorePoint(string restorePointName)
        {
            var newRestorePoint = new FileRestorePoint(restorePointName, JobObjects);
            Repository.Save(newRestorePoint);
            Logger.Write($"New RestorePoint {newRestorePoint.Name} created");
            Repository.ClearRestorePoints();
        }

        public void RecoverFiles(IRestorePoint<FileInfo> restorePoint, DirectoryInfo destination = default)
        {
            Repository.RecoverFiles(restorePoint, destination);
            Logger.Write($"Files from restore point {restorePoint.Name} restored");
        }
    }
}