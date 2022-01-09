using System.Collections.Generic;
using System.IO;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.DeleterAlgorithm
{
    public interface IDeleterAlgorithm<TJobObjectType, TDestinationType>
    {
        void Run(IExtraRepository<TJobObjectType, TDestinationType> repository, IEnumerable<IRestorePoint<TJobObjectType>> restorePointsToRemove);
    }
}