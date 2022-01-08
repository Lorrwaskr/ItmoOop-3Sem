using System.Collections.Generic;
using Backups.JobObject;
using Backups.RestorePoint;

namespace Backups.BackupAlgorithm
{
    public interface IBackupAlgorithm<TJobObjectType, TDestinationType>
    {
        AlgorithmType AlgorithmType { get; }
        void Run(IRestorePoint<TJobObjectType> restorePoint, TDestinationType destination);
    }
}