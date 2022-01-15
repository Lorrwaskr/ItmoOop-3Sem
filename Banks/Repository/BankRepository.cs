using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Bank;

namespace Banks.Repository
{
    public class BankRepository : IRepository<IBank>
    {
        public BankRepository()
        {
            Collection = new List<IBank>();
        }

        public ICollection<IBank> Collection { get; }

        public void Save(IBank objectForSaving)
        {
            IBank oldObject = Collection.ToList().Find(bank => bank.Id == objectForSaving.Id);
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

        public IEnumerable<IBank> GetAll()
        {
            return Collection;
        }

        public IBank Get(Guid id)
        {
            return Collection.ToList().Find(bank => bank.Id == id);
        }
    }
}