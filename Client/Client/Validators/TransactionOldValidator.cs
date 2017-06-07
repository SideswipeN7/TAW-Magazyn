using Client.Interfaces;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Validators
{
    class TransactionOldValidator : IValidate
    {
        private static TransactionOldValidator _instance;
        private Admin _window;
        protected TransactionOldValidator() { }

        public static TransactionOldValidator GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new TransactionOldValidator();
            }
            _instance._window = window;
            return _instance;
        }
        public bool Validate()
        {
            if (_window.TxbDoGridOneImie.Text.Length < 2)
            {
                MessageBox.Show("Zbyt krótkie imię", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.TxbDoGridOneNazwisko.Text.Length < 2)
            {
                MessageBox.Show("Zbyt krótkie nazwisko", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if ((_window.TxbDoGridOneFirma.Text.Length < 5 && _window.TxbDoGridOneFirma.Text.Length > 0))
            {
                MessageBox.Show("Zbyt krótkie nazwa firmy", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.TxbDoGridOneKodPocztowy.Text.Length != 6)
            {
                MessageBox.Show("Zły format kodu pocztowego", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.TxbDoGridOneMiejscowosc.Text.Length < 2)
            {
                MessageBox.Show("Zbyt krótka nazwa miejscowości", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.CmbDoGridOneWojewodztwo.SelectedIndex < 0)
            {
                MessageBox.Show("Nie wybrano województwa", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.CmbDoGridOneDostawca.SelectedIndex < 0)
            {
                MessageBox.Show("Nie wybrano dostwacy", "Błąd", MessageBoxButton.OK);
                return false;
            }
            return true;
        }
    }
}
