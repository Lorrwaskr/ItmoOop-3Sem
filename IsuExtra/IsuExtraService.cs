using System;
using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;
using IsuExtra.Repository;
using IsuExtra.Services;

namespace IsuExtra
{
    public class IsuExtraService : IsuService, IIsuExtraService
    {
        public IsuExtraService(IsuService service)
            : base(service)
        {
            LessonsRepository = new LessonsRepository();
            OgnpStudentsRepository = new OgnpStudentsRepository();
            OgnpStreamsRepository = new OgnpStreamsRepository();
            OgnpCoursesRepository = new OgnpCourseRepository();
            TeachersRepository = new TeachersRepository();
        }

        protected LessonsRepository LessonsRepository { get; }
        protected OgnpStudentsRepository OgnpStudentsRepository { get; }
        protected OgnpStreamsRepository OgnpStreamsRepository { get; }
        protected OgnpCourseRepository OgnpCoursesRepository { get; }
        protected TeachersRepository TeachersRepository { get; }

        public void EnrollStudent(Student student, Stream stream)
        {
            if (IsStudentEnrolledInStream(student, stream.ID))
                throw new IsuException("Student already enrolled in stream " + stream.Name);
            if (student.GroupName.Megafaculty == OgnpCoursesRepository.Get(OgnpStreamsRepository.Get(stream.ID).CourseID).Megafaculty)
                throw new IsuException("Student cannot enroll in his megafaculty course");
            if (OgnpStudentsRepository.FindByOgnpGroup(stream.ID).Count == stream.Limit)
                throw new IsuException("Too many students in group " + stream.Name);
            OgnpStudent oldStudent = OgnpStudentsRepository.Get(student.ID) ?? new OgnpStudent(student, Guid.Empty, Guid.Empty);
            OgnpStudent ognpStudent;

            List<Lesson> studentPairs = LessonsRepository.FindByGroup(student.GroupID);

            if (oldStudent.StreamID1 == Guid.Empty)
            {
                ognpStudent = new OgnpStudent(student, stream.ID, oldStudent.StreamID2);
                if (oldStudent.StreamID2 != Guid.Empty)
                    studentPairs.AddRange(LessonsRepository.FindByGroup(oldStudent.StreamID2));
            }
            else if (oldStudent.StreamID2 == Guid.Empty)
            {
                ognpStudent = new OgnpStudent(student, oldStudent.StreamID1, stream.ID);
                studentPairs.AddRange(LessonsRepository.FindByGroup(oldStudent.StreamID1));
            }
            else
            {
                throw new IsuException("Student already enrolled on 2 courses");
            }

            if (CheckPairIntersection(studentPairs, LessonsRepository.FindByGroup(stream.ID)))
                throw new IsuException("Pairs intersect");

            OgnpStudentsRepository.Save(ognpStudent);
        }

        public Lesson AddLesson(string classroom, LessonTimeInterval lessonTime, Guid groupId, Guid teacherId)
        {
            var pair = new Lesson(classroom, lessonTime, groupId, teacherId);
            LessonsRepository.Save(pair);
            return pair;
        }

        public OgnpCourse AddOgnpCourse(char megafaculty, string name)
        {
            var course = new OgnpCourse(megafaculty, name);
            OgnpCoursesRepository.Save(course);
            return course;
        }

        public Stream AddOgnpStream(Guid courseId, string name, int limit)
        {
            var stream = new Stream(name, limit, courseId);
            OgnpStreamsRepository.Save(stream);
            return stream;
        }

        public Teacher AddTeacher(string name)
        {
            var teacher = new Teacher(name);
            TeachersRepository.Save(teacher);
            return teacher;
        }

        public void UnenrollStudent(Student student, Stream stream)
        {
            OgnpStudent oldStudent = OgnpStudentsRepository.Get(student.ID);
            if (oldStudent.StreamID1 == stream.ID)
                OgnpStudentsRepository.Save(new OgnpStudent(oldStudent.Student, Guid.Empty, oldStudent.StreamID2));
            if (oldStudent.StreamID2 == stream.ID)
                OgnpStudentsRepository.Save(new OgnpStudent(oldStudent.Student, oldStudent.StreamID1, Guid.Empty));
        }

        public OgnpStudent GetOgnpStudent(Guid studentId)
        {
            return OgnpStudentsRepository.Get(studentId);
        }

        public Stream GetStream(Guid streamId)
        {
            return OgnpStreamsRepository.Get(streamId);
        }

        public OgnpCourse GetOgnpCourse(Guid courseId)
        {
            return OgnpCoursesRepository.Get(courseId);
        }

        public List<Stream> GetOgnpCourseStreams(Guid courseId)
        {
            return OgnpStreamsRepository.FindByCourse(courseId);
        }

        public List<OgnpStudent> GetOgnpGroupStudents(Guid groupId)
        {
            return OgnpStudentsRepository.FindByGroup(groupId);
        }

        public List<Student> FindUnenrolledStudents(Guid groupId)
        {
            return StudentsRepository.FindByGroup(groupId).FindAll(student => IsStudentUnenrolled(student));
        }

        private static bool CheckPairIntersection(List<Lesson> first, List<Lesson> second)
        {
            return first.Any(pair =>
                second.Any(pair1 => LessonTimeInterval.IsTimeIntersect(pair.LessonTime, pair1.LessonTime)));
        }

        private bool IsStudentUnenrolled(Student student)
        {
            OgnpStudent ognpStudent = OgnpStudentsRepository.Get(student.ID);
            if (ognpStudent == null)
                return true;
            return ognpStudent.StreamID1 == Guid.Empty &&
                   ognpStudent.StreamID2 == Guid.Empty;
        }

        private bool IsStudentEnrolledInStream(Student student, Guid streamId)
        {
            OgnpStudent ognpStudent = OgnpStudentsRepository.Get(student.ID);
            if (ognpStudent == null)
                return false;
            return ognpStudent.StreamID1 == streamId || ognpStudent.StreamID2 == streamId;
        }
    }
}