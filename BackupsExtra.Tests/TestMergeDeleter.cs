using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.JobObject;
using Backups.RestorePoint;
using BackupsExtra.DeleterAlgorithm;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.Tests
{
    public class TestMergeDeleter : IDeleterAlgorithm<FileInfo, DirectoryInfo>
    {
        public void Run(IExtraRepository<FileInfo, DirectoryInfo> repository, IEnumerable<IRestorePoint<FileInfo>> restorePointsToRemove)
        {
            IRestorePoint<FileInfo> newRestorePoint = repository.RestorePoints.Except(restorePointsToRemove).First();
            foreach (IRestorePoint<FileInfo> oldRestorePoint in restorePointsToRemove)
            {
                foreach (IJobObject<FileInfo> jobObject in oldRestorePoint.JobObjects)
                {
                    if (newRestorePoint.JobObjects.Exists(jobObject1 => jobObject1.Name == jobObject.Name)) continue;
                    newRestorePoint.JobObjects.Add(jobObject);
                }

                repository.RestorePoints.Remove(oldRestorePoint);
            }
        }
    }
}