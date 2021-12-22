using System;
using System.Collections.Generic;
using Banks.Bank;

namespace Banks.Repository
{
    public class BankRepository : IRepository<IBank>
    {
        private List<IBank> _banks;

        public BankRepository()
        {
            _banks = new List<IBank>();
        }

        public void Save(IBank objectForSaving)
        {
            IBank oldObject = _banks.Find(bank => bank.Id == objectForSaving.Id);
            if (oldObject == null)
            {
                _banks.Add(objectForSaving);
            }
            else
            {
                _banks.Add(objectForSaving);
                _banks.Remove(oldObject);
            }
        }

        public IEnumerable<IBank> GetAll()
        {
            return _banks;
        }

        public IBank Get(Guid id)
        {
            return _banks.Find(bank => bank.Id == id);
        }
    }
}