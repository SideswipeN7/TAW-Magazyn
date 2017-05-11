using Client.Model;
using System.Collections.Generic;

namespace Client.Controller
{
    interface IManager
    {
        void ShowClientData();
        void ShowMagazineStateData(IEnumerable<Artykul> categories);
        void ShowCategoryData(IEnumerable<Kategoria> categories);
        void ShowClientTransactionData(IEnumerable<Transakcja> transactions);
        void ShowItemData(IEnumerable<Artykul> categories);
        void GetClientData();
        void GetMagazineState();
        void GetCategoryData();
        void GetClientTransactionData();
        void GetItemData();
        void SetClientData();
        void SetCategoryData();       
        void SetItemData();
        void ChangeClientData();
        void ChangeCategoryData();
        void ChangeItemData();
        void SelectClientDoTransactionSurname();
        void SelectClientDoTransactionFirm();
    }
}
