using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Receipt;
using Banks.Transaction;

namespace Banks.Repository
{
    public class TransactionRepository : IRepository<ITransaction>
    {
        public TransactionRepository()
        {
            Collection = new List<ITransaction>();
        }

        public ICollection<ITransaction> Collection { get; }

        public void Save(ITransaction objectForSaving)
        {
            ITransaction oldObject = Collection.ToList().Find(transaction => transaction.Id == objectForSaving.Id);
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

        public IEnumerable<ITransaction> GetAll()
        {
            return Collection;
        }

        public ITransaction Get(Guid id)
        {
            return Collection.ToList().Find(transaction => transaction.Id == id);
        }
    }
}