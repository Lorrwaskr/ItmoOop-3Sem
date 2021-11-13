using System;

namespace IsuExtra
{
    public class OgnpCourse
    {
        public OgnpCourse(char megafaculty, string name, Guid id)
        {
            Megafaculty = megafaculty;
            Name = name;
            ID = id;
        }

        public char Megafaculty { get; }
        public string Name { get; }
        public Guid ID { get; }
    }
}