using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class IsuService : IIsuService
    {
        private List<Course> courses;
        private Dictionary<uint, Student> idList;

        public IsuService()
        {
            courses = new List<Course>();
            idList = new Dictionary<uint, Student>();
        }

        public Group AddGroup(string name)
        {
            Group group = FindGroup(name);
            if (group != null)
                return group;
            group = new Group(name);
            if (FindCourse(GroupName.GetCourseNumber(name)) == null)
                courses.Add(new Course(GroupName.GetCourseNumber(name)));
            FindCourse(GroupName.GetCourseNumber(name)).Groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(group.Name, name);
            if (group.IsStudentInGroup(student))
                throw new IsuException("Student " + name + " is already in group " + group.Name);
            group.AddStudent(student);
            idList.Add(student.ID, student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return idList[(uint)id];
        }

        public Student FindStudent(string name)
        {
            foreach (KeyValuePair<uint, Student> student in idList)
            {
                if (student.Value.Name == name)
                    return student.Value;
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            if (FindCourse(GroupName.GetCourseNumber(groupName)) == null)
                return null;
            foreach (Group group in FindCourse(GroupName.GetCourseNumber(groupName)).Groups)
            {
                if (group.Name == groupName)
                    return group.Students;
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            if (FindCourse(courseNumber) == null)
                return null;
            var result = new List<Student>();
            foreach (Group group in FindCourse(courseNumber).Groups)
            {
                result.AddRange(group.Students);
            }

            if (result.Count == 0)
                return null;
            else return result;
        }

        public Group FindGroup(string groupName)
        {
            if (FindCourse(GroupName.GetCourseNumber(groupName)) == null)
                return null;
            foreach (Group group in FindCourse(GroupName.GetCourseNumber(groupName)).Groups)
            {
                if (group.Name == groupName)
                    return group;
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return FindCourse(courseNumber).Groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            GroupName oldGroupName = student.Group;
            newGroup.AddStudent(student);
            FindGroup(oldGroupName).Students.Remove(student);
        }

        public Course FindCourse(CourseNumber courseNumber)
        {
            foreach (Course course in courses)
            {
                if (course.Number.Equals(courseNumber))
                    return course;
            }

            return null;
        }
    }
}