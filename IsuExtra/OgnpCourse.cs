using System;
using Isu.Tools;

namespace IsuExtra
{
    public class OgnpCourse
    {
        public OgnpCourse(char megafaculty, string name)
        {
            if (name.Length == 0)
                throw new IsuException("OgnpCourse name lenght must be > 0");
            Megafaculty = megafaculty;
            Name = name;
            ID = Guid.NewGuid();
        }

        public char Megafaculty { get; }
        public string Name { get; }
        public Guid ID { get; }
    }
}