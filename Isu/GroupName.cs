using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu
{
    public class GroupName
    {
        private static readonly Regex Pattern = new Regex(@"\b[A-Z][1-6][1-6][0-1][0-9]\b");

        public GroupName(string groupName)
        {
            if (!Pattern.IsMatch(groupName))
                throw new IsuException("Incorrect GroupName: " + groupName);
            Name = groupName;
        }

        public string Name { get; }

        public static implicit operator GroupName(string name)
        {
            return new GroupName(name);
        }

        public static implicit operator string(GroupName name)
        {
            return name.Name;
        }

        public static CourseNumber GetCourseNumber(string name)
        {
            return (CourseNumber)(name[2] - '0');
        }

        public CourseNumber GetCourseNumber()
        {
            return (CourseNumber)(Name[2] - '0');
        }
    }
}