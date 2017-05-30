using Client.Model;
using System.Collections.Generic;

namespace Client.Controller
{
    internal interface IManager
    {
        void ShowClientData(); 
        
        void GetEmployeeData();

        void GetClientData();

        void GetMagazineState();

        void GetCategoryData();

        void GetClientTransactionData();

        void GetItemData();

        void SetClientData();

        void SetCategoryData();

        void SetItemData();

        void SetEmployeeData();

        void ChangeClientData();

        void ChangeCategoryData();

        void ChangeItemData();

        void ChangeEmployeeData();

        void SelectClientDoTransactionSurname();

        void SelectClientDoTransactionFirm();

        void ShowFacture(Transakcja tran);
    }
}