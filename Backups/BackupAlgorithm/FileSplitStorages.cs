using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.JobObject;

namespace Backups.BackupAlgorithm
{
    public class FileSplitStorages : IBackupAlgorithm<FileInfo, DirectoryInfo>
    {
        public AlgorithmType AlgorithmType { get; } = AlgorithmType.SplitStorages;

        public void Run(IEnumerable<IJobObject<FileInfo>> jobObjects, DirectoryInfo destinationDirectory)
        {
            string startDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(destinationDirectory.FullName);
            foreach (IJobObject<FileInfo> jobObject in jobObjects)
            {
                destinationDirectory.CreateSubdirectory(jobObject.Name);
                jobObject.Get().CopyTo($"./{jobObject.Name}/{jobObject.Name + jobObject.Get().Extension}");
                ZipFile.CreateFromDirectory($"./{jobObject.Name}", $"./{jobObject.Name}.zip");
                Directory.Delete($"./{jobObject.Name}", true);
            }

            Directory.SetCurrentDirectory(startDir);
        }
    }
}