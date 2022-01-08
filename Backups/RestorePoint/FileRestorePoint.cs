using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.BackupAlgorithm;
using Backups.JobObject;

namespace Backups.RestorePoint
{
    public class FileRestorePoint : IRestorePoint<FileInfo>
    {
        public FileRestorePoint(string name, IEnumerable<IJobObject<FileInfo>> jobObjects, AlgorithmType algorithmType)
        {
            AlgorithmType = algorithmType;
            JobObjects = jobObjects.ToList();
            CreationTime = DateTime.UtcNow;
            Name = $"{name}_{CreationTime.Day}.{CreationTime.Month}.{CreationTime.Year}_{CreationTime.Hour}.{CreationTime.Minute}.{CreationTime.Second}.{CreationTime.Millisecond}";
        }

        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public AlgorithmType AlgorithmType { get; }
        public List<IJobObject<FileInfo>> JobObjects { get; set; }
    }
}