using System;
using Banks.Client;
using Banks.Notification;
using Banks.Receipt;
using Banks.Repository;
using Banks.Transaction;

namespace Banks.Bank
{
    public class RegularBank : IBank
    {
        public RegularBank(INotification notification)
        {
            Notification = notification;
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
        public Guid Id { get; }
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
            TransactionsRepository.Save(new RegularTransaction(money, Guid.Empty, receiptId, Guid.Empty, Id));
        }

        public void InternalTransfer(float money, Guid fromReceipt, Guid toReceipt)
        {
            var receipt = ReceiptsRepository.Get(fromReceipt);
            receipt.SendTransfer(money, ReceiptsRepository.Get(toReceipt));
            float commission = 0;
            if (receipt.Name == Conditions.Names.CreditCommission && receipt.Cash < 0)
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

        public void ChangeCondition(Conditions.Names name, float newValue)
        {
            Conditions.ChangeCondition(name, newValue);
            foreach (var receipt in ReceiptsRepository.GetAll())
            {
                if (receipt.Name != name)
                    continue;
                Notification.Notify(ClientsRepository.Get(receipt.ClientId), ClientsRepository.Get(receipt.ClientId).NotificationAddress);
            }
        }
    }
}