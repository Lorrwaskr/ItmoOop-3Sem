using System;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace IsuExtra
{
    public class Teacher
    {
        public Teacher(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new IsuException("Teacher name must not be empty");
            Name = name;
            ID = Guid.NewGuid();
        }

        public string Name { get; }
        public Guid ID { get; }
    }
}