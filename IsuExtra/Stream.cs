using System;
using System.Text.RegularExpressions;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Stream
    {
        public Stream(string name, int limit, Guid courseId)
        {
            if (limit <= 0)
                throw new IsuException("Group limit must be > 0");
            if (string.IsNullOrEmpty(name))
                throw new IsuException("Stream name must not be empty");
            Name = name;
            Limit = limit;
            ID = Guid.NewGuid();
            CourseID = courseId;
        }

        public int Limit { get; }
        public Guid ID { get; }
        public Guid CourseID { get; }
        public string Name { get; }
    }
}