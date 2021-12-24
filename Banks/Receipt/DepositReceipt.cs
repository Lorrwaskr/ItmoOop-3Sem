using System;
using Banks.Bank;
using Banks.Client;

namespace Banks.Receipt
{
    public class DepositReceipt : ReceiptBase
    {
        public DepositReceipt(IClient client, float cash, Conditions condition)
            : base(client, cash, condition.Calculate(Conditions.Names.DepositInterest1, cash), Conditions.Names.DepositInterest1, condition)
        {
        }

        public override void Withdraw(float money)
        {
            throw new Exception("You can't withdraw money from the deposit receipt");
        }

        public override void SendTransfer(float money, ReceiptBase toReceipt)
        {
            throw new Exception("You can't withdraw money from the deposit receipt");
        }
    }
}