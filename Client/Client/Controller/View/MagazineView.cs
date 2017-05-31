using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Controller.View
{
    class MagazineView:IViewController
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

            _window.LblStateCena.Visibility = Visibility.Hidden;
            _window.LblStateIlosc.Visibility = Visibility.Hidden;
            _window.LblStateKategoria.Visibility = Visibility.Hidden;
            _window.LblStateNazwa.Visibility = Visibility.Hidden;

            _window.TxbStateCenaMax.Visibility = Visibility.Hidden;
            _window.TxbStateCenaMin.Visibility = Visibility.Hidden;
            _window.TxbStateIlosc.Visibility = Visibility.Hidden;
            _window.TxbStateNazwa.Visibility = Visibility.Hidden;
            _window.CmbStateKategoria.Visibility = Visibility.Hidden;
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

