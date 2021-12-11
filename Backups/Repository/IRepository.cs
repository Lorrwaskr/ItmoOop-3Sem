using System.Collections.Generic;
using Backups.BackupAlgorithm;
using Backups.RestorePoint;

namespace Backups.Repository
{
    public interface IRepository<TJobObjectClass, TJobObjectType, TDestinationType>
    {
        IBackupAlgorithm<TJobObjectType, TDestinationType> BackupAlgorithm { get; set; }
        List<IRestorePoint<TJobObjectType>> RestorePoints { get; set; }
        void Save(string restorePointName);
        void Add(TJobObjectClass newObject);
        void AddRange(IEnumerable<TJobObjectClass> newObjects);
        void Remove(TJobObjectClass objectToRemove);
    }
}