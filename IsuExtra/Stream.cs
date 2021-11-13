using System;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Stream
    {
        public Stream(string name, Guid id, int limit, Guid courseId)
        {
            if (limit < 0)
                throw new IsuException("Group limit must be > 0");
            Name = name;
            Limit = limit;
            ID = id;
            CourseID = courseId;
        }

        public int Limit { get; }
        public Guid ID { get; }
        public Guid CourseID { get; }
        public string Name { get; }
    }
}