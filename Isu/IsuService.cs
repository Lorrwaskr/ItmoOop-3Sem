using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class IsuService : IIsuService
    {
        private List<Group> groups;
        private StudentID _id;

        public IsuService()
        {
            groups = new List<Group>();
            _id = new StudentID(1);
        }

        public Group AddGroup(string name, int limit)
        {
            var newGroupList = new List<Group>(groups);
            var group = new Group(name, limit);
            newGroupList.Add(group);
            groups = newGroupList;
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(group, name, _id.MakeID());
            if (group.Students.Count == group.Limit)
                throw new IsuException("Too many students in group " + group.Name);
            group.Students.Add(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return groups.SelectMany(group => group.Students).First(student => student.ID == id);
        }

        public Student FindStudent(string name)
        {
            return groups.SelectMany(group => group.Students).First(student => student.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return FindGroup(groupName).Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return FindGroups(courseNumber).SelectMany(group => group.Students).ToList();
        }

        public Group FindGroup(string groupName)
        {
            return groups.First(group => group.Name == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return groups.Where(group => group.Name.GetCourseNumber() == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            FindGroup(student.Group.Name).Students.Remove(student);
            student.Group = newGroup.Name;
            newGroup.Students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            FindGroup(student.Group).Students.Remove(student);
        }
    }
}