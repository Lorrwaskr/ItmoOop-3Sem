using Banks.Bank;
using Banks.Repository;

namespace Banks.CentralBank
{
    public class CentralBank
    {
        private IRepository<IBank> _banks;

        public CentralBank(IRepository<IBank> banks)
        {
            _banks = banks;
        }

        public void AddBank(IBank newBank)
        {
            _banks.Save(newBank);
        }
    }
}