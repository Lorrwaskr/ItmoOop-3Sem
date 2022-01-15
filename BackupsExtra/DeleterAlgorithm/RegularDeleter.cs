using System.Collections.Generic;
using System.IO;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.DeleterAlgorithm
{
    public class RegularDeleter : IDeleterAlgorithm<FileInfo, DirectoryInfo>
    {
        public void Run(IExtraRepository<FileInfo, DirectoryInfo> repository, IEnumerable<IRestorePoint<FileInfo>> restorePointsToRemove)
        {
            string startDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(repository.GetDestination().FullName);
            foreach (IRestorePoint<FileInfo> restorePointToRemove in restorePointsToRemove)
            {
                Directory.Delete($"./{restorePointToRemove.Name}", true);
                repository.RestorePoints.Remove(restorePointToRemove);
            }

            Directory.SetCurrentDirectory(startDir);
        }
    }
}