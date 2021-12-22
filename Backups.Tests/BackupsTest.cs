/*﻿using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
            var dir = new DirectoryInfo(Environment.CurrentDirectory);
            while (dir.Name != "Backups.Tests")
            {
                dir = dir.Parent;
            }

            Directory.SetCurrentDirectory(dir.FullName + @"\TestFiles");
            dir = new DirectoryInfo(Environment.CurrentDirectory);
            _repository = new FileJobObjectRepository(new FileSplitStorages(), dir);
            _backupJob = new FileBackupJob("aboba", _repository, new FileSplitStorages());
        }
        
        [Test]
        public void TestCase1()
        {
            const string results1 = @".\TestCase1FirstPoint";
            const string results2 = @".\TestCase1SecondPoint";
            if (Directory.Exists(results1))
                Directory.Delete(results1, true);
            if (Directory.Exists(results2))
                Directory.Delete(results2, true);
            _backupJob.ChangeAlgorithm(new FileSplitStorages());
            var file123 = new FileJobObject(@"./123.txt");
            var fileqwe = new FileJobObject(@"./qwe.docx");
            _backupJob.AddObject(file123);
            _backupJob.AddObject(fileqwe);
            _backupJob.RunJob("TestCase1FirstPoint");
            _backupJob.RemoveObject(fileqwe);
            _backupJob.RunJob("TestCase1SecondPoint");
            Assert.True(Directory.Exists(results1));
            Assert.True(Directory.Exists(results2));
            Assert.True(File.Exists(results1 + @"\123.zip"));
            Assert.True(File.Exists(results1 + @"\qwe.zip"));
            Assert.True(File.Exists(results2 + @"\123.zip"));
        }
        
        [Test]
        public void TestCase2()
        {
            const string results1 = @".\TestCase2FirstPoint";
            if (Directory.Exists(results1))
                Directory.Delete(results1, true);
            _backupJob.ChangeAlgorithm(new FileSingleStorage());
            var fileabc = new FileJobObject(@"./abc.txt");
            var fileqwe = new FileJobObject(@"./qwe.docx");
            _backupJob.AddObject(fileabc);
            _backupJob.AddObject(fileqwe);
            _backupJob.RunJob("TestCase2FirstPoint");
            Assert.True(Directory.Exists(results1));
            Assert.True(File.Exists(results1 + @"\files.zip"));
            ZipArchive archive = ZipFile.Open(results1 + @"\files.zip", ZipArchiveMode.Read);
            Assert.True(archive.Entries.Any(file => file.Name == "abc.txt"));
            Assert.True(archive.Entries.Any(file => file.Name == "qwe.docx"));
        }
    }
}*/
