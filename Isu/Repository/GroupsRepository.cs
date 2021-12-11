using System;
using System.Collections.Generic;

namespace Isu.Repository
{
    public class GroupsRepository : IRepository<Group>
    {
        private List<Group> _groups;

        public GroupsRepository()
        {
            _groups = new List<Group>();
        }

        public void Save(Group newGroup)
        {
            Group oldGroup = _groups.Find(group => group.ID == newGroup.ID);
            if (oldGroup != null)
            {
                _groups.Add(newGroup);
                _groups.Remove(oldGroup);
            }
            else
            {
                _groups.Add(newGroup);
            }
        }

        public Group Get(Guid id)
        {
            return _groups.Find(group => group.ID == id);
        }

        public List<Group> GetAll()
        {
            return _groups;
        }

        public Group FindByName(string name)
        {
            return _groups.Find(group => group.Name == name);
        }

        public List<Group> FindByCourse(CourseNumber number)
        {
            return _groups.FindAll(group => group.Name.GetCourseNumber() == number);
        }
    }
}