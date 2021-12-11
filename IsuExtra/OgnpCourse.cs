using System;
using Isu.Tools;

namespace IsuExtra
{
    public class OgnpCourse
    {
        public OgnpCourse(char megafaculty, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new IsuException("OgnpCourse name must not be empty");
            Megafaculty = megafaculty;
            Name = name;
            ID = Guid.NewGuid();
        }

        public char Megafaculty { get; }
        public string Name { get; }
        public Guid ID { get; }
    }
}