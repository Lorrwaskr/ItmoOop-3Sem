using System;

namespace Isu
{
    public class Student
    {
        public Student(Group group, string name, Guid id)
        {
            Name = name;
            ID = id;
            GroupID = group.ID;
            GroupName = group.Name;
        }

        public string Name { get; }
        public GroupName GroupName { get; }
        public Guid ID { get; }
        public Guid GroupID { get; }
    }
}