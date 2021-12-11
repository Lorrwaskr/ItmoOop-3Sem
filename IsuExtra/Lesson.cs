using System;
using Isu.Tools;

namespace IsuExtra
{
    public class Lesson
    {
        public Lesson(string classroom, LessonTimeInterval lessonTime, Guid groupId, Guid teacherId)
        {
            if (string.IsNullOrEmpty(classroom))
                throw new IsuException("Classroom must not be empty");
            Classroom = classroom;
            LessonTime = lessonTime;
            GroupID = groupId;
            TeacherID = teacherId;
            ID = Guid.NewGuid();
        }

        public string Classroom { get; }
        public LessonTimeInterval LessonTime { get; }
        public Guid GroupID { get; }
        public Guid TeacherID { get; }
        public Guid ID { get; }
    }
}