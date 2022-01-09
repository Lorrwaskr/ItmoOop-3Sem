using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.RestorePoint;
using BackupsExtra.ExtraRepository;

namespace BackupsExtra.CleanerAlgorithm
{
    public class ByAmountCleaner : ICleanerAlgorithm<FileInfo>
    {
        private int _limit;

        public ByAmountCleaner(int limit)
        {
            if (limit <= 0)
                throw new Exception("ByAmountCleaner limit can't be <= 0");
            _limit = limit;
        }

        public void ChangeLimit(int limit)
        {
            if (limit <= 0)
                throw new Exception("ByAmountCleaner limit can't be <= 0");
            _limit = limit;
        }

        public List<IRestorePoint<FileInfo>> Run(List<IRestorePoint<FileInfo>> restorePoints)
        {
            return restorePoints.OrderBy(point => point.CreationTime).Take(restorePoints.Count - _limit).ToList();
        }
    }
}