using System.Collections.Generic;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.Repository;

namespace Backups.BackupJob
{
    public interface IBackupJob<TJobObjectType, TDestinationType>
    {
        string Name { get; set; }
        IRepository<TJobObjectType, TDestinationType> Repository { get; set; }
        void AddObject(IJobObject<TJobObjectType> jobObject);
        void AddObjects(IEnumerable<IJobObject<TJobObjectType>> jobObjects);
        void RemoveObject(IJobObject<TJobObjectType> jobObject);
        void ChangeAlgorithm(IBackupAlgorithm<TJobObjectType, TDestinationType> newAlgorithm);
        void RunJob(string restorePointName);
    }
}