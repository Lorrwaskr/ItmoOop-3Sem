using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Backups.BackupAlgorithm;
using Backups.JobObject;
using Backups.RestorePoint;
using Backups.Tests;
using BackupsExtra.CleanerAlgorithm;
using BackupsExtra.DeleterAlgorithm;
using BackupsExtra.ExtraBackupJob;
using BackupsExtra.ExtraRepository;
using BackupsExtra.Logger;
using Microsoft.VisualBasic;

namespace BackupsExtra.Tests
{
    public class TestLoader
    {
        public static string Save(IExtraBackupJob<FileInfo, DirectoryInfo> backupJob)
        {
            var result = new StringBuilder();
            var fileOut = new StringWriter(result);
            fileOut.WriteLine(backupJob.Name);
            fileOut.WriteLine(backupJob.Repository.GetDestination().FullName);
            fileOut.WriteLine("job objects:");
            foreach (IJobObject<FileInfo> jobObject in backupJob.JobObjects)
            {
                fileOut.WriteLine(jobObject.Get().FullName);
            }

            foreach (IRestorePoint<FileInfo> restorePoint in backupJob.Repository.RestorePoints)
            {
                fileOut.WriteLine("restore point:");
                fileOut.WriteLine(restorePoint.Name);
                fileOut.WriteLine(restorePoint.CreationTime);
                foreach (IJobObject<FileInfo> jobObject in restorePoint.JobObjects)
                {
                    fileOut.WriteLine(jobObject.Get().FullName);
                }
            }

            fileOut.Close();
            return result.ToString();
        }

        public static IExtraBackupJob<FileInfo, DirectoryInfo> Load(string file)
        {
            var fileIn = new StringReader(file);
            string jobName = fileIn.ReadLine();
            string jobDestination = fileIn.ReadLine();
            if (fileIn.ReadLine() != "job objects:")
                throw new InvalidDataException("Invalid file");
            var jobObjects = new List<IJobObject<FileInfo>>();
            string newLine = fileIn.ReadLine();
            while (newLine != "restore point:")
            {
                jobObjects.Add(new TestJobObject(newLine));
                newLine = fileIn.ReadLine();
            }

            var restorePoints = new List<IRestorePoint<FileInfo>>();
            string creationTimeFormat = "dd.MM.yyyy HH:mm:ss";
            var provider = new CultureInfo("de-DE");
            while (newLine != null)
            {
                string restorePointName = fileIn.ReadLine();
                string creationTime = fileIn.ReadLine();
                var restorePointJobObj = new List<IJobObject<FileInfo>>();
                newLine = fileIn.ReadLine();
                while (newLine != "restore point:" && newLine != null)
                {
                    restorePointJobObj.Add(new TestJobObject(newLine));
                    newLine = fileIn.ReadLine();
                }

                var newRestorePoint = new FileRestorePoint(restorePointName, restorePointJobObj);
                newRestorePoint.CreationTime = DateTime.ParseExact(creationTime, creationTimeFormat, provider);
                restorePoints.Add(newRestorePoint);
            }

            fileIn.Close();
            var newBackupJob = new FileExtraBackupJob(jobName, new FileExtraRepository(new FileSplitStorages(), new DirectoryInfo(jobDestination), new ByAmountCleaner(5), new RegularDeleter()), new ConsoleLogger(), jobObjects);
            newBackupJob.Repository.RestorePoints = restorePoints;
            return newBackupJob;
        }
    }
}