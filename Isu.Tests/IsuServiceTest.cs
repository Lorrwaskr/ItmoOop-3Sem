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
            Assert.AreEqual(student.Group, group.Name);
            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group group = _isuService.AddGroup("M3209", 30);
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < group.Limit + 1; i++)
                {
                    _isuService.AddStudent(group, "Anton" + i);
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
            Assert.False(group1.Students.Contains(student));
            Assert.Contains(student, group2.Students);
            Assert.AreEqual(student.Group, group2.Name);
        }
    }
}