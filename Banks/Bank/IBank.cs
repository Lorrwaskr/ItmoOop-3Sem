using System;
using System.Collections.Generic;
using Banks.Client;
using Banks.Condition;
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
        string Name { get; }
        void AddClient(IClient client);
        void AddReceipt(Guid clientId, Conditions.ReceiptType receiptType, float cash = 0);
        ReceiptBase FindReceipt(Guid clientId, Conditions.ReceiptType receiptType);
        IEnumerable<ReceiptBase> GetReceipts(Guid clientId);
        IEnumerable<ITransaction> FindTransactions(Guid receiptId);
        void AddInterest();
        void UpdateInterest();
        void Deposit(float money, Guid receiptId);
        void Withdraw(float money, Guid receiptId);
        void InternalTransfer(float money, Guid fromReceipt, Guid toReceipt);
        void SendExternalTransfer(ITransaction transaction, ReceiptBase toReceipt);
        void ReceiveExternalTransfer(ITransaction transaction);
        void CancelTransaction(Guid transaction);
        void ChangeDebitInterest(float newValue);
        void ChangeDepositInterest(List<float> newValue);
        void ChangeCreditCommission(float newValue);
        void ChangeCreditLimit(float newValue);
    }
}