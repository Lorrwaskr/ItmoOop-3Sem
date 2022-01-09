using System;
using Banks.Bank;
using Banks.Repository;
using Banks.Transaction;

namespace Banks.CentralBank
{
    public interface ICentralBank
    {
        public IRepository<ITransaction> Transactions { get; }
        public IRepository<IBank> Banks { get; }

        public void AddBank(IBank newBank);

        public void Transaction(float cash, Guid fromReceipt, Guid toReceipt, Guid fromBank, Guid toBank);
    }
}