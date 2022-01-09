using System.Collections.Generic;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.CleanerAlgorithm
{
    public interface ICleanerAlgorithm<TJobObjectType>
    {
        List<IRestorePoint<TJobObjectType>> Run(List<IRestorePoint<TJobObjectType>> restorePoints);
    }
}