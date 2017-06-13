using Client.Model;
using System.Collections.Generic;

namespace Client.Interfaces
{
    public interface ICommClient
    {
        bool ChangeClient(Klient klient);

        int RegisterClient(KlientAdress klientAdres);

        IEnumerable<Klient> GetClients();

        void DeleteClient(int id);

        bool ChangeAddress(Adres adres);
    }
}