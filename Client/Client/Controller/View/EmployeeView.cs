using Client.Windows;
using System;
using System.Windows;

namespace Client.Controller.View
{
    internal class EmployeeView : IViewController
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

            _window.TxbEmployeeHaslo.IsEnabled = false;
            _window.TxbEmployeeImie.IsEnabled = false;
            _window.TxbEmployeeKodPocztowy.IsEnabled = false;
            _window.TxbEmployeeLogin.IsEnabled = false;
            _window.TxbEmployeeMiejscowosc.IsEnabled = false;
            _window.TxbEmployeeNazwisko.IsEnabled = false;
            _window.TxbEmployeeWiek.IsEnabled = false;
            _window.CmbEmployeeAdmin.IsEnabled = false;
            _window.CmbEmployeeWojewodztwo.IsEnabled = false;
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

            _window.TxbEmployeeHaslo.IsEnabled = true;
            _window.TxbEmployeeImie.IsEnabled = true;
            _window.TxbEmployeeKodPocztowy.IsEnabled = true;
            _window.TxbEmployeeLogin.IsEnabled = true;
            _window.TxbEmployeeMiejscowosc.IsEnabled = true;
            _window.TxbEmployeeNazwisko.IsEnabled = true;
            _window.TxbEmployeeWiek.IsEnabled = true;
            _window.CmbEmployeeAdmin.IsEnabled = true;
            _window.CmbEmployeeWojewodztwo.IsEnabled = true;
        }

        public void ShowDelete()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeUsun.Visibility = Visibility.Visible;

            _window.TxbEmployeeHaslo.IsEnabled = false;
            _window.TxbEmployeeImie.IsEnabled = false;
            _window.TxbEmployeeKodPocztowy.IsEnabled = false;
            _window.TxbEmployeeLogin.IsEnabled = false;
            _window.TxbEmployeeMiejscowosc.IsEnabled = false;
            _window.TxbEmployeeNazwisko.IsEnabled = false;
            _window.TxbEmployeeWiek.IsEnabled = false;
            _window.CmbEmployeeAdmin.IsEnabled = false;
            _window.CmbEmployeeWojewodztwo.IsEnabled = false;
        }

        public void ShowModify()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Visible;
            _window.BtnEmployeeUsun.Visibility = Visibility.Hidden;

            _window.TxbEmployeeHaslo.IsEnabled = true;
            _window.TxbEmployeeImie.IsEnabled = true;
            _window.TxbEmployeeKodPocztowy.IsEnabled = true;
            _window.TxbEmployeeLogin.IsEnabled = true;
            _window.TxbEmployeeMiejscowosc.IsEnabled = true;
            _window.TxbEmployeeNazwisko.IsEnabled = true;
            _window.TxbEmployeeWiek.IsEnabled = true;
            _window.CmbEmployeeAdmin.IsEnabled = true;
            _window.CmbEmployeeWojewodztwo.IsEnabled = true;
        }
    }
}