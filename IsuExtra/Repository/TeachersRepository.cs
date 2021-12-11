using System;
using System.Collections.Generic;
using Isu.Repository;

namespace IsuExtra.Repository
{
    public class TeachersRepository : IRepository<Teacher>
    {
        private List<Teacher> _teachers;

        public TeachersRepository()
        {
            _teachers = new List<Teacher>();
        }

        public void Save(Teacher newTeacher)
        {
            Teacher oldTeacher = _teachers.Find(teacher => teacher.ID == newTeacher.ID);
            if (oldTeacher != null)
            {
                _teachers.Add(newTeacher);
                _teachers.Remove(oldTeacher);
            }
            else
            {
                _teachers.Add(newTeacher);
            }
        }

        public Teacher Get(Guid id)
        {
            return _teachers.Find(teacher => teacher.ID == id);
        }

        public List<Teacher> GetAll()
        {
            return _teachers;
        }
    }
}