using Client.Communication;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.Controller
{
    internal class ItemsController : IWork
    {
        private static ItemsController _instance;
        private Admin _window { get; set; }
        private ICommunication _comm;
        private IEnumerable<Artykul> art;


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
            int quantity;
            decimal price;
            if (_window.TxbItemINazwa.Text.Length > 5 &&
                 Int32.TryParse(_window.TxbItemIlosc.Text, out quantity) &&
                 Decimal.TryParse(_window.TxbItemCenaMin.Text, out price) &&
                 _window.CmbItemKategoria.SelectedIndex >= 0)
            {
                if (_comm.RegisterItem(new Artykul() { Cena = price, Ilosc = quantity, Nazwa = _window.TxbItemINazwa.Text, idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Kategorie = new Kategoria() { idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content } }))
                {
                    GetData();
                }
            }
        }

        public void ChangeData()
        {
            int quantity;
            decimal price;
            int id = ((Artykul)_window.DgItemLista.SelectedItem).idArtykulu;
            if (_window.TxbItemINazwa.Text.Length > 5 &&
                Int32.TryParse(_window.TxbItemIlosc.Text, out quantity) &&
                Decimal.TryParse(_window.TxbItemCenaMin.Text, out price) &&
                _window.CmbItemKategoria.SelectedIndex >= 0)
            {
                if (_comm.ChangeItem(new Artykul() { idArtykulu = id, Cena = price, Ilosc = quantity, Nazwa = _window.TxbItemINazwa.Text, idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Kategorie = new Kategoria() { idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content } }))
                {
                    GetData();
                }
            }
        }

        public void DeleteData()
        {
            if (_window.DgItemLista.SelectedIndex >= 0)
            {
                _comm.DeleteItem(((Artykul)_window.DgItemLista.SelectedItem).idArtykulu);
                GetData();
            }
        }

        public void ShowData()
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                // _window.CmbCategoryId.Items.Clear();
                foreach (Artykul r in art)
                {
                    _window.DgItemLista.Items.Add(r);
                }
            }));
        }

        public void ShowSelectedData()
        {
            if (_window.DgItemLista.SelectedIndex >= 0)
            {
                try
                {
                    for (int i = 0; i < _window.CmbItemKategoria.Items.Count; i++)
                    {
                        if (((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.Items.GetItemAt(i)).Tag).idKategorii == ((Artykul)_window.DgItemLista.SelectedItem).Kategorie.idKategorii)
                        {
                            _window.CmbItemKategoria.SelectedIndex = i;
                        }
                    }
                    //_window.CmbItemKategoria.SelectedItem = new ComboBoxItem() { Content = ((Artykul)_window.DgItemLista.SelectedItem).Kategorie.Nazwa, Tag = ((Artykul)_window.DgItemLista.SelectedItem).Kategorie };
                    _window.TxbItemCenaMin.Text = ((Artykul)_window.DgItemLista.SelectedItem).Cena + "";
                    _window.TxbItemIlosc.Text = ((Artykul)_window.DgItemLista.SelectedItem).Ilosc + "";
                    _window.TxbItemINazwa.Text = ((Artykul)_window.DgItemLista.SelectedItem).Nazwa;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}ERROR: {ex}");
                }
            }
        }

        public void GetData()
        {
            Task<IEnumerable<Artykul>>.Factory.StartNew(() =>
            {
                return _comm.GetItems();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    art = x.Result;
                    ShowData();
                });
            });
        }
    }
}