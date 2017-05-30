using Client.Communication;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    class ItemsController : IWork
    {
        private static ItemsController _instance;
        Admin _window { get; set; }
        private ICommunication _comm;
        List<Artykul> art;

        private ItemsController()
        {
            _comm = Communicator.GetInstance();
             _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
            //_comm.SetUrlAddress("http://localhost:52992");
        }
        public static ItemsController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new ItemsController();
            }
            _instance._window = window;            
            return _instance;
        }

        public void AddData()
        {
        }

        public void ChangeData()
        {
            throw new NotImplementedException();
        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        public void ShowData()
        {
            throw new NotImplementedException();
        }

        public void ShowSelectedData()
        {
            throw new NotImplementedException();
        }

        public void SearchData()
        {
        }

        public void GetData()
        {
            
        }
    }
}
