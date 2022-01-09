using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.CleanerAlgorithm
{
    public class ByDateCleaner : ICleanerAlgorithm<FileInfo>
    {
        private TimeSpan _maxTimeInterval;

        public ByDateCleaner(TimeSpan maxTimeInterval)
        {
            if (maxTimeInterval == TimeSpan.Zero)
                throw new Exception("ByDateCleaner MaxTimeInterval can't be == 0");
            _maxTimeInterval = maxTimeInterval;
        }

        public void ChangeMaxTimeInterval(TimeSpan newInterval)
        {
            if (newInterval == TimeSpan.Zero)
                throw new Exception("ByDateCleaner MaxTimeInterval can't be == 0");
            _maxTimeInterval = newInterval;
        }

        public List<IRestorePoint<FileInfo>> Run(List<IRestorePoint<FileInfo>> restorePoints)
        {
            return restorePoints.Where(point => DateTime.Now - point.CreationTime > _maxTimeInterval).ToList();
        }
    }
}