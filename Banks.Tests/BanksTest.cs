using System.Linq;
using Banks.Bank;
using Banks.CentralBank;
using Banks.Client;
using Banks.Condition;
using Banks.Notification;
using Banks.Repository;
using Banks.Transaction;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest
    {
        private ICentralBank _centralBank;
        
        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank.CentralBank(new BankRepository(), new TransactionRepository());
        }

        [Test]
        public void CreateBankAndAddClients()
        {
            var bank1 = new RegularBank(new RegularNotification(), "ITMOBank");
            _centralBank.AddBank(bank1);
            Assert.That(_centralBank.Banks.Collection.Contains(bank1));
            var client1 = new RegularClient("Gusein", "Gasanov");
            var client2 = new RegularClient("Vladimir", "Putin");
            bank1.AddClient(client1);
            bank1.AddClient(client2);
            Assert.That(bank1.ClientsRepository.Collection.Contains(client1));
            Assert.That(bank1.ClientsRepository.Collection.Contains(client2));
        }

        [Test]
        public void WithdrawMoneyFromReceipt_MoneyRemoved()
        {
            var bank1 = new RegularBank(new RegularNotification(), "ITMOBank");
            _centralBank.AddBank(bank1);
            var client1 = new RegularClient("Gusein", "Gasanov");
            bank1.AddClient(client1);
            bank1.AddReceipt(client1.Id, Conditions.ReceiptType.Debit);
            bank1.Deposit(100000, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id);
            bank1.Withdraw(12345, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id);
            Assert.AreEqual(100000 - 12345, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Cash);
            Assert.AreEqual(bank1.Id,
                bank1.FindTransactions(bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id).First()
                    .ToBank);
        }

        [Test]
        public void Transaction()
        {
            var bank1 = new RegularBank(new RegularNotification(), "ITMOBank");
            _centralBank.AddBank(bank1);
            var client1 = new RegularClient("Gusein", "Gasanov");
            var client2 = new RegularClient("Vladimir", "Putin");
            bank1.AddClient(client1);
            bank1.AddClient(client2);
            bank1.AddReceipt(client1.Id, Conditions.ReceiptType.Debit);
            bank1.AddReceipt(client2.Id, Conditions.ReceiptType.Debit);
            bank1.Deposit(100000, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id);
            bank1.InternalTransfer(22222, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id,
                bank1.FindReceipt(client2.Id, Conditions.ReceiptType.Debit).Id);
            Assert.AreEqual(100000 - 22222, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Cash);
            Assert.AreEqual(22222, bank1.FindReceipt(client2.Id, Conditions.ReceiptType.Debit).Cash);
            ITransaction transaction = bank1
                .FindTransactions(bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id).First(
                    transaction => transaction.ToReceipt ==
                                   bank1.FindReceipt(client2.Id, Conditions.ReceiptType.Debit).Id);
            Assert.AreEqual(bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id, transaction.FromReceipt);
            Assert.AreEqual(bank1.FindReceipt(client2.Id, Conditions.ReceiptType.Debit).Id, transaction.ToReceipt);
        }

        [Test]
        public void CancelTransaction()
        {
            var bank1 = new RegularBank(new RegularNotification(), "ITMOBank");
            _centralBank.AddBank(bank1);
            var client1 = new RegularClient("Gusein", "Gasanov");
            var client2 = new RegularClient("Vladimir", "Putin");
            bank1.AddClient(client1);
            bank1.AddClient(client2);
            bank1.AddReceipt(client1.Id, Conditions.ReceiptType.Debit);
            bank1.AddReceipt(client2.Id, Conditions.ReceiptType.Debit);
            bank1.Deposit(100000, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id);
            bank1.InternalTransfer(22222, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id,
                bank1.FindReceipt(client2.Id, Conditions.ReceiptType.Debit).Id);
            ITransaction transaction = bank1
                .FindTransactions(bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Id).First(
                    transaction => transaction.ToReceipt ==
                                   bank1.FindReceipt(client2.Id, Conditions.ReceiptType.Debit).Id);
            bank1.CancelTransaction(transaction.Id);
            Assert.AreEqual(100000, bank1.FindReceipt(client1.Id, Conditions.ReceiptType.Debit).Cash);
            Assert.AreEqual(0, bank1.FindReceipt(client2.Id, Conditions.ReceiptType.Debit).Cash);
        }
    }
}   