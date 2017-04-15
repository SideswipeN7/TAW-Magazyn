using System.Collections.Generic;
using WebServer.Models;

namespace WebServer.Interfaces
{
    internal interface ICommunication
    {
        IEnumerable<Artykul_w_transakcji> GetTransItems();

        bool RegisterTransItems(IEnumerable<Artykul_w_transakcji> artykul_w_transkacji);

        bool ChangeSupplier(Dostawca dostawca);

        int GetSupplier(int id);

        IEnumerable<Dostawca> GetSuppliers();

        bool RegisterSupplier(Dostawca dostawca);

        IEnumerable<Artykul> GetItems();

        bool ChangeClient(Klient klient);

        bool RegisterClient(Klient klient, Adres adres);

        bool RegisterEmployee(Pracownik pracownik, Adres adres);

        Adres GetAddress(int id);

        bool ChangeItem(Artykul artykul);

        bool RegisterItem(Artykul artykul);

        bool ChangeAddress(Adres adres);

        int RegisterAddress(Adres adres);

        bool RegisterCategory(Kategoria kategoria);

        IEnumerable<Kategoria> GetCategories();

        bool ChangeCategory(Kategoria kategoria);

        int RegisterTransaction(Transakcja transakcja);

        IEnumerable<Transakcja> IEnumerableGetTransactions();

        int GetTransaction(int id);
    }
}