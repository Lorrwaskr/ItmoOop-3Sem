using System;

namespace IsuExtra
{
    public class Pair
    {
        private readonly TimeSpan pairLenght = TimeSpan.FromHours(1.5);

        public Pair(string classroom, TimeSpan pairTimeBegin, int durationMin, Guid groupId, Guid teacherId)
        {
            Classroom = classroom;
            PairTimeBegin = pairTimeBegin;
            DurationMin = durationMin;
            GroupID = groupId;
            TeacherID = teacherId;
            ID = Guid.NewGuid();
        }

        public string Classroom { get; }
        public TimeSpan PairTimeBegin { get; }
        public int DurationMin { get; }
        public Guid GroupID { get; }
        public Guid TeacherID { get; }
        public Guid ID { get; }
    }
}