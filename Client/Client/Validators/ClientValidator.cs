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
    class ClientValidator : IValidate
    {
        private static ClientValidator _instace;
        private Admin _window;
        protected ClientValidator() { }

        public static ClientValidator GetInstance(Admin window)
        {
            if (_instace == null)
            {
                _instace = new ClientValidator();
            }
            _instace._window = window;
            return _instace;
        }

        public bool Validate()
        {
            if (_window.TxbClientsImie.Text.Length < 5)
            {
                MessageBox.Show("Imię zbyt krótkie", "Bład", MessageBoxButton.OK);
                return false;
            }

            if (_window.TxbClientsNazwisko.Text.Length < 5)
            {
                MessageBox.Show("Nazwisko zbyt krótkie", "Bład", MessageBoxButton.OK);
                return false;
            }

            if (_window.TxbClientsFirma.Text.Length != 0 && _window.TxbClientsFirma.Text.Length < 5)
            {
                MessageBox.Show("Firma zbyt krótka nazwa", "Bład", MessageBoxButton.OK);
                return false;
            }

            if (_window.TxbClientsKodPocztowy.Text.Length != 6)
            {
                MessageBox.Show("Zły format kodu pocztowego", "Bład", MessageBoxButton.OK);
                return false;
            }

            if (_window.TxbClientsMiejscowosc.Text.Length < 5)
            {
                MessageBox.Show("Miejscowość zbyt krótka", "Bład", MessageBoxButton.OK);
                return false;
            }

            if (_window.CmbClientsWojewodztwo.SelectedIndex < 0)
            {
                MessageBox.Show("Bład wyboru Województwa", "Bład", MessageBoxButton.OK);
                return false;
            }
            return true;
        }
    }
}
