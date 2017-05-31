using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Controller.View
{
    class EmployeeView:IViewController
    {
        private Admin _window { get; set; }
        private static EmployeeView _instance;

        private EmployeeView()
        {
        }
        public static EmployeeView GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new EmployeeView();
            }
            _instance._window = window;
            return _instance;
        }

        public void ShowAll()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeUsun.Visibility = Visibility.Hidden;
        }

        public void ShowSearch()
        {
            throw new NotImplementedException();
        }

        public void ShowAdd()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Visible;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeUsun.Visibility = Visibility.Hidden;
        }

        public void ShowDelete()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeUsun.Visibility = Visibility.Visible;
        }

        public void ShowModify()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Visible;
            _window.BtnEmployeeUsun.Visibility = Visibility.Hidden;
        }
    }
}
