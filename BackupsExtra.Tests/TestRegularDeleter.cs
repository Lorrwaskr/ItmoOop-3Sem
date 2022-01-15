using System.Collections.Generic;
using System.IO;
using Backups.RestorePoint;
using BackupsExtra.DeleterAlgorithm;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.Tests
{
    public class TestRegularDeleter : IDeleterAlgorithm<FileInfo, DirectoryInfo>
    {
        public void Run(IExtraRepository<FileInfo, DirectoryInfo> repository, IEnumerable<IRestorePoint<FileInfo>> restorePointsToRemove)
        {
            foreach (IRestorePoint<FileInfo> restorePointToRemove in restorePointsToRemove)
            {
                repository.RestorePoints.Remove(restorePointToRemove);
            }
        }
    }
}