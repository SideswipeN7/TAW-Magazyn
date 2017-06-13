using Client.Windows;
using System.Windows;

namespace Client.Controller.View
{
    internal class CategoryView : IViewController
    {
        private Admin _window { get; set; }
        private static CategoryView _instance;

        protected CategoryView()
        {
        }

        public static CategoryView GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new CategoryView();
            }
            _instance._window = window;
            return _instance;
        }

        public void ShowAdd()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Hidden;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Hidden;

            _window.BtnCategoryDodaj.Visibility = Visibility.Visible;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;

            _window.LblCategoryId.IsEnabled = true;
            _window.LblCategoryNazwa.IsEnabled = true;

            _window.TxbCategoryNazwa.IsEnabled = true;

            _window.CmbCategoryId.IsEnabled = true;
        }

        public void ShowAll()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.IsEnabled = false;
            _window.LblCategoryNazwa.IsEnabled = false;

            _window.TxbCategoryNazwa.IsEnabled = false;

            _window.CmbCategoryId.IsEnabled = false;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        public void ShowDelete()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Visible;
            _window.LblCategoryId.IsEnabled = true;
            _window.LblCategoryNazwa.IsEnabled = true;

            _window.TxbCategoryNazwa.IsEnabled = false;

            _window.CmbCategoryId.IsEnabled = false;
        }

        public void ShowModify()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Visible;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
            _window.LblCategoryId.IsEnabled = true;
            _window.LblCategoryNazwa.IsEnabled = true;

            _window.TxbCategoryNazwa.IsEnabled = true;

            _window.CmbCategoryId.IsEnabled = true;
        }

        public void ShowSearch()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Visible;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Visible;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
            _window.LblCategoryId.IsEnabled = true;
            _window.LblCategoryNazwa.IsEnabled = true;

            _window.TxbCategoryNazwa.IsEnabled = true;

            _window.CmbCategoryId.IsEnabled = true;
        }
    }
}