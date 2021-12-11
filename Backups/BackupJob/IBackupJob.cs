using System.Collections.Generic;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.Repository;

namespace Backups.BackupJob
{
    public interface IBackupJob<TJobObjectClass, TJobObjectType, TDestinationType>
    {
        string Name { get; set; }
        IRepository<TJobObjectClass, TJobObjectType, TDestinationType> Repository { get; set; }
        void AddObject(TJobObjectClass jobObject);
        void AddObjects(IEnumerable<TJobObjectClass> jobObjects);
        void RemoveObject(TJobObjectClass jobObject);
        void ChangeAlgorithm(IBackupAlgorithm<TJobObjectType, TDestinationType> newAlgorithm);
        void RunJob(string restorePointName);
    }
}