using System;
using Banks.Bank;
using Banks.Client;

namespace Banks.Receipt
{
    public class CreditReceipt : ReceiptBase
    {
        public CreditReceipt(IClient client, float cash, float limit, Conditions condition)
            : base(client, cash, 0, Conditions.Names.CreditCommission, condition, limit)
        {
            CreditLimit = condition.CreditLimit;
            Commission = condition.CreditCommission;
        }

        public float CreditLimit { get; private set; }
        public float Commission { get; private set; }

        public void ChangeCreditLimit(float newLimit)
        {
            if (newLimit >= 0)
                throw new ArgumentException("CreditReceipt limit should be < 0");
            CreditLimit = newLimit;
        }

        public void ChangeCommission(float newCommission)
        {
            Commission = newCommission;
        }

        public override void Withdraw(float money)
        {
            if (Cash <= CreditLimit)
                throw new Exception("This CreditReceipt don't have enough money");
            if (Cash < 0)
            {
                base.Withdraw(money + Commission);
            }
            else
            {
                base.Withdraw(money);
            }
        }

        public override void SendTransfer(float money, ReceiptBase toReceipt)
        {
            if (Cash <= CreditLimit)
                throw new Exception("This CreditReceipt don't have enough money");
            if (money < 0)
                throw new ArgumentException("Transfer amount should be >= 0");
            CheckIfFitsIntoLimit(money);
            if (Cash < 0)
            {
                Cash -= money + Commission;
                toReceipt.GetTransfer(money);
            }
            else
            {
                Cash -= money;
                toReceipt.GetTransfer(money);
            }
        }
    }
}