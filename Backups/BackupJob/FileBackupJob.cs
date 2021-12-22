using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.Repository;

namespace Backups.BackupJob
{
    public class FileBackupJob : IBackupJob<FileInfo, DirectoryInfo>
    {
        public FileBackupJob(string name, IRepository<FileInfo, DirectoryInfo> repository, IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm)
        {
            Name = name;
            Repository = repository;
            Repository.BackupAlgorithm = backupAlgorithm;
        }

        public string Name { get; set; }
        public IRepository<FileInfo, DirectoryInfo> Repository { get; set; }

        public void AddObject(IJobObject<FileInfo> jobObject)
        {
            Repository.Add(jobObject);
        }

        public void AddObjects(IEnumerable<IJobObject<FileInfo>> jobObjects)
        {
            Repository.AddRange(jobObjects);
        }

        public void RemoveObject(IJobObject<FileInfo> jobObject)
        {
            Repository.Remove(jobObject);
        }

        public void ChangeAlgorithm(IBackupAlgorithm<FileInfo, DirectoryInfo> newAlgorithm)
        {
            Repository.BackupAlgorithm = newAlgorithm;
        }

        public void RunJob(string restorePointName = "")
        {
            Repository.Save(restorePointName);
        }
    }
}