using System;
using Banks.Bank;
using Banks.Client;
using Banks.Condition;
using Banks.Transaction;

namespace Banks.Receipt
{
    public abstract class ReceiptBase : ICloneable
    {
        protected ReceiptBase(IClient client, float cash, float interestPercent, Conditions.ReceiptType receiptType, Conditions condition, float limit = float.MaxValue)
        {
            if (Limit < 0)
                throw new ArgumentException("Limit should be >= 0");
            ClientId = client.Id;
            Cash = cash;
            InterestPercent = interestPercent;
            Interest = 0;
            ReceiptType = receiptType;
            Condition = condition;
            Limit = float.MaxValue;
            IsLimited = false;
            SpentInPeriod = 0;
            Id = Guid.NewGuid();
            if (!client.IsTrustworthy())
            {
                Limit = limit;
                IsLimited = true;
            }
        }

        public float Cash { get; protected set; }
        public Conditions.ReceiptType ReceiptType { get; }
        public Conditions Condition { get; }
        public float Interest { get; protected set; }
        public float InterestPercent { get; protected set; }
        public Guid Id { get; }
        public Guid ClientId { get; }
        public bool IsLimited { get; set; }
        public float Limit { get; protected set; }
        public float SpentInPeriod { get; protected set; }

        public void ReturnMoney(ITransaction transaction)
        {
            Cash += transaction.Cash * (1 - transaction.Commission);
        }

        public void RemoveMoney(ITransaction transaction)
        {
            Cash -= transaction.Cash;
        }

        public virtual void AddInterest()
        {
            Cash += Interest;
        }

        public virtual void UpdateInterest()
        {
            Interest += Cash * InterestPercent;
        }

        public virtual void NullifySpentInPeriod()
        {
            SpentInPeriod = 0;
        }

        public virtual void Deposit(float money)
        {
            if (money < 0)
                throw new ArgumentException("Deposit amount should be >= 0");
            Cash += money;
        }

        public virtual void Withdraw(float money)
        {
            if (money < 0)
                throw new ArgumentException("Withdraw amount should be >= 0");
            CheckIfFitsIntoLimit(money);
            Cash -= money;
        }

        public virtual void SendTransfer(float money, ReceiptBase toReceipt)
        {
            if (money < 0)
                throw new ArgumentException("Transfer amount should be >= 0");
            CheckIfFitsIntoLimit(money);
            Cash -= money;
            toReceipt.GetTransfer(money);
        }

        public virtual void GetTransfer(float money)
        {
            if (money < 0)
                throw new ArgumentException("Transfer amount should be >= 0");
            Cash += money;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public virtual ReceiptBase Clone()
        {
            return MemberwiseClone() as ReceiptBase;
        }

        protected void CheckIfFitsIntoLimit(float money)
        {
            if (!IsLimited)
                return;
            if (SpentInPeriod + money > Limit)
                throw new Exception("Limit is exceeded");
            SpentInPeriod += money;
        }
    }
}