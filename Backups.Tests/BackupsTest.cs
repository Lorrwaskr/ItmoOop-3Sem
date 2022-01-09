using System;
using System.IO;
using Backups.BackupAlgorithm;
using Backups.BackupJob;
using Backups.JobObject;
using Backups.Repository;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        private IBackupJob<FileInfo, DirectoryInfo> _backupJob;
        private IRepository<FileInfo, DirectoryInfo> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new TestRepository(new TestSplitStorages());
            _backupJob = new FileBackupJob("aboba", _repository, new TestSplitStorages());
        }
        
        [Test]
        public void TestCase1()
        {
            _backupJob.ChangeAlgorithm(new TestSplitStorages());
            var file123 = new TestJobObject(@"./123.txt");
            var fileqwe = new TestJobObject(@"./qwe.docx");
            _backupJob.AddObject(file123);
            _backupJob.AddObject(fileqwe);
            _backupJob.CreateNewRestorePoint("TestCase1FirstPoint");
            Assert.That(_backupJob.JobObjects.Count == 2);
            _backupJob.RemoveObject(file123);
            _backupJob.CreateNewRestorePoint("TestCase1SecondPoint");
            Assert.That(_backupJob.JobObjects.Count == 1);
            Assert.That(_backupJob.Repository.RestorePoints.Count == 2);
            Assert.That(_backupJob.Repository.RestorePoints[0].JobObjects[0].Name == "123.zip");
            Assert.That(_backupJob.Repository.RestorePoints[0].JobObjects[1].Name == "qwe.zip");
            Assert.That(_backupJob.Repository.RestorePoints[1].JobObjects[0].Name == "qwe.zip");
        }
        
        [Test]
        public void TestCase2()
        {
            _backupJob.ChangeAlgorithm(new TestSingleStorage());
            var fileabc = new TestJobObject(@"./abc.txt");
            var fileqwe = new TestJobObject(@"./qwe.docx");
            _backupJob.AddObject(fileabc);
            _backupJob.AddObject(fileqwe);
            _backupJob.CreateNewRestorePoint("TestCase2FirstPoint");
            Assert.That(_backupJob.JobObjects.Count == 2);
            Assert.That(_backupJob.Repository.RestorePoints[0].JobObjects[0].Get().Name == "files.zip");
        }
    }
}
