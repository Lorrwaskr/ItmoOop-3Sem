using System;

namespace IsuExtra
{
    public class Pair
    {
        public Pair(string classroom, PairTimeInterval pairTime, Guid groupId, Guid teacherId)
        {
            Classroom = classroom;
            PairTime = pairTime;
            GroupID = groupId;
            TeacherID = teacherId;
            ID = Guid.NewGuid();
        }

        public string Classroom { get; }
        public PairTimeInterval PairTime { get; }
        public Guid GroupID { get; }
        public Guid TeacherID { get; }
        public Guid ID { get; }

        public class PairTimeInterval
        {
            private readonly TimeSpan pairLenght = TimeSpan.FromHours(1.5);

            public PairTimeInterval(TimeSpan begin, TimeSpan end)
            {
                if (end - begin != pairLenght)
                    throw new ArgumentException("Pair time must be == 1.5h");
                Begin = begin;
                End = end;
            }

            public TimeSpan Begin { get; }
            public TimeSpan End { get; }

            public static bool IsTimeIntersect(PairTimeInterval first, PairTimeInterval second)
            {
                if (first.Begin.Days != second.Begin.Days) return false;
                if (first.Begin.Hours == second.Begin.Hours || first.End.Hours == second.End.Hours)
                    return true;
                if (first.Begin.Hours == second.End.Hours)
                    return first.Begin.Minutes <= second.End.Minutes;
                if (first.End.Hours == second.Begin.Hours)
                    return first.Begin.Minutes >= second.End.Minutes;

                return false;
            }
        }
    }
}