using Banks.Bank;
using Banks.Receipt;

namespace Banks.Transaction
{
    public interface ITransaction
    {
        float Cash { get; set; }
        float Commission { get; set; }
        IReceipt FromReceipt { get; set; }
        IReceipt ToReceipt { get; set; }
        IBank FromBank { get; set; }
        IBank ToBank { get; set; }
        bool WasCancelled { get; set; }
    }
}