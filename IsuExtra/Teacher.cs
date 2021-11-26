using System;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace IsuExtra
{
    public class Teacher
    {
        public Teacher(string name)
        {
            if (name.Length <= 3)
                throw new IsuException("Teacher name lenght must be > 3");
            Name = name;
            ID = Guid.NewGuid();
        }

        public string Name { get; }
        public Guid ID { get; }
    }
}