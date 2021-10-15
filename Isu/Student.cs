namespace Isu
{
    public class Student
    {
        public Student(Group group, string name, int id)
        {
            Name = name;
            ID = id;
            Group = group.Name;
        }

        public Student(Student other)
        {
            Name = other.Name;
            ID = other.ID;
            Group = other.Name;
        }

        public string Name { get; }
        public int ID { get; }
        public GroupName Group { get; set; }

        public bool Equals(Student other)
        {
            return ID == other.ID;
        }
    }
}