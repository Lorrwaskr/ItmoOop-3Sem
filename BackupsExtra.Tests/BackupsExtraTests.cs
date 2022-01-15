using System;
using System.IO;
using System.Linq;
using System.Threading;
using Backups.BackupJob;
using Backups.Repository;
using Backups.RestorePoint;
using Backups.Tests;
using BackupsExtra.CleanerAlgorithm;
using BackupsExtra.DeleterAlgorithm;
using BackupsExtra.ExtraBackupJob;
using BackupsExtra.ExtraRepository;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        private IExtraBackupJob<FileInfo, DirectoryInfo> _backupJob;
        private IExtraRepository<FileInfo, DirectoryInfo> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new TestExtraRepository(new TestSplitStorages(), new DirectoryInfo("./BackupsTestRepository"), new ByAmountCleaner(3), new TestMergeDeleter());
            _backupJob = new FileExtraBackupJob("aboba", _repository, new TestLogger());
        }
        
        [Test]
        public void CreateALotOfPoints_UnnecessaryAreDeleted()
        {
            _repository.ChangeCleanerAlgorithm(new ByAmountCleaner(3));
            var file123 = new TestJobObject(@"./123.txt");
            var fileqwe = new TestJobObject(@"./qwe.docx");
            var fileabc = new TestJobObject(@"./abc.txt");
            _backupJob.AddObject(file123);
            _backupJob.CreateNewRestorePoint("1");
            _backupJob.AddObject(fileqwe);
            _backupJob.CreateNewRestorePoint("2");
            _backupJob.AddObject(fileabc);
            _backupJob.CreateNewRestorePoint("3");
            _backupJob.RemoveObject(file123);
            _backupJob.CreateNewRestorePoint("4");
            _backupJob.RemoveObject(fileqwe);
            _backupJob.CreateNewRestorePoint("5");
            _backupJob.RemoveObject(fileabc);
            _backupJob.CreateNewRestorePoint("6");
            Assert.That(_backupJob.JobObjects.Count == 0);
            Assert.That(_backupJob.Repository.RestorePoints.Count == 3);
            Assert.That(_backupJob.Repository.RestorePoints.Last().JobObjects.Count == 0);
        }
        
        [Test]
        public void CreateALotOfPoints_OutdatedAreDeleted()
        {
            _backupJob.Repository.ChangeCleanerAlgorithm(new ByDateCleaner(new TimeSpan(0,9, 0)));
            _backupJob.Repository.ChangeDeleterAlgorithm(new TestRegularDeleter());
            var file123 = new TestJobObject(@"./123.txt");
            var fileqwe = new TestJobObject(@"./qwe.docx");
            var fileabc = new TestJobObject(@"./abc.txt");
            _backupJob.AddObject(file123);
            _backupJob.CreateNewRestorePoint("1");
            _backupJob.AddObject(fileqwe);
            _backupJob.CreateNewRestorePoint("2");
            _backupJob.AddObject(fileabc);
            _backupJob.CreateNewRestorePoint("3");
            for (int i = 0; i < 3; i++)
            {
                _backupJob.Repository.RestorePoints[i].CreationTime = DateTime.Now - TimeSpan.FromMinutes(10);
            }
            _backupJob.RemoveObject(file123);
            _backupJob.CreateNewRestorePoint("4");
            Assert.That(_backupJob.JobObjects.Count == 2);
            Assert.That(_backupJob.Repository.RestorePoints.Count == 1);
            Assert.That(_backupJob.Repository.RestorePoints.Last().JobObjects.Count == 2);
        }
        
        [Test]
        public void MergeDiscardedPoints()
        {
            _backupJob.Repository.ChangeCleanerAlgorithm(new ByAmountCleaner(1));
            _backupJob.Repository.ChangeDeleterAlgorithm(new TestMergeDeleter());
            var file123 = new TestJobObject(@"./123.txt");
            var fileqwe = new TestJobObject(@"./qwe.docx");
            var fileabc = new TestJobObject(@"./abc.txt");
            _backupJob.AddObject(file123);
            _backupJob.CreateNewRestorePoint("1");
            _backupJob.RemoveObject(file123);
            _backupJob.AddObject(fileqwe);
            _backupJob.CreateNewRestorePoint("2");
            _backupJob.RemoveObject(fileqwe);
            _backupJob.AddObject(fileabc);
            _backupJob.CreateNewRestorePoint("3");
            _backupJob.RemoveObject(fileabc);
            Assert.That(_backupJob.JobObjects.Count == 0);
            Assert.That(_backupJob.Repository.RestorePoints.Count == 1);
            Assert.That(_backupJob.Repository.RestorePoints.Last().JobObjects.Count == 3);
        }

        [Test]
        public void SaverTest()
        {
            var file123 = new TestJobObject(@"./123.txt");
            var fileqwe = new TestJobObject(@"./qwe.docx");
            var fileabc = new TestJobObject(@"./abc.txt");
            _backupJob.AddObject(file123);
            _backupJob.CreateNewRestorePoint("1");
            _backupJob.AddObject(fileqwe);
            _backupJob.CreateNewRestorePoint("2");
            _backupJob.AddObject(fileabc);
            _backupJob.CreateNewRestorePoint("3");
            string save = TestLoader.Save(_backupJob);
            IExtraBackupJob<FileInfo, DirectoryInfo> newBackupJob = TestLoader.Load(save);
            Assert.That(_backupJob.Name == newBackupJob.Name);
            Assert.That(_backupJob.JobObjects.Count == newBackupJob.JobObjects.Count);
            Assert.That(_backupJob.Repository.RestorePoints.Count == newBackupJob.Repository.RestorePoints.Count);
            for (int i = 0; i < _backupJob.Repository.RestorePoints.Count; i++)
            {
                Assert.That(_backupJob.Repository.RestorePoints[i].JobObjects.Count == newBackupJob.Repository.RestorePoints[i].JobObjects.Count);
            }
        }
    }
}