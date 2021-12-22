using System;
using Banks.Client;
using Banks.Receipt;
using Banks.Repository;
using Banks.Transaction;

namespace Banks.Bank
{
    public interface IBank
    {
        IRepository<IClient> ClientsRepository { get; set; }
        IRepository<IReceipt> ReceiptsRepository { get; set; }
        IRepository<ITransaction> TransactionsRepository { get; set; }
        Guid Id { get; set; }
        void AddInterestToAccountBalance();
        void WithdrawMoney(Guid receiptId);
    }
}