using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Model;

namespace Client.Communication
{
    class Communicator : ICommunication
    {
        string urlAddress;
        public void SetUrlAddress(string URL)
        {
            urlAddress = URL;
        }

        public bool ChangeAddress(Adres adres)
        {
            throw new NotImplementedException();
        }

        public bool ChangeCategory(Kategoria kategoria)
        {
            throw new NotImplementedException();
        }

        public bool ChangeClient(Klient klient)
        {
            throw new NotImplementedException();
        }

        public bool ChangeItem(Artykul artykul)
        {
            throw new NotImplementedException();
        }

        public bool ChangeSupplier(Dostawca dostawca)
        {
            throw new NotImplementedException();
        }

        public Adres GetAddress(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Kategoria> GetCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Artykul> GetItems()
        {
            throw new NotImplementedException();
        }

        public int GetSupplier(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dostawca> GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public int GetTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Artykul_w_transakcji> GetTransItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transakcja> IEnumerableGetTransactions()
        {
            throw new NotImplementedException();
        }

        public int RegisterAddress(Adres adres)
        {
            throw new NotImplementedException();
        }

        public bool RegisterCategory(Kategoria kategoria)
        {
            throw new NotImplementedException();
        }

        public bool RegisterClient(Klient klient, Adres adres)
        {
            throw new NotImplementedException();
        }

        public bool RegisterEmployee(Pracownik pracownik, Adres adres)
        {
            throw new NotImplementedException();
        }

        public bool RegisterItem(Artykul artykul)
        {
            throw new NotImplementedException();
        }

        public bool RegisterSupplier(Dostawca dostawca)
        {
            throw new NotImplementedException();
        }

        public int RegisterTransaction(Transakcja transakcja)
        {
            throw new NotImplementedException();
        }

        public bool RegisterTransItems(IEnumerable<Artykul_w_transakcji> artykul_w_transkacji)
        {
            throw new NotImplementedException();
        }
    }
}
