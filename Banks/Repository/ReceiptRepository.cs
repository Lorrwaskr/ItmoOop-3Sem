using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Receipt;

namespace Banks.Repository
{
    public class ReceiptRepository : IRepository<ReceiptBase>
    {
        public ReceiptRepository()
        {
            Collection = new List<ReceiptBase>();
        }

        public ICollection<ReceiptBase> Collection { get; }

        public void Save(ReceiptBase objectForSaving)
        {
            ReceiptBase oldObject = Collection.ToList().Find(receipt => receipt.Id == objectForSaving.Id);
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

        public IEnumerable<ReceiptBase> GetAll()
        {
            return Collection;
        }

        public ReceiptBase Get(Guid id)
        {
            return Collection.ToList().Find(receipt => receipt.Id == id);
        }
    }
}