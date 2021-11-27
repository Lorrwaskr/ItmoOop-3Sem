using System;

namespace IsuExtra
{
    public class LessonTimeInterval
    {
        private readonly TimeSpan pairLenght = TimeSpan.FromHours(1.5);

        public LessonTimeInterval(TimeSpan begin, TimeSpan end)
        {
            if (end - begin != pairLenght)
                throw new ArgumentException("Pair time must be == 1.5h");
            Begin = begin;
            End = end;
        }

        public TimeSpan Begin { get; }
        public TimeSpan End { get; }

        public static bool IsTimeIntersect(LessonTimeInterval first, LessonTimeInterval second)
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