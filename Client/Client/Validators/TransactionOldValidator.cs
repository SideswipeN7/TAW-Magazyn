using Client.Interfaces;
using Client.Windows;
using System.Windows;

namespace Client.Validators
{
    internal class TransactionOldValidator : IValidate
    {
        private static TransactionOldValidator _instance;
        private Admin _window;

        protected TransactionOldValidator()
        {
        }

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
            if (_window.TxbDoGridTwoImie.Text.Length < 2)
            {
                MessageBox.Show("Zbyt krótkie imię", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.CmbDoGridTwoNazwisko.SelectedIndex < 0)
            {
                MessageBox.Show("Nie wybrano nazwiska", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.TxbDoGridTwoKodPocztowy.Text.Length != 6)
            {
                MessageBox.Show("Zły format kodu pocztowego", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.TxbDoGridTwoMiejscowosc.Text.Length < 2)
            {
                MessageBox.Show("Zbyt krótka nazwa miejscowości", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.CmbDoGridTwoWojewodztwo.SelectedIndex < 0)
            {
                MessageBox.Show("Nie wybrano województwa", "Błąd", MessageBoxButton.OK);
                return false;
            }
            if (_window.CmbDoGridTwoDostawca.SelectedIndex < 0)
            {
                MessageBox.Show("Nie wybrano dostwacy", "Błąd", MessageBoxButton.OK);
                return false;
            }
            return true;
        }
    }
}