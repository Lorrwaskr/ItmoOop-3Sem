using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3209", 30);
            Student student = _isuService.AddStudent(group, "Kirill");
            Assert.AreEqual(group.Name, student.GroupName);
            Assert.AreEqual(1, _isuService.FindGroup("M3209").StudentsCount);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group group = _isuService.AddGroup("M3209", 30);
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < _isuService.FindGroup("M3209").Limit + 1; i++)
                {
                    _isuService.AddStudent(_isuService.FindGroup("M3209"), "Anton" + i);
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3001", 30);
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group1 = _isuService.AddGroup("M3209", 30);
            Group group2 = _isuService.AddGroup("M3210", 30);
            Student student = _isuService.AddStudent(group1, "Kirill");
            _isuService.ChangeStudentGroup(student, group2);
            Assert.AreEqual(group2.Name, _isuService.FindStudent("Kirill").GroupName);
        }
    }
}