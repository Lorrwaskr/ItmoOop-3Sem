using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Client;

namespace Banks.Repository
{
    public class ClientRepository : IRepository<IClient>
    {
        private List<IClient> _clients;

        public ClientRepository()
        {
            _clients = new List<IClient>();
        }

        public void Save(IClient objectForSaving)
        {
            IClient oldObject = _clients.Find(client => client.Id == objectForSaving.Id);
            if (oldObject == null)
            {
                _clients.Add(objectForSaving);
            }
            else
            {
                _clients.Add(objectForSaving);
                _clients.Remove(oldObject);
            }
        }

        public IEnumerable<IClient> GetAll()
        {
            return _clients;
        }

        public IClient Get(Guid id)
        {
            return _clients.Find(client => client.Id == id);
        }
    }
}