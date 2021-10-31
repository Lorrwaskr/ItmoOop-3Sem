using System;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        public Group(GroupName name, Guid id, int limit)
        {
            if (limit < 0)
                throw new IsuException("Group limit must be > 0");
            Name = name;
            Limit = limit;
            ID = id;
        }

        public int Limit { get; }
        public GroupName Name { get; }
        public Guid ID { get; }
    }
}
