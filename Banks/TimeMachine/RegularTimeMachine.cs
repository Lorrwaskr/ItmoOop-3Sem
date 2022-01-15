using System;
using Banks.Receipt;

namespace Banks.TimeMachine
{
    public class RegularTimeMachine
    {
        public static ReceiptBase Run(ReceiptBase receipt, int daysToAddInterest, DateTime @from, DateTime to)
        {
            int days = to.Subtract(from).Days;
            int daysInterest = daysToAddInterest;
            ReceiptBase result = receipt.Clone();
            while (days > 1)
            {
                daysInterest--;
                days--;
                if (daysInterest <= 0)
                {
                    result.AddInterest();
                    daysInterest = 31;
                }

                result.UpdateInterest();
            }

            return result;
        }
    }
}