using Client.Windows;
using System.Windows;

namespace Client.Controller.View
{
    internal class ClientsView : IViewController
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

            _window.TxbClientsFirma.IsEnabled = false;
            _window.TxbClientsImie.IsEnabled = false;
            _window.TxbClientsKodPocztowy.IsEnabled = false;
            _window.TxbClientsMiejscowosc.IsEnabled = false;
            _window.TxbClientsNazwisko.IsEnabled = false;
            _window.CmbClientsWojewodztwo.IsEnabled = false;

        }

        public void ShowSearch()
        {
            _window.GridClientsSearch.Visibility = Visibility.Visible;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Visible;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;

            _window.TxbClientsFirma.IsEnabled = false;
            _window.TxbClientsImie.IsEnabled = false;
            _window.TxbClientsKodPocztowy.IsEnabled = false;
            _window.TxbClientsMiejscowosc.IsEnabled = false;
            _window.TxbClientsNazwisko.IsEnabled = false;
            _window.CmbClientsWojewodztwo.IsEnabled = false;
        }

        public void ShowAdd()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Visible;
            _window.TxbClientsFirma.IsEnabled = true;
            _window.TxbClientsImie.IsEnabled = true;
            _window.TxbClientsKodPocztowy.IsEnabled = true;
            _window.TxbClientsMiejscowosc.IsEnabled = true;
            _window.TxbClientsNazwisko.IsEnabled = true;
            _window.CmbClientsWojewodztwo.IsEnabled = true;
        }

        public void ShowDelete()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Visible;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
            _window.TxbClientsFirma.IsEnabled = true;
            _window.TxbClientsImie.IsEnabled = true;
            _window.TxbClientsKodPocztowy.IsEnabled = true;
            _window.TxbClientsMiejscowosc.IsEnabled = true;
            _window.TxbClientsNazwisko.IsEnabled = true;
            _window.CmbClientsWojewodztwo.IsEnabled = true;
        }

        public void ShowModify()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Visible;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
            _window.TxbClientsFirma.IsEnabled = false;
            _window.TxbClientsImie.IsEnabled = false;
            _window.TxbClientsKodPocztowy.IsEnabled = false;
            _window.TxbClientsMiejscowosc.IsEnabled = false;
            _window.TxbClientsNazwisko.IsEnabled = false;
            _window.CmbClientsWojewodztwo.IsEnabled = false;
        }
    }
}