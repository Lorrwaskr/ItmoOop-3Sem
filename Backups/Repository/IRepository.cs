using System.Collections.Generic;
using Backups.BackupAlgorithm;
using Backups.RestorePoint;

namespace Backups.Repository
{
    public interface IRepository<TJobObjectType, TDestinationType>
    {
        IBackupAlgorithm<TJobObjectType, TDestinationType> BackupAlgorithm { get; set; }
        List<IRestorePoint<TJobObjectType>> RestorePoints { get; set; }
        void Save(IRestorePoint<TJobObjectType> restorePoint);
    }
}