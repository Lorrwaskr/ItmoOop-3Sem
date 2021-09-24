using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu
{
    public class GroupName
    {
        private static readonly Regex Pattern = new Regex(@"M3[1-4][0-1][0-9]");

        private readonly string _groupName;

        public GroupName(string groupName)
        {
            if (!Pattern.IsMatch(groupName))
                throw new IsuException("Incorrect GroupName: " + groupName);
            _groupName = groupName;
        }

        public string GetGroupName => _groupName;

        public static implicit operator GroupName(string name)
        {
            return new GroupName(name);
        }

        public static implicit operator string(GroupName name)
        {
            return name.GetGroupName;
        }

        public static CourseNumber GetCourseNumber(GroupName name)
        {
            return (int)(((string)name)[2] - '0');
        }

        public CourseNumber GetCourseNumber()
        {
            return (int)(_groupName[2] - '0');
        }
    }
}