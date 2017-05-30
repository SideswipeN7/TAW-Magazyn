using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    class MagazineController : IWork
    {
        private Admin _window { get; set; }
        private static MagazineController _instance;

        private MagazineController() { }

        public static MagazineController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new MagazineController();
            }
            _instance._window = window;
            return _instance;
        }

        public void AddData()
        {
            throw new NotImplementedException();
        }

        public void ChangeDate()
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
    }
}
