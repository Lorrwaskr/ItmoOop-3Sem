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
            Group group = _isuService.AddGroup("M3209");
            Student student = _isuService.AddStudent(group, "Kirill");
            if ((student.Group == group.Name) && (group.Students.Contains(student)))
                return;
            Assert.Fail();
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3209");
                for (int i = 0; i < Group.LIMIT + 1; i++)
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
                _isuService.AddGroup("M3001");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group1 = _isuService.AddGroup("M3209");
                Group group2 = _isuService.AddGroup("M3210");
                Student student = _isuService.AddStudent(group1, "Kirill");
                _isuService.ChangeStudentGroup(student, group2);
                if (!group1.Students.Contains(student) && group2.Students.Contains(student) && (student.Group == group2.Name))
                    throw new IsuException("???");
            });
        }
    }
}