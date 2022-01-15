using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.RestorePoint;

namespace BackupsExtra.CleanerAlgorithm
{
    public class AllHybridCleaner : ICleanerAlgorithm<FileInfo>
    {
        public AllHybridCleaner(List<ICleanerAlgorithm<FileInfo>> algorithms)
        {
            Algorithms = algorithms;
        }

        public List<ICleanerAlgorithm<FileInfo>> Algorithms { get; private set; }

        public void ChangeAlgorithms(List<ICleanerAlgorithm<FileInfo>> algorithms)
        {
            Algorithms = algorithms;
        }

        public List<IRestorePoint<FileInfo>> Run(List<IRestorePoint<FileInfo>> restorePoints)
        {
            var result = new List<IRestorePoint<FileInfo>>();
            foreach (ICleanerAlgorithm<FileInfo> cleanerAlgorithm in Algorithms)
            {
                result = result.Union(cleanerAlgorithm.Run(restorePoints)).ToList();
            }

            return result;
        }
    }
}