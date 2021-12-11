using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.JobObject;

namespace Backups.RestorePoint
{
    public class FileRestorePoint : IRestorePoint<FileInfo>
    {
        public FileRestorePoint(string name, IEnumerable<IJobObject<FileInfo>> jobObjects)
        {
            if (name.Length == 0)
                Name = "RestorePoint";
            Name = name;
            JobObjects = jobObjects.ToList();
            CreationTime = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public List<IJobObject<FileInfo>> JobObjects { get; set; }
    }
}