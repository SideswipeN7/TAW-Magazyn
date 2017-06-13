using Client.Model;
using System.Collections.Generic;

namespace Client.Interfaces
{
    public interface ICommTransaction
    {
        int RegisterTransaction(Transakcja transakcja);

        IEnumerable<Transakcja> GetTransactions();
    }
}