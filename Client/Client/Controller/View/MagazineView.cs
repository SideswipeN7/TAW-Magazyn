using Client.Windows;
using System;
using System.Windows;

namespace Client.Controller.View
{
    internal class MagazineView : IViewController
    {
        private Admin _window { get; set; }
        private static MagazineView _instance;

        private MagazineView()
        {
        }

        public static MagazineView GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new MagazineView();
            }
            _instance._window = window;
            return _instance;
        }

        public void ShowAll()
        {
            _window.ChbStateCena.Visibility = Visibility.Hidden;
            _window.ChbStateIlosc.Visibility = Visibility.Hidden;
            _window.ChbStateKategoria.Visibility = Visibility.Hidden;
            _window.ChbStateNazwa.Visibility = Visibility.Hidden;
            _window.LblState_.Visibility = Visibility.Hidden;
            _window.BtnSateSzukaj.Visibility = Visibility.Hidden;

           

            _window.TxbStateCenaMax.Visibility = Visibility.Hidden;
            _window.LblStateCena.IsEnabled = false;
            _window.LblStateIlosc.IsEnabled = false;
            _window.LblStateKategoria.IsEnabled = false;
            _window.LblStateNazwa.IsEnabled = false;
            _window.TxbStateCenaMin.IsEnabled = false;
            _window.TxbStateIlosc.IsEnabled = false;
            _window.TxbStateNazwa.IsEnabled = false;
            _window.CmbStateKategoria.IsEnabled = false;
        }

        public void ShowSearch()
        {
            _window.ChbStateCena.Visibility = Visibility.Visible;
            _window.ChbStateIlosc.Visibility = Visibility.Visible;
            _window.ChbStateKategoria.Visibility = Visibility.Visible;
            _window.ChbStateNazwa.Visibility = Visibility.Visible;
            _window.LblState_.Visibility = Visibility.Visible;
            _window.BtnSateSzukaj.Visibility = Visibility.Visible;

            _window.LblStateCena.Visibility = Visibility.Visible;
            _window.LblStateIlosc.Visibility = Visibility.Visible;
            _window.LblStateKategoria.Visibility = Visibility.Visible;
            _window.LblStateNazwa.Visibility = Visibility.Visible;

            _window.TxbStateCenaMax.Visibility = Visibility.Visible;
            _window.TxbStateCenaMin.Visibility = Visibility.Visible;
            _window.TxbStateIlosc.Visibility = Visibility.Visible;
            _window.TxbStateNazwa.Visibility = Visibility.Visible;
            _window.CmbStateKategoria.Visibility = Visibility.Visible;

            _window.LblStateCena.IsEnabled = true;
            _window.LblStateIlosc.IsEnabled = true;
            _window.LblStateKategoria.IsEnabled = true;
            _window.LblStateNazwa.IsEnabled = true;
            _window.TxbStateCenaMin.IsEnabled = true;
            _window.TxbStateIlosc.IsEnabled = true;
            _window.TxbStateNazwa.IsEnabled = true;
            _window.CmbStateKategoria.IsEnabled = true;
        }

        public void ShowAdd()
        {
            throw new NotImplementedException();
        }

        public void ShowDelete()
        {
            throw new NotImplementedException();
        }

        public void ShowModify()
        {
            throw new NotImplementedException();
        }
    }
}