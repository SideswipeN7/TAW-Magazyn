using Client.Model;
using System.Collections.Generic;

namespace Client.Interfaces
{
    public interface ICommSupplier
    {
        IEnumerable<Dostawca> GetSuppliers();
    }
}