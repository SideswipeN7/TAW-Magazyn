using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    interface ICommEmployee
    {
        bool RegisterEmployee(PracownikAdress PracownikAdres);
        void ModifyEmployee(PracownikAdress PracownikAdres);
        void DeleteEmployee(int idPracownika);
        IEnumerable<Pracownik> GetEmpoyees();
    }
}
