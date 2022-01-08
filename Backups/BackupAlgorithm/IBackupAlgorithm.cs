﻿using System.Collections.Generic;
using Backups.JobObject;

namespace Backups.BackupAlgorithm
{
    public interface IBackupAlgorithm<TJobObjectType, TDestinationType>
    {
        AlgorithmType AlgorithmType { get; }
        void Run(IEnumerable<IJobObject<TJobObjectType>> jobObjects, TDestinationType destination);
    }
}