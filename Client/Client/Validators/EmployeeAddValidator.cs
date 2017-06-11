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
    class EmployeeAddValidator : IValidate
    {
        private static EmployeeAddValidator _instace;
        private Admin _window;
        protected EmployeeAddValidator() { }

        public static EmployeeAddValidator GetInstance(Admin window)
        {
            if (_instace == null)
            {
                _instace = new EmployeeAddValidator();
            }
            _instace._window = window;
            return _instace;
        }

        public bool Validate()
        {

#pragma warning disable IDE0018 // Inline variable declaration
            int wiek;
#pragma warning restore IDE0018 // Inline variable declaration
            if (_window.TxbEmployeeHaslo.Text.Length < 5)
            {
                MessageBox.Show("Hasło zbyt krótkie - minimum 5 znaków", "Bład", MessageBoxButton.OK);
                return false;
            }


            if (_window.TxbEmployeeLogin.Text.Length < 5) { MessageBox.Show("Login zbyt krótki - minimum 5 znaków", "Bład", MessageBoxButton.OK); return false; }


            if (_window.TxbEmployeeImie.Text.Length < 4) { MessageBox.Show("imię zbyt krótkie", "Bład", MessageBoxButton.OK); return false; }


            if (_window.TxbEmployeeNazwisko.Text.Length < 5) { MessageBox.Show("Nazwisko zbyt krótkie", "Bład", MessageBoxButton.OK); return false; }


            if (_window.TxbEmployeeMiejscowosc.Text.Length < 5) { MessageBox.Show("Miejscowość zbyt krótkie", "Bład", MessageBoxButton.OK); return false; }


            if (_window.TxbEmployeeKodPocztowy.Text.Length != 6) { MessageBox.Show("Zły format kodu pocztowego", "Bład", MessageBoxButton.OK); return false; }


            if (!Int32.TryParse(_window.TxbEmployeeWiek.Text, out wiek)) { MessageBox.Show("Zły foramt wieku", "Bład", MessageBoxButton.OK); return false; }


            if (_window.CmbEmployeeAdmin.SelectedIndex < 0) { MessageBox.Show("Błąd wyboru statusu użytkownika", "Bład", MessageBoxButton.OK); return false; }


            if (_window.CmbEmployeeWojewodztwo.SelectedIndex < 0) { MessageBox.Show("Bład wyboru województwa", "Bład", MessageBoxButton.OK); return false; }
            return true;
        }
    }
}