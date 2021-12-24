using System;
using System.Collections.Generic;
using Banks.Receipt;

namespace Banks.Repository
{
    public class ReceiptRepository : IRepository<ReceiptBase>
    {
        private List<ReceiptBase> _receipts;

        public ReceiptRepository()
        {
            _receipts = new List<ReceiptBase>();
        }

        public void Save(ReceiptBase objectForSaving)
        {
            ReceiptBase oldObject = _receipts.Find(receipt => receipt.Id == objectForSaving.Id);
            if (oldObject == null)
            {
                _receipts.Add(objectForSaving);
            }
            else
            {
                _receipts.Add(objectForSaving);
                _receipts.Remove(oldObject);
            }
        }

        public IEnumerable<ReceiptBase> GetAll()
        {
            return _receipts;
        }

        public ReceiptBase Get(Guid id)
        {
            return _receipts.Find(receipt => receipt.Id == id);
        }
    }
}