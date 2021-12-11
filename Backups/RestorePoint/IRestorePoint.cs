using System;
using System.Collections.Generic;
using System.IO;
using Backups.JobObject;

namespace Backups.RestorePoint
{
    public interface IRestorePoint<TJobObjectType>
    {
        string Name { get; set; }
        DateTime CreationTime { get; set; }
        List<IJobObject<TJobObjectType>> JobObjects { get; set; }
    }
}