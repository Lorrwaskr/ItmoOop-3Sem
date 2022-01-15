using System.IO;
using Backups.JobObject;
using Backups.RestorePoint;

namespace BackupsExtra.RecoverAlgorithm
{
    public interface IRecoverAlgorithm<TJobObjectType, TDestinationType>
    {
        void Run(IRestorePoint<TJobObjectType> restorePoint, TDestinationType repositoryDestination, TDestinationType destination);
    }
}