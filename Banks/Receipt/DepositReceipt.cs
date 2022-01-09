using System;
using Banks.Bank;
using Banks.Client;
using Banks.Condition;

namespace Banks.Receipt
{
    public class DepositReceipt : ReceiptBase
    {
        public DepositReceipt(IClient client, float cash, Conditions condition)
            : base(client, cash, condition.CalculateDepositInterest(cash), Conditions.ReceiptType.Deposit, condition)
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