using System;
using System.Collections.Generic;
using Isu.Repository;

namespace IsuExtra.Repository
{
    public class LessonsRepository : IRepository<Lesson>
    {
        private List<Lesson> _pairs;

        public LessonsRepository()
        {
            _pairs = new List<Lesson>();
        }

        public void Save(Lesson newLesson)
        {
            Lesson oldLesson = _pairs.Find(pair => pair.ID == newLesson.ID);
            if (oldLesson != null)
            {
                _pairs.Add(newLesson);
                _pairs.Remove(oldLesson);
            }
            else
            {
                _pairs.Add(newLesson);
            }
        }

        public Lesson Get(Guid id)
        {
            return _pairs.Find(pair => pair.ID == id);
        }

        public List<Lesson> GetAll()
        {
            return _pairs;
        }

        public List<Lesson> FindByGroup(Guid groupId)
        {
            return _pairs.FindAll(pair => pair.GroupID == groupId);
        }
    }
}