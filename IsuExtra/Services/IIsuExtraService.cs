using System;
using System.Collections.Generic;
using Isu;
using Isu.Services;

namespace IsuExtra.Services
{
    public interface IIsuExtraService : IIsuService
    {
        Lesson AddLesson(string classroom, LessonTimeInterval lessonTime, Guid groupId, Guid teacherId);

        OgnpCourse AddOgnpCourse(char megafaculty, string name);

        Stream AddOgnpStream(Guid courseId, string name, int limit);

        Teacher AddTeacher(string name);

        void EnrollStudent(Student student, Stream stream);

        void UnenrollStudent(Student student, Stream stream);

        OgnpStudent GetOgnpStudent(Guid studentId);

        Stream GetStream(Guid streamId);

        OgnpCourse GetOgnpCourse(Guid courseId);

        List<Stream> GetOgnpCourseStreams(Guid courseId);

        List<OgnpStudent> GetOgnpGroupStudents(Guid groupId);

        List<Student> FindUnenrolledStudents(Guid groupId);
    }
}