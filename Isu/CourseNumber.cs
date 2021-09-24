using System;

namespace Isu
{
    public class CourseNumber
    {
        private readonly int _course;

        public CourseNumber(int course)
        {
            if ((course > 0) && (course <= 4))
                _course = course;
            else
                throw new Exception("Incorrect course");
        }

        public int Course => _course;

        public static implicit operator CourseNumber(int num)
        {
            return new CourseNumber(num);
        }

        public static implicit operator int(CourseNumber num)
        {
            return num.Course;
        }

        public bool Equals(CourseNumber other)
        {
            return Course == other.Course;
        }
    }
}