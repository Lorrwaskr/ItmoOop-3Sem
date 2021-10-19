using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class IsuService : IIsuService
    {
        private List<Group> _groups;
        private List<Student> _students;
        private StudentID _studentId;
        private GroupID _groupId;

        public IsuService()
        {
            _groups = new List<Group>();
            _students = new List<Student>();
            _studentId = new StudentID(1);
            _groupId = new GroupID(1);
        }

        public Group AddGroup(string name, int limit)
        {
            var group = new Group(name, _groupId.MakeID(), limit, 0);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(group, name, _studentId.MakeID());
            if (group.StudentsCount == group.Limit)
                throw new IsuException("Too many students in group " + group.Name);
            var newGroup = new Group(group.Name, group.ID, group.Limit, group.StudentsCount + 1);
            _students.Add(student);
            _groups.Add(newGroup);
            _groups.Remove(group);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _students.First(student => student.ID == id);
        }

        public Student FindStudent(string name)
        {
            return _students.First(student => student.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return _students.Where(student => student.GroupID == FindGroup(groupName).ID).ToList();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _students.Where(student => _groups.First(group => @group.ID == student.GroupID).Name.GetCourseNumber() == courseNumber).ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _groups.First(group => group.Name == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(group => group.Name.GetCourseNumber() == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group oldGroup = _groups.First(group => group.ID == student.GroupID);
            var newStudent = new Student(newGroup, student.Name, student.ID);
            _students.Add(newStudent);
            _students.Remove(student);
            var newOldGroup = new Group(oldGroup.Name, oldGroup.ID, oldGroup.Limit, oldGroup.StudentsCount - 1);
            _groups.Add(newOldGroup);
            _groups.Remove(oldGroup);
            var newNewGroup = new Group(newGroup.Name, newGroup.ID, newGroup.Limit, newGroup.StudentsCount + 1);
            _groups.Add(newNewGroup);
            _groups.Remove(newGroup);
        }

        public void DeleteStudent(Student student)
        {
            Group group = _groups.First(group => group.ID == student.GroupID);
            var newGroup = new Group(group.Name, group.ID, group.Limit, group.StudentsCount - 1);
            _groups.Add(newGroup);
            _groups.Remove(group);
            _students.Remove(student);
        }
    }
}