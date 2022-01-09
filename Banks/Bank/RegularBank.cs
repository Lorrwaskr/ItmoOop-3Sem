using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Client;
using Banks.Condition;
using Banks.Notification;
using Banks.Receipt;
using Banks.Repository;
using Banks.Transaction;

namespace Banks.Bank
{
    public class RegularBank : IBank
    {
        public RegularBank(INotification notification, string name)
        {
            Notification = notification;
            Name = name;
            Id = Guid.NewGuid();
            ClientsRepository = new ClientRepository();
            ReceiptsRepository = new ReceiptRepository();
            TransactionsRepository = new TransactionRepository();
            Conditions = new Conditions();
        }

        public IRepository<IClient> ClientsRepository { get; }
        public IRepository<ReceiptBase> ReceiptsRepository { get; }
        public IRepository<ITransaction> TransactionsRepository { get; }
        public INotification Notification { get; }
        public Conditions Conditions { get; set; }
        public string Name { get; }
        public Guid Id { get; }
        public void AddClient(IClient client)
        {
            ClientsRepository.Save(client);
        }

        public void AddReceipt(Guid clientId, Conditions.ReceiptType receiptType, float cash = 0)
        {
            switch (receiptType)
            {
                case Conditions.ReceiptType.Credit:
                    ReceiptsRepository.Save(new CreditReceipt(ClientsRepository.Get(clientId), 0, Conditions, cash));
                    break;
                case Conditions.ReceiptType.Debit:
                    ReceiptsRepository.Save(new DebitReceipt(ClientsRepository.Get(clientId), 0, Conditions, cash));
                    break;
                case Conditions.ReceiptType.Deposit:
                    ReceiptsRepository.Save(new DepositReceipt(ClientsRepository.Get(clientId), cash, Conditions));
                    break;
            }
        }

        public ReceiptBase FindReceipt(Guid clientId, Conditions.ReceiptType receiptType)
        {
            return GetReceipts(clientId).First(receipt => receipt.ReceiptType == receiptType);
        }

        public IEnumerable<ReceiptBase> GetReceipts(Guid clientId)
        {
            return ReceiptsRepository.GetAll().Where(receipt => receipt.ClientId == clientId).ToList();
        }

        public IEnumerable<ITransaction> FindTransactions(Guid receiptId)
        {
            return TransactionsRepository.GetAll().Where(transaction =>
                transaction.FromReceipt == receiptId || transaction.ToReceipt == receiptId);
        }

        public void AddInterest()
        {
            foreach (var receipt in ReceiptsRepository.GetAll())
            {
                receipt.AddInterest();
            }
        }

        public void UpdateInterest()
        {
            foreach (var receipt in ReceiptsRepository.GetAll())
            {
                receipt.UpdateInterest();
            }
        }

        public void Deposit(float money, Guid receiptId)
        {
            ReceiptsRepository.Get(receiptId).Deposit(money);
            TransactionsRepository.Save(new RegularTransaction(money, Guid.Empty, receiptId, Guid.Empty, Id));
        }

        public void Withdraw(float money, Guid receiptId)
        {
            ReceiptsRepository.Get(receiptId).Withdraw(money);
            TransactionsRepository.Save(new RegularTransaction(money, receiptId, Guid.Empty, Id, Guid.Empty));
        }

        public void InternalTransfer(float money, Guid fromReceipt, Guid toReceipt)
        {
            var receipt = ReceiptsRepository.Get(fromReceipt);
            receipt.SendTransfer(money, ReceiptsRepository.Get(toReceipt));
            float commission = 0;
            if (receipt.ReceiptType == Conditions.ReceiptType.Credit && receipt.Cash < 0)
                commission = Conditions.CreditCommission;
            TransactionsRepository.Save(new RegularTransaction(money, fromReceipt, toReceipt, Id, Id, commission));
        }

        public void SendExternalTransfer(ITransaction transaction, ReceiptBase toReceipt)
        {
            var receipt = ReceiptsRepository.Get(transaction.FromReceipt);
            receipt.SendTransfer(transaction.Cash, toReceipt);
            TransactionsRepository.Save(transaction);
        }

        public void ReceiveExternalTransfer(ITransaction transaction)
        {
            TransactionsRepository.Save(transaction);
        }

        public void CancelTransaction(Guid transaction)
        {
            var transaction1 = TransactionsRepository.Get(transaction);
            if (transaction1.WasCancelled)
                throw new ArgumentException("Transaction can't be cancelled twice");
            if (transaction1.FromBank == Id)
            {
                ReceiptsRepository.Get(transaction1.FromReceipt).ReturnMoney(transaction1);
            }

            if (transaction1.ToBank == Id)
            {
                ReceiptsRepository.Get(transaction1.ToReceipt).RemoveMoney(transaction1);
            }

            transaction1.WasCancelled = true;
        }

        public void ChangeDebitInterest(float newValue)
        {
            Conditions.DebitInterest = newValue;
            foreach (var receipt in ReceiptsRepository.GetAll())
            {
                if (receipt.ReceiptType != Conditions.ReceiptType.Debit)
                    continue;
                Notification.Notify(ClientsRepository.Get(receipt.ClientId), ClientsRepository.Get(receipt.ClientId).NotificationAddress);
            }
        }

        public void ChangeDepositInterest(List<float> newValue)
        {
            Conditions.DepositInterest = newValue;
            foreach (var receipt in ReceiptsRepository.GetAll())
            {
                if (receipt.ReceiptType != Conditions.ReceiptType.Deposit)
                    continue;
                Notification.Notify(ClientsRepository.Get(receipt.ClientId), ClientsRepository.Get(receipt.ClientId).NotificationAddress);
            }
        }

        public void ChangeCreditCommission(float newValue)
        {
            Conditions.CreditCommission = newValue;
            foreach (var receipt in ReceiptsRepository.GetAll())
            {
                if (receipt.ReceiptType != Conditions.ReceiptType.Credit)
                    continue;
                Notification.Notify(ClientsRepository.Get(receipt.ClientId), ClientsRepository.Get(receipt.ClientId).NotificationAddress);
            }
        }

        public void ChangeCreditLimit(float newValue)
        {
            Conditions.CreditLimit = newValue;
            foreach (var receipt in ReceiptsRepository.GetAll())
            {
                if (receipt.ReceiptType != Conditions.ReceiptType.Credit)
                    continue;
                var receipt1 = (CreditReceipt)receipt;
                receipt1.ChangeCreditLimit(newValue);
                Notification.Notify(ClientsRepository.Get(receipt.ClientId), ClientsRepository.Get(receipt.ClientId).NotificationAddress);
            }
        }
    }
}