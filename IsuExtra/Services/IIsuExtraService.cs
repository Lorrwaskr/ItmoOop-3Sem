using System;
using System.Collections.Generic;
using Isu;
using Isu.Services;
using Isu.Tools;

namespace IsuExtra.Services
{
    public interface IIsuExtraService : IIsuService
    {
        public Pair AddPair(string classroom, Pair.PairTimeInterval pairTime, Guid groupId, Guid teacherId);

        public OgnpCourse AddOgnpCourse(char megafaculty, string name);

        public Stream AddOgnpStream(Guid courseId, string name, int limit);

        public Teacher AddTeacher(string name);

        public void EnrollStudent(Student student, Stream stream);

        public void UnenrollStudent(Student student, Stream stream);

        public OgnpStudent GetOgnpStudent(Guid studentId);

        public Stream GetStream(Guid streamId);

        public OgnpCourse GetOgnpCourse(Guid courseId);

        public List<Stream> GetOgnpCourseStreams(Guid courseId);

        public List<OgnpStudent> GetOgnpGroupStudents(Guid groupId);

        public List<Student> FindUnenrolledStudents(Guid groupId);
    }
}