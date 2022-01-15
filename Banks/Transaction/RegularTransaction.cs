using System;

namespace Banks.Transaction
{
    public class RegularTransaction : ITransaction
    {
        public RegularTransaction(float cash, Guid fromReceipt, Guid toReceipt, Guid fromBank, Guid toBank, float commission = 0)
        {
            if (cash <= 0)
                throw new ArgumentException("Transaction amount should be > 0");
            Cash = cash;
            FromReceipt = fromReceipt;
            ToReceipt = toReceipt;
            FromBank = fromBank;
            ToBank = toBank;
            Commission = commission;
            Id = Guid.NewGuid();
            Time = DateTime.Now;
            WasCancelled = false;
        }

        public float Cash { get; }
        public float Commission { get; }
        public Guid Id { get; }
        public Guid FromReceipt { get; }
        public Guid ToReceipt { get; }
        public Guid FromBank { get; }
        public Guid ToBank { get; }
        public DateTime Time { get; }
        public bool WasCancelled { get; set; }
    }
}