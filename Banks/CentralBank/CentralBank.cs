using System;
using Banks.Bank;
using Banks.Condition;
using Banks.Repository;
using Banks.Transaction;

namespace Banks.CentralBank
{
    public class CentralBank : ICentralBank
    {
        public CentralBank(IRepository<IBank> banks, IRepository<ITransaction> transactions)
        {
            Banks = banks;
            Transactions = transactions;
        }

        public IRepository<ITransaction> Transactions { get; }
        public IRepository<IBank> Banks { get; }

        public void AddBank(IBank newBank)
        {
            Banks.Save(newBank);
        }

        public void Transaction(float cash, Guid fromReceipt, Guid toReceipt, Guid fromBank, Guid toBank)
        {
            var fromReceiptObject = Banks.Get(fromBank).ReceiptsRepository.Get(fromReceipt);
            var toReceiptObject = Banks.Get(toBank).ReceiptsRepository.Get(toReceipt);
            float commission = 0;
            if (fromReceiptObject.ReceiptType == Conditions.ReceiptType.Credit && fromReceiptObject.Cash < 0)
                commission = Banks.Get(fromBank).Conditions.CreditCommission;
            var transaction = new RegularTransaction(cash, fromReceipt, toReceipt, fromBank, toBank, commission);
            Banks.Get(fromBank).SendExternalTransfer(transaction, toReceiptObject);
            Banks.Get(fromBank).ReceiveExternalTransfer(transaction);
        }
    }
}