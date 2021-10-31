using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class IsuService : IIsuService
    {
        private GroupsRepository _groups;
        private StudentsRepository _students;

        public IsuService()
        {
            _groups = new GroupsRepository();
            _students = new StudentsRepository();
        }

        public Group AddGroup(string name, int limit)
        {
            var group = new Group(name, IdGenerator.MakeID(), limit);
            _groups.Save(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(group, name, IdGenerator.MakeID());
            if (_students.FindByGroup(group.ID).Count == group.Limit)
                throw new IsuException("Too many students in group " + group.Name);
            var newGroup = new Group(group.Name, group.ID, group.Limit);
            _students.Save(student);
            _groups.Save(newGroup);
            return student;
        }

        public Student GetStudent(Guid id)
        {
            return _students.Get(id);
        }

        public Student FindStudent(string name)
        {
            return _students.FindByName(name);
        }

        public List<Student> FindStudents(string groupName)
        {
            Group group = _groups.FindByName(groupName);
            return @group == null ? null : _students.FindByGroup(@group.ID);
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _students.GetAll().Where(student =>
                    _groups.Get(student.GroupID).Name.GetCourseNumber() ==
                    courseNumber)
                .ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _groups.FindByName(groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.FindByCourse(courseNumber);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (_students.FindByGroup(newGroup.ID).Count == newGroup.Limit)
                throw new IsuException("Too many students in group " + newGroup.Name);
            Group oldGroup = _groups.Get(student.GroupID);
            var newStudent = new Student(newGroup, student.Name, student.ID);
            _students.Save(newStudent);
        }

        public void DeleteStudent(Student student)
        {
            Group group = _groups.Get(student.GroupID);
            _students.Save(student);
        }
    }
}