using System;

namespace IsuExtra
{
    public class Teacher
    {
        public Teacher(string name, Guid id)
        {
            Name = name;
            ID = id;
        }

        public string Name { get; }
        public Guid ID { get; }
    }
}