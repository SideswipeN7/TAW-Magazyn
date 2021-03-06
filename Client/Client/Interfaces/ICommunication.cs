﻿using Client.Model;
using System.Collections.Generic;

namespace Client.Communication
{
    public interface ICommunication
    {
        void SetUrlAddress(string URL);

        IEnumerable<Artykul_w_transakcji> GetTransItems();

        bool RegisterTransItems(IEnumerable<Artykul_w_transakcji> artykul_w_transkacji);

        bool ChangeSupplier(Dostawca dostawca);

        Dostawca GetSupplier(int id);

        IEnumerable<Dostawca> GetSuppliers();

        bool RegisterSupplier(Dostawca dostawca);

        IEnumerable<Artykul> GetItems();

        bool ChangeClient(Klient klient);

        int RegisterClient(KlientAdress adres);

        bool RegisterEmployee(PracownikAdress adres);

        Adres GetAddress(int id);

        bool ChangeItem(Artykul artykul);

        bool RegisterItem(Artykul artykul);

        bool ChangeAddress(Adres adres);

        int RegisterAddress(Adres adres);

        bool RegisterCategory(Kategoria kategoria);

        IEnumerable<Kategoria> GetCategories();

        bool ChangeCategory(Kategoria kategoria);

        int RegisterTransaction(Transakcja transakcja);

        IEnumerable<Transakcja> GetTransactions();

        Transakcja GetTransaction(int id);

        IEnumerable<Klient> GetClients();

        void DeleteCategory(Kategoria selectedItem);

        void DeleteClient(int id);

        void ModifyEmployee(PracownikAdress adres);

        void DeleteEmployee(int idPracownika);

        IEnumerable<Pracownik> GetEmpoyees();

        void DeleteItem(int idArtykulu);
    }
}