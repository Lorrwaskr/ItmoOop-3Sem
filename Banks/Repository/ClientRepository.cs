using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Client;

namespace Banks.Repository
{
    public class ClientRepository : IRepository<IClient>
    {
        public ClientRepository()
        {
            Collection = new List<IClient>();
        }

        public ICollection<IClient> Collection { get; }

        public void Save(IClient objectForSaving)
        {
            IClient oldObject = Collection.ToList().Find(client => client.Id == objectForSaving.Id);
            if (oldObject == null)
            {
                Collection.Add(objectForSaving);
            }
            else
            {
                Collection.Add(objectForSaving);
                Collection.Remove(oldObject);
            }
        }

        public IEnumerable<IClient> GetAll()
        {
            return Collection;
        }

        public IClient Get(Guid id)
        {
            return Collection.ToList().Find(client => client.Id == id);
        }
    }
}