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
    class ItemAddValidator : IValidate
    {
        private static ItemAddValidator _instace;
        private Admin _window;
        protected ItemAddValidator() { }

        public static ItemAddValidator GetInstance(Admin window)
        {
            if (_instace == null)
            {
                _instace = new ItemAddValidator();
            }
            _instace._window = window;
            return _instace;
        }

        public bool Validate()
        {
            int quantity;
            decimal price;
            if (_window.TxbItemINazwa.Text.Length < 5)
            {
                MessageBox.Show("Nazwa zbyt krótka zbyt krótkie", "Bład", MessageBoxButton.OK);
                return false;
            }
            
                 if (!Int32.TryParse(_window.TxbItemIlosc.Text, out quantity))
            {
                MessageBox.Show("Zły format ilości", "Bład", MessageBoxButton.OK);
                return false;
            }
            
                 if (!Decimal.TryParse(_window.TxbItemCenaMin.Text, out price))
            {
                MessageBox.Show("Zły format Ceny", "Bład", MessageBoxButton.OK);
                return false;
            }
          
                 if (_window.CmbItemKategoria.SelectedIndex < 0)
            {
                MessageBox.Show("Bład wyboru kategori", "Bład", MessageBoxButton.OK);
                return false;
            }
            return true;
        }
    }
}
