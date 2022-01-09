using System;
using System.Collections.Generic;

namespace Banks.Condition
{
    public class Conditions
    {
        public Conditions(List<float> depositInterest = null, float creditLimit = -50000, float debitInterest = 0.02F, float creditCommission = 1000)
        {
            if (creditLimit >= 0)
                throw new ArgumentException("CreditReceipt limit should be < 0");
            CreditLimit = creditLimit;
            DebitInterest = debitInterest;
            if (depositInterest == null)
            {
                DepositInterest = new List<float> { 0.03f, 0.035f, 0.04f };
            }

            DepositInterest = depositInterest;
            CreditCommission = creditCommission;
        }

        public enum ReceiptType
        {
            /// <summary>
            /// Debit
            /// </summary>
            Debit,

            /// <summary>
            /// Deposit
            /// </summary>
            Deposit,

            /// <summary>
            /// Credit
            /// </summary>
            Credit,
        }

        public float DebitInterest { get; set; }
        public List<float> DepositInterest { get; set; }
        public float CreditCommission { get; set; }
        public float CreditLimit { get; set; }

        public float CalculateDepositInterest(float money)
        {
            if (money < 50000)
                return DepositInterest[0];
            if (money > 50000 && money < 100000)
                return DepositInterest[1];
            return DepositInterest[2];
        }
    }
}