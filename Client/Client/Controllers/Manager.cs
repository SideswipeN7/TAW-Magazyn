using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public sealed class Manager :IManager
    {
        private Manager _instance;
        public Manager GetInstance()
        {
            if (_instance == null)
                _instance = new Manager();
            return _instance;
        }


        public void ChangeCategoryData()
        {
            throw new NotImplementedException();
        }

        public void ChangeClientData()
        {
            throw new NotImplementedException();
        }

        public void ChangeItemData()
        {
            throw new NotImplementedException();
        }

        public void GetCategoryData()
        {
            throw new NotImplementedException();
        }

        public void GetClientData()
        {
            throw new NotImplementedException();
        }

        public void GetClientTransactionData()
        {
            throw new NotImplementedException();
        }



        public void GetItemData()
        {
            throw new NotImplementedException();
        }

        public void GetMagazineState()
        {
            throw new NotImplementedException();
        }

        public void SetCategoryData()
        {
            throw new NotImplementedException();
        }

        public void SetClientData()
        {
            throw new NotImplementedException();
        }

        public void SetClientTransactionData()
        {
            throw new NotImplementedException();
        }

        public void SetItemData()
        {
            throw new NotImplementedException();
        }

        public void UpdateCategoryData()
        {
            throw new NotImplementedException();
        }

        public void UpdateClientData()
        {
            throw new NotImplementedException();
        }

        public void UpdateClientTransactionData()
        {
            throw new NotImplementedException();
        }

        public void UpdateItemData()
        {
            throw new NotImplementedException();
        }

        public void UpdateMagazineState()
        {
            throw new NotImplementedException();
        }
    }
}
