using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.Repository;
using Backups.RestorePoint;

namespace Backups.BackupJob
{
    public class FileBackupJob : IBackupJob<FileInfo, DirectoryInfo>
    {
        public FileBackupJob(string name, IRepository<FileInfo, DirectoryInfo> repository, IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm)
        {
            Name = name;
            Repository = repository;
            JobObjects = new List<IJobObject<FileInfo>>();
            Repository.BackupAlgorithm = backupAlgorithm;
        }

        public string Name { get; set; }
        public IRepository<FileInfo, DirectoryInfo> Repository { get; set; }

        public ICollection<IJobObject<FileInfo>> JobObjects { get; }

        public void AddObject(IJobObject<FileInfo> jobObject)
        {
            JobObjects.Add(jobObject);
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
        }

        public void ChangeAlgorithm(IBackupAlgorithm<FileInfo, DirectoryInfo> newAlgorithm)
        {
            Repository.BackupAlgorithm = newAlgorithm;
        }

        public void CreateNewRestorePoint(string restorePointName)
        {
            var newRestorePoint = new FileRestorePoint(restorePointName, JobObjects);
            Repository.Save(newRestorePoint);
        }
    }
}