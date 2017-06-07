using Client.Model;
using System.Collections.Generic;

namespace Client.Interfaces
{
    public interface ICommEmployee
    {
        bool RegisterEmployee(PracownikAdress PracownikAdres);

        void ModifyEmployee(PracownikAdress PracownikAdres);

        void DeleteEmployee(int idPracownika);

        IEnumerable<Pracownik> GetEmpoyees();
    }
}