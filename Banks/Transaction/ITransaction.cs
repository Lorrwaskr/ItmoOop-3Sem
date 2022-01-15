using System;

namespace Banks.Transaction
{
    public interface ITransaction
    {
        float Cash { get; }
        float Commission { get; }
        Guid Id { get; }
        Guid FromReceipt { get; }
        Guid ToReceipt { get; }
        Guid FromBank { get; }
        Guid ToBank { get; }
        DateTime Time { get; }
        bool WasCancelled { get; set; }
    }
}