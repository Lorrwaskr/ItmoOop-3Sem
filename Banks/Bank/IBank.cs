using System;
using Banks.Client;
using Banks.Notification;
using Banks.Receipt;
using Banks.Repository;
using Banks.Transaction;

namespace Banks.Bank
{
    public interface IBank
    {
        IRepository<IClient> ClientsRepository { get; }
        IRepository<ReceiptBase> ReceiptsRepository { get; }
        IRepository<ITransaction> TransactionsRepository { get; }
        Guid Id { get; }
        INotification Notification { get; }
        Conditions Conditions { get; }
        void AddInterest();
        void UpdateInterest();
        void Deposit(float money, Guid receiptId);
        void Withdraw(float money, Guid receiptId);
        void InternalTransfer(float money, Guid fromReceipt, Guid toReceipt);
        void SendExternalTransfer(ITransaction transaction, ReceiptBase toReceipt);
        void ReceiveExternalTransfer(ITransaction transaction);
        void CancelTransaction(Guid transaction);
        void ChangeCondition(Conditions.Names name, float newValue);
    }
}