using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Repository;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class IsuService : IIsuService
    {
        public IsuService()
        {
            GroupsRepository = new GroupsRepository();
            StudentsRepository = new StudentsRepository();
        }

        public IsuService(IsuService other)
        {
            GroupsRepository = other.GroupsRepository;
            StudentsRepository = other.StudentsRepository;
        }

        protected GroupsRepository GroupsRepository { get; }
        protected StudentsRepository StudentsRepository { get; }

        public Group AddGroup(string name, int limit)
        {
            var group = new Group(name, IdGenerator.MakeID(), limit);
            GroupsRepository.Save(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(group, name, IdGenerator.MakeID());
            if (StudentsRepository.FindByGroup(group.ID).Count == group.Limit)
                throw new IsuException("Too many students in group " + group.Name);
            StudentsRepository.Save(student);
            return student;
        }

        public Student GetStudent(Guid id)
        {
            return StudentsRepository.Get(id);
        }

        public Student FindStudent(string name)
        {
            return StudentsRepository.FindByName(name);
        }

        public List<Student> FindStudents(string groupName)
        {
            Group group = GroupsRepository.FindByName(groupName);
            return @group == null ? null : StudentsRepository.FindByGroup(@group.ID);
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return StudentsRepository.GetAll().Where(student =>
                    GroupsRepository.Get(student.GroupID).Name.GetCourseNumber() ==
                    courseNumber)
                .ToList();
        }

        public Group FindGroup(string groupName)
        {
            return GroupsRepository.FindByName(groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return GroupsRepository.FindByCourse(courseNumber);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (StudentsRepository.FindByGroup(newGroup.ID).Count == newGroup.Limit)
                throw new IsuException("Too many students in group " + newGroup.Name);
            Group oldGroup = GroupsRepository.Get(student.GroupID);
            var newStudent = new Student(newGroup, student.Name, student.ID);
            StudentsRepository.Save(newStudent);
        }

        public void DeleteStudent(Student student)
        {
            Group group = GroupsRepository.Get(student.GroupID);
            StudentsRepository.Save(student);
        }
    }
}