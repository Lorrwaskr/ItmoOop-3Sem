using System;
using System.Linq;
using Isu;
using Isu.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraServiceTest
    {
        private IsuExtraService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuExtraService(new IsuService());
        }

        [Test]
        public void EnrollStudentInStreams_StudentHaveStreams()
        {
            _isuService.AddGroup("S3209", 30);
            Student student = _isuService.AddStudent(_isuService.FindGroup("S3209"), "Lev");
            OgnpCourse course = _isuService.AddOgnpCourse('M', "BTS");
            Stream stream1 = _isuService.AddOgnpStream(course.ID, "BTS1", 30);
            OgnpCourse course1 = _isuService.AddOgnpCourse('F', "F");
            Stream stream3 = _isuService.AddOgnpStream(course1.ID, "f1", 30);
            
            _isuService.EnrollStudent(_isuService.GetStudent(student.ID), stream1);
            _isuService.EnrollStudent(_isuService.GetStudent(student.ID), stream3);
            
            Assert.AreEqual(stream1.ID, _isuService.GetOgnpStudent(student.ID).StreamID1);
            Assert.AreEqual(stream3.ID, _isuService.GetOgnpStudent(student.ID).StreamID2);
        }
        
        [Test]
        public void UnenrollStudent_StudentDoesntHaveStream()
        {
            _isuService.AddGroup("S3209", 30);
            Student student = _isuService.AddStudent(_isuService.FindGroup("S3209"), "Lev");
            OgnpCourse course = _isuService.AddOgnpCourse('M', "BTS");
            Stream stream1 = _isuService.AddOgnpStream(course.ID, "BTS1", 30);
            OgnpCourse course1 = _isuService.AddOgnpCourse('F', "F");
            Stream stream3 = _isuService.AddOgnpStream(course1.ID, "f1", 30);
            
            _isuService.EnrollStudent(_isuService.GetStudent(student.ID), stream1);
            _isuService.EnrollStudent(_isuService.GetStudent(student.ID), stream3);
            _isuService.UnenrollStudent(_isuService.GetStudent(student.ID), stream1);
            Assert.AreEqual(Guid.Empty, _isuService.GetOgnpStudent(student.ID).StreamID1);
            Assert.AreEqual(stream3.ID, _isuService.GetOgnpStudent(student.ID).StreamID2);
        }

        [Test]
        public void EnrollStudentOnHisMegafaculty_ThrowException()
        {
            _isuService.AddGroup("M3209", 30);
            Student student = _isuService.AddStudent(_isuService.FindGroup("M3209"), "Lev");
            OgnpCourse course = _isuService.AddOgnpCourse('F', "BTS");
            Stream stream1 = _isuService.AddOgnpStream(course.ID, "BTS1", 30);
            OgnpCourse course1 = _isuService.AddOgnpCourse('M', "F");
            Stream stream3 = _isuService.AddOgnpStream(course1.ID, "f1", 30);

            Assert.Catch<IsuException>(() =>
            {
                _isuService.EnrollStudent(_isuService.GetStudent(student.ID), stream3);
            });
        }

        [Test]
        public void ReachMaxStudentsPerStream_ThrowException()
        {
            _isuService.AddGroup("M3209", 50);
            OgnpCourse course = _isuService.AddOgnpCourse('F', "BTS");
            Stream stream1 = _isuService.AddOgnpStream(course.ID, "BTS1", 30);
            OgnpCourse course1 = _isuService.AddOgnpCourse('M', "F");
            Stream stream3 = _isuService.AddOgnpStream(course1.ID, "f1", 30);

            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < stream1.Limit + 1; i++)
                {
                    Student student = _isuService.AddStudent(_isuService.FindGroup("M3209"), "Anton" + i);
                    _isuService.EnrollStudent(student, _isuService.GetStream(stream1.ID));
                }
            });
        }
        
        [Test]
        public void UnrollStudentOnSameStream_ThrowException()
        {
            _isuService.AddGroup("M3209", 30);
            Student student = _isuService.AddStudent(_isuService.FindGroup("M3209"), "Lev");
            OgnpCourse course = _isuService.AddOgnpCourse('F', "BTS");
            Stream stream1 = _isuService.AddOgnpStream(course.ID, "BTS1", 30);
            _isuService.EnrollStudent(_isuService.GetStudent(student.ID), stream1);

            Assert.Catch<IsuException>(() =>
            {
                _isuService.EnrollStudent(_isuService.GetStudent(student.ID), stream1);
            });
        }

        [Test]
        public void FindUnenrolledStudents()
        {
            _isuService.AddGroup("M3209", 30);
            Student student = _isuService.AddStudent(_isuService.FindGroup("M3209"), "Lev");
            Student student1 = _isuService.AddStudent(_isuService.FindGroup("M3209"), "Renat");
            OgnpCourse course = _isuService.AddOgnpCourse('F', "BTS");
            Stream stream1 = _isuService.AddOgnpStream(course.ID, "BTS1", 30);
            _isuService.EnrollStudent(student, stream1);
            _isuService.FindUnenrolledStudents(_isuService.FindGroup("M3209").ID);
            Assert.AreEqual(student1, _isuService.FindUnenrolledStudents(_isuService.FindGroup("M3209").ID).First());
        }

        [Test]
        public void AddPairs_PairsAdded()
        {
            Group group = _isuService.AddGroup("M3209", 30);
            Pair pair1 = _isuService.AddPair("asd", new TimeSpan(1, 11, 40, 0), 90,
                group.ID, Guid.Empty);
            Pair pair2 = _isuService.AddPair("asd", new TimeSpan(1, 14, 10, 0), 90,
                group.ID, Guid.Empty);
            Assert.Contains(pair1, _isuService.GetPairs(group.ID));
            Assert.Contains(pair2, _isuService.GetPairs(group.ID));
        }

        [Test]
        public void PairsIntersect_ThrowException()
        {
            _isuService.AddGroup("M3209", 30);
            _isuService.AddPair("asd", new TimeSpan(1, 11, 40, 0), 90,
                _isuService.FindGroup("M3209").ID, Guid.Empty);

            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddPair("asd", new TimeSpan(1, 11, 10, 0), 90,
                    _isuService.FindGroup("M3209").ID, Guid.Empty);
            });
        }

        [Test]
        public void OgnpPairsIntersect_ThrowException()
        {
            _isuService.AddGroup("M3209", 30);
            Student student = _isuService.AddStudent(_isuService.FindGroup("M3209"), "Lev");
            OgnpCourse course = _isuService.AddOgnpCourse('F', "BTS");
            Stream stream1 = _isuService.AddOgnpStream(course.ID, "BTS1", 30);
            _isuService.AddPair("asd", new TimeSpan(1, 11, 40, 0), 90,
                _isuService.FindGroup("M3209").ID, Guid.Empty);
            _isuService.AddPair("asd", new TimeSpan(1, 11, 10, 0), 90,
                stream1.ID, Guid.Empty);
            
            Assert.Catch<IsuException>(() =>
            {
                _isuService.EnrollStudent(_isuService.GetStudent(student.ID), stream1);
            });
        }
    }
}