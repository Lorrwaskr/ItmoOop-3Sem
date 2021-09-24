using System.Collections.Generic;

namespace Isu
{
    public class Course
    {
        public Course(CourseNumber number)
        {
            Groups = new List<Group>();
            Number = number;
        }

        public List<Group> Groups { get; private set; }
        public CourseNumber Number { get; private set; }
    }
}