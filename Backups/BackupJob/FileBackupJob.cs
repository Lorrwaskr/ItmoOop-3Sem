using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.Repository;

namespace Backups.BackupJob
{
    public class FileBackupJob : IBackupJob<FileJobObject, FileInfo, DirectoryInfo>
    {
        public FileBackupJob(string name, IRepository<FileJobObject, FileInfo, DirectoryInfo> repository, IBackupAlgorithm<FileInfo, DirectoryInfo> backupAlgorithm)
        {
            Name = name;
            Repository = repository;
            Repository.BackupAlgorithm = backupAlgorithm;
        }

        public string Name { get; set; }
        public IRepository<FileJobObject, FileInfo, DirectoryInfo> Repository { get; set; }

        public void AddObject(FileJobObject jobObject)
        {
            Repository.Add(jobObject);
        }

        public void AddObjects(IEnumerable<FileJobObject> jobObjects)
        {
            Repository.AddRange(jobObjects);
        }

        public void RemoveObject(FileJobObject jobObject)
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