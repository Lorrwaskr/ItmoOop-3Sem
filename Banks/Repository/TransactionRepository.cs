using System;
using System.Collections.Generic;
using Banks.Receipt;
using Banks.Transaction;

namespace Banks.Repository
{
    public class TransactionRepository : IRepository<ITransaction>
    {
            private List<ITransaction> _transactions;

            public TransactionRepository()
            {
                _transactions = new List<ITransaction>();
            }

            public void Save(ITransaction objectForSaving)
            {
                ITransaction oldObject = _transactions.Find(transaction => transaction.Id == objectForSaving.Id);
                if (oldObject == null)
                {
                    _transactions.Add(objectForSaving);
                }
                else
                {
                    _transactions.Add(objectForSaving);
                    _transactions.Remove(oldObject);
                }
            }

            public IEnumerable<ITransaction> GetAll()
            {
                return _transactions;
            }

            public ITransaction Get(Guid id)
            {
                return _transactions.Find(transaction => transaction.Id == id);
            }
        }
    }