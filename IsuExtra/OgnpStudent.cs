using System;
using Isu;

namespace IsuExtra
{
    public class OgnpStudent
    {
        public OgnpStudent(Student student, Guid streamId1, Guid streamId2)
        {
            Student = student;
            StreamID1 = streamId1;
            StreamID2 = streamId2;
        }

        public Student Student { get; }
        public Guid StreamID1 { get; }
        public Guid StreamID2 { get; }
    }
}