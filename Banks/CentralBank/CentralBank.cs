using System;
using Banks.Bank;
using Banks.Repository;
using Banks.Transaction;

namespace Banks.CentralBank
{
    public class CentralBank
    {
        private IRepository<IBank> _banks;

        public CentralBank(IRepository<IBank> banks)
        {
            _banks = banks;
        }

        public IRepository<ITransaction> Transactions { get; set; }

        public void AddBank(IBank newBank)
        {
            _banks.Save(newBank);
        }

        public void Transaction(float cash, Guid fromReceipt, Guid toReceipt, Guid fromBank, Guid toBank)
        {
            var fromReceiptObject = _banks.Get(fromBank).ReceiptsRepository.Get(fromReceipt);
            var toReceiptObject = _banks.Get(toBank).ReceiptsRepository.Get(toReceipt);
            float commission = 0;
            if (fromReceiptObject.Name == Conditions.Names.CreditCommission && fromReceiptObject.Cash < 0)
                commission = _banks.Get(fromBank).Conditions.CreditCommission;
            var transaction = new RegularTransaction(cash, fromReceipt, toReceipt, fromBank, toBank, commission);
            _banks.Get(fromBank).SendExternalTransfer(transaction, toReceiptObject);
            _banks.Get(fromBank).ReceiveExternalTransfer(transaction);
        }
    }
}