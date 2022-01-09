using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.CleanerAlgorithm
{
    public class HybridCleaner : ICleanerAlgorithm<FileInfo>
    {
        public HybridCleaner(List<ICleanerAlgorithm<FileInfo>> algorithms, Modes mode)
        {
            Algorithms = algorithms;
            Mode = mode;
        }

        public enum Modes
        {
            /// <summary>
            /// At least one limit
            /// </summary>
            AtLeastOne,

            /// <summary>
            /// All limits together
            /// </summary>
            All,
        }

        public Modes Mode { get; set; }
        public List<ICleanerAlgorithm<FileInfo>> Algorithms { get; private set; }

        public void ChangeAlgorithms(List<ICleanerAlgorithm<FileInfo>> algorithms)
        {
            Algorithms = algorithms;
        }

        public List<IRestorePoint<FileInfo>> Run(List<IRestorePoint<FileInfo>> restorePoints)
        {
            var result = new List<IRestorePoint<FileInfo>>();
            if (Mode == Modes.AtLeastOne)
            {
                foreach (ICleanerAlgorithm<FileInfo> cleanerAlgorithm in Algorithms)
                {
                    result = result.Union(cleanerAlgorithm.Run(restorePoints)).ToList();
                }
            }
            else if (Mode == Modes.All)
            {
                foreach (ICleanerAlgorithm<FileInfo> cleanerAlgorithm in Algorithms)
                {
                    result = result.Intersect(cleanerAlgorithm.Run(restorePoints)).ToList();
                }
            }

            return result;
        }
    }
}