using Client.Windows;
using System;
using System.Windows;

namespace Client.Controller.View
{
    internal class TransactionView : IViewController
    {
        private Admin _window { get; set; }
        private static TransactionView _instance;

        private TransactionView()
        {
        }

        public static TransactionView GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new TransactionView();
            }
            _instance._window = window;
            return _instance;
        }

        public void ShowAll()
        {
            _window.GridOverwviewSearch.Visibility = Visibility.Hidden;
        }

        public void ShowSearch()
        {
            _window.GridOverwviewSearch.Visibility = Visibility.Visible;
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