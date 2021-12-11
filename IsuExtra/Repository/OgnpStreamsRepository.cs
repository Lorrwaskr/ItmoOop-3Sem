using System;
using System.Collections.Generic;
using Isu;
using Isu.Repository;

namespace IsuExtra.Repository
{
    public class OgnpStreamsRepository : IRepository<Stream>
    {
        private List<Stream> _streams;

        public OgnpStreamsRepository()
        {
            _streams = new List<Stream>();
        }

        public void Save(Stream newStream)
        {
            Stream oldStream = _streams.Find(stream => stream.ID == newStream.ID);
            if (oldStream != null)
            {
                _streams.Add(newStream);
                _streams.Remove(oldStream);
            }
            else
            {
                _streams.Add(newStream);
            }
        }

        public Stream Get(Guid id)
        {
            return _streams.Find(stream => stream.ID == id);
        }

        public List<Stream> GetAll()
        {
            return _streams;
        }

        public Stream FindByName(string name)
        {
            return _streams.Find(stream => stream.Name == name);
        }

        public List<Stream> FindByCourse(Guid id)
        {
            return _streams.FindAll(stream => stream.CourseID == id);
        }
    }
}