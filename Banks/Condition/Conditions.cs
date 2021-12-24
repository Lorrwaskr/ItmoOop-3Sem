using System;

#pragma warning disable SA1602
namespace Banks.Bank
{
    public class Conditions
    {
        public Conditions(float creditLimit = 50000, float debitInterest = 0.02F, float depositInterest1 = 0.03F, float depositInterest2 = 0.35F, float depositInterest3 = 0.04F, float creditCommission = 1000)
        {
            if (CreditLimit >= 0)
                throw new ArgumentException("CreditReceipt limit should be < 0");
            CreditLimit = creditLimit;
            DebitInterest = debitInterest;
            DepositInterest1 = depositInterest1;
            DepositInterest2 = depositInterest2;
            DepositInterest3 = depositInterest3;
            CreditCommission = creditCommission;
        }

        public enum Names
        {
            DebitInterest,
            DepositInterest1,
            DepositInterest2,
            DepositInterest3,
            CreditCommission,
            CreditLimit,
        }

        public float DebitInterest { get; set; }
        public float DepositInterest1 { get; set; }
        public float DepositInterest2 { get; set; }
        public float DepositInterest3 { get; set; }
        public float CreditCommission { get; set; }
        public float CreditLimit { get; set; }

        public float Calculate(Names name, float money = 0)
        {
            switch (name)
            {
                case Names.DebitInterest:
                    return DebitInterest;
                case Names.CreditCommission:
                    return CreditCommission;
                case Names.CreditLimit:
                    return CreditLimit;
                default:
                {
                    if (money < 50000)
                        return DepositInterest1;
                    if (money > 50000 && money < 100000)
                        return DepositInterest2;
                    return DepositInterest3;
                }
            }
        }

        public void ChangeCondition(Names newName, float value)
        {
            switch (newName)
            {
                case Names.DebitInterest:
                    DebitInterest = value;
                    break;
                case Names.CreditCommission:
                    CreditCommission = value;
                    break;
                case Names.DepositInterest1:
                    DepositInterest1 = value;
                    break;
                case Names.DepositInterest2:
                    DepositInterest2 = value;
                    break;
                case Names.DepositInterest3:
                    DepositInterest3 = value;
                    break;
            }
        }
    }
}