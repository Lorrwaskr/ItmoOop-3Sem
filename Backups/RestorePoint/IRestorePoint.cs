using System;
using System.Collections.Generic;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.JobObject;

namespace Backups.RestorePoint
{
    public interface IRestorePoint<TJobObjectType>
    {
        string Name { get; set; }
        DateTime CreationTime { get; set; }
        AlgorithmType AlgorithmType { get; }
        List<IJobObject<TJobObjectType>> JobObjects { get; set; }
    }
}