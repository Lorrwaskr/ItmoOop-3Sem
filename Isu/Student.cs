namespace Isu
{
    public class Student
    {
        private static uint count = 1;
        public Student(GroupName group, string name)
        {
            ID = count;
            count++;
            Group = group;
            Name = name;
        }

        public GroupName Group { get; private set; }

        public string Name { get; private set; }
        public uint ID { get; private set; }

        public void ChangeGroup(GroupName newGroup)
        {
            Group = newGroup;
        }
    }
}