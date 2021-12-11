using System;
using System.Collections.Generic;
using Isu.Repository;

namespace IsuExtra.Repository
{
    public class OgnpCourseRepository : IRepository<OgnpCourse>
    {
        private List<OgnpCourse> _courses;

        public OgnpCourseRepository()
        {
            _courses = new List<OgnpCourse>();
        }

        public void Save(OgnpCourse newCourse)
        {
            OgnpCourse oldCourse = _courses.Find(course => course.ID == newCourse.ID);
            if (oldCourse != null)
            {
                _courses.Add(newCourse);
                _courses.Remove(oldCourse);
            }
            else
            {
                _courses.Add(newCourse);
            }
        }

        public OgnpCourse Get(Guid id)
        {
            return _courses.Find(course => course.ID == id);
        }

        public List<OgnpCourse> GetAll()
        {
            return _courses;
        }
    }
}