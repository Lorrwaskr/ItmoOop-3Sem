using System;
using System.Collections.Generic;
using Isu.Repository;

namespace IsuExtra.Repository
{
    public class PairsRepository : IRepository<Pair>
    {
        private List<Pair> _pairs;

        public PairsRepository()
        {
            _pairs = new List<Pair>();
        }

        public void Save(Pair newPair)
        {
            Pair oldPair = _pairs.Find(pair => pair.ID == newPair.ID);
            if (oldPair != null)
            {
                _pairs.Add(newPair);
                _pairs.Remove(oldPair);
            }
            else
            {
                _pairs.Add(newPair);
            }
        }

        public Pair Get(Guid id)
        {
            return _pairs.Find(pair => pair.ID == id);
        }

        public List<Pair> GetAll()
        {
            return _pairs;
        }

        public List<Pair> FindByGroup(Guid groupId)
        {
            return _pairs.FindAll(pair => pair.GroupID == groupId);
        }
    }
}