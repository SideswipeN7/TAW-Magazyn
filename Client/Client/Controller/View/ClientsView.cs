using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Controller.View
{
    class ClientsView:IViewController
    {
        private Admin _window { get; set; }
        private static ClientsView _instance;

        private ClientsView()
        {
        }
        public static ClientsView GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new ClientsView();
            }
            _instance._window = window;
            return _instance;
        }

        public void ShowAll()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
        }

        public void ShowSearch()
        {
            _window.GridClientsSearch.Visibility = Visibility.Visible;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Visible;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
        }

        public void ShowAdd()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Visible;
        }

        public void ShowDelete()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Visible;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
        }

        public void ShowModify()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Visible;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
        }
    }
}
