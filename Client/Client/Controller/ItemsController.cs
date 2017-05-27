using Client.Communication;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    class ItemsController : IWork
    {
        private static ItemsController _instance;
        Admin _window { get; set; }
        private ICommunication _comm;

        private ItemsController()
        {
            _comm = Communicator.GetInstance();
             _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
            //_comm.SetUrlAddress("http://localhost:52992");
        }
        public static ItemsController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new ItemsController();
            }
            _instance._window = window;            
            return _instance;
        }

        public void AddData()
        {
            //int quantity;
            //decimal price;
            //if (_window.TxbItemINazwa.Text.Length > 5 &&
            //     Int32.TryParse(_window.TxbItemIlosc.Text, out quantity) &&
            //     Decimal.TryParse(_window.TxbItemCenaMin.Text, out price) &&
            //     _window.CmbItemKategoria.SelectedIndex >= 0)
            //{
            //    if (_comm.RegisterItem(new Artykul() { Cena = price, Ilosc = quantity, Nazwa = _window.TxbItemINazwa.Text, idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Kategorie = new Kategoria() { idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content } }))
            //    {
            //        GetData();
            //    }
            //}
        }

        public void ChangeDate()
        {
            throw new NotImplementedException();
        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        public void ShowData()
        {
            throw new NotImplementedException();
        }

        public void ShowSelectedData()
        {
            throw new NotImplementedException();
        }

        public void GetData()
        {
            throw new NotImplementedException();
        }
    }
}
