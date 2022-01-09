using System;
using Banks.Bank;
using Banks.Client;
using Banks.Condition;

namespace Banks.Receipt
{
    public class DebitReceipt : ReceiptBase
    {
        public DebitReceipt(IClient client, float limit, Conditions condition, float cash = 0)
            : base(client, cash, condition.DebitInterest, Conditions.ReceiptType.Debit, condition, limit)
        {
        }

        public override void Withdraw(float money)
        {
            if (Cash < money)
                throw new ArgumentException("DebitReceipt cash should be >= 0");
            base.Withdraw(money);
        }

        public override void SendTransfer(float money, ReceiptBase toReceipt)
        {
            if (Cash < money)
                throw new ArgumentException("DebitReceipt cash should be >= 0");
            base.SendTransfer(money, toReceipt);
        }
    }
}