using System.Collections.Generic;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        private static readonly uint Limit = 30;
        public Group(GroupName name)
        {
            Students = new List<Student>();
            Name = name;
        }

        public static uint LIMIT => Limit;
        public GroupName Name { get; private set; }
        public List<Student> Students { get; private set; }

        public Student AddStudent(Student student)
        {
            if (Students.Count == Limit)
                throw new IsuException("Too many students in group " + Name);
            Students.Add(student);
            student.ChangeGroup(Name);
            return student;
        }

        public bool IsStudentInGroup(Student student)
        {
            foreach (Student stud in Students)
            {
                if (student.ID == stud.ID)
                    return true;
            }

            return false;
        }
    }
}
