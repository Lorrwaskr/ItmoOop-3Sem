using System.Collections.Generic;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.RestorePoint;

namespace Backups.Repository
{
    public interface IRepository<TJobObjectType, TDestinationType>
    {
        IBackupAlgorithm<TJobObjectType, TDestinationType> BackupAlgorithm { get; set; }
        List<IRestorePoint<TJobObjectType>> RestorePoints { get; set; }
        void Save(string restorePointName);
        void Add(IJobObject<TJobObjectType> newObject);
        void AddRange(IEnumerable<IJobObject<TJobObjectType>> newObjects);
        void Remove(IJobObject<TJobObjectType> objectToRemove);
    }
}