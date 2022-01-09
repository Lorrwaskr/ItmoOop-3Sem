using System.IO;
using Backups.BackupAlgorithm;
using Backups.Repository;
using Backups.RestorePoint;
using BackupsExtra.CleanerAlgorithm;
using BackupsExtra.DeleterAlgorithm;

namespace BackupsExtra.ExtraRepository
{
    public interface IExtraRepository<TJobObjectType, TDestinationType> : IRepository<TJobObjectType, TDestinationType>
    {
        ICleanerAlgorithm<TJobObjectType> CleanerAlgorithm { get; }
        IDeleterAlgorithm<TJobObjectType, TDestinationType> DeleterAlgorithm { get; }
        TDestinationType GetDestination();
        void ClearRestorePoints();
        void ChangeCleanerAlgorithm(ICleanerAlgorithm<FileInfo> cleanerAlgorithm);
        void ChangeDeleterAlgorithm(IDeleterAlgorithm<FileInfo, DirectoryInfo> deleterAlgorithm);
        void RecoverFiles(IRestorePoint<FileInfo> restorePoint, DirectoryInfo destination = default);
    }
}