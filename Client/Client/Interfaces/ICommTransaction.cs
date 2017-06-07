using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    interface ICommTransaction
    {
        int RegisterTransaction(Transakcja transakcja);

        IEnumerable<Transakcja> GetTransactions();
    }
}
