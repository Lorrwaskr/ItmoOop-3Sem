using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Repository;

namespace IsuExtra.Repository
{
    public class OgnpStudentsRepository : IRepository<OgnpStudent>
    {
        private List<OgnpStudent> _students;

        public OgnpStudentsRepository()
        {
            _students = new List<OgnpStudent>();
        }

        public void Save(OgnpStudent newStudent)
        {
            OgnpStudent oldStudent = _students.Find(student => student.Student.ID == newStudent.Student.ID);
            if (oldStudent != null)
            {
                _students.Add(newStudent);
                _students.Remove(oldStudent);
            }
            else
            {
                _students.Add(newStudent);
            }
        }

        public void Remove(Guid id)
        {
            _students.Remove(_students.Find(student => student.Student.ID == id));
        }

        public OgnpStudent Get(Guid id)
        {
            return _students.Find(student => student.Student.ID == id);
        }

        public List<OgnpStudent> GetAll()
        {
            return _students;
        }

        public OgnpStudent FindByName(string name)
        {
            return _students.First(student => student.Student.Name == name);
        }

        public List<OgnpStudent> FindByOgnpGroup(Guid groupId)
        {
            return _students.FindAll(student => student.StreamID1 == groupId || student.StreamID2 == groupId);
        }

        public List<OgnpStudent> FindByGroup(Guid groupId)
        {
            return _students.FindAll(student => student.Student.GroupID == groupId);
        }
    }
}