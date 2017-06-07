using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    interface ICommClient
    {
        bool ChangeClient(Klient klient);
        int RegisterClient(KlientAdress klientAdres);
        IEnumerable<Klient> GetClients();
        void DeleteClient(int id);
        bool ChangeAddress(Adres adres);
    }
}
