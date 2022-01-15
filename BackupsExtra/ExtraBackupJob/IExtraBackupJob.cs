using Backups.BackupJob;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.ExtraBackupJob
{
    public interface IExtraBackupJob<TJobObjectType, TDestinationType> : IBackupJob<TJobObjectType, TDestinationType>
    {
        new IExtraRepository<TJobObjectType, TDestinationType> Repository { get; }

        void RecoverFiles(IRestorePoint<TJobObjectType> restorePoint, TDestinationType destination = default);
    }
}