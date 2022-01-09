using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.JobObject;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.DeleterAlgorithm
{
    public class MergeDeleter : IDeleterAlgorithm<FileInfo, DirectoryInfo>
    {
        public void Run(IExtraRepository<FileInfo, DirectoryInfo> repository, IEnumerable<IRestorePoint<FileInfo>> restorePointsToRemove)
        {
            string startDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(repository.GetDestination().FullName);
            IRestorePoint<FileInfo> newRestorePoint = repository.RestorePoints.Except(restorePointsToRemove).First();
            foreach (IRestorePoint<FileInfo> oldRestorePoint in restorePointsToRemove)
            {
                foreach (IJobObject<FileInfo> jobObject in oldRestorePoint.JobObjects)
                {
                    if (newRestorePoint.JobObjects.Exists(jobObject1 => jobObject1.Name == jobObject.Name)) continue;
                    File.Copy($"./{oldRestorePoint.Name}/{jobObject.Name}", $"./{newRestorePoint.Name}/{jobObject.Name}");
                    newRestorePoint.JobObjects.Add(jobObject);
                }

                Directory.Delete($"./{oldRestorePoint.Name}", true);
                repository.RestorePoints.Remove(oldRestorePoint);
            }

            Directory.SetCurrentDirectory(startDir);
        }
    }
}