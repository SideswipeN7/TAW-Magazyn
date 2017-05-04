using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    interface IManager
    {
        void UpdateClientData();
        void UpdateMagazineState();
        void UpdateCategoryData();
        void UpdateClientTransactionData();
        void UpdateItemData();
        void GetClientData();
        void GetMagazineState();
        void GetCategoryData();
        void GetClientTransactionData();
        void GetItemData();
        void SetClientData();
        void SetCategoryData();
        void SetClientTransactionData();
        void SetItemData();
        void ChangeClientData();
        void ChangeCategoryData();
        void ChangeItemData();
    }
}
