using Isu.Tools;

namespace Isu
{
    public class Group
    {
        public Group(GroupName name, int id, int limit, int studentsCount)
        {
            if (limit < 0)
                throw new IsuException("Group limit must be > 0");
            if (studentsCount < 0)
                throw new IsuException("Students count must be >= 0");
            Name = name;
            Limit = limit;
            ID = id;
            StudentsCount = studentsCount;
        }

        public int Limit { get; }
        public int StudentsCount { get; }
        public GroupName Name { get; }
        public int ID { get; }
    }
}
