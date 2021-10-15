using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        public Group(GroupName name, int limit)
        {
            if (limit < 0)
                throw new IsuException("Group limit must be > 0");
            Students = new List<Student>();
            Name = name;
            Limit = (uint)limit;
        }

        public Group(Group oldGroup)
        {
            Limit = oldGroup.Limit;
            Name = new GroupName(oldGroup.Name);
            Students = new List<Student>(oldGroup.Students);
        }

        public uint Limit { get; private set; }

        public GroupName Name { get; private set; }
        public List<Student> Students { get; private set; }

        public bool IsStudentInGroup(Student student)
        {
            return Students.Any(stud => student.ID == stud.ID);
        }
    }
}
