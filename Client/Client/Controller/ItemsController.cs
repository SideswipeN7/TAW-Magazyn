using Client.Communication;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Client.Controller
{
    internal class ItemsController : IWork
    {
        private static ItemsController _instance;
        private Admin _window { get; set; }
        private ICommunication _comm;
        private List<Artykul> art;


        private ItemsController()
        {
            _comm = Communicator.GetInstance();
            //_comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
            _comm.SetUrlAddress("http://localhost:52992");
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

            try
            {

                int quantity;
                decimal price;
                if (_window.TxbItemINazwa.Text.Length < 5)
                {
                    MessageBox.Show("Nazwa zbyt krótka zbyt krótkie", "Bład", MessageBoxButton.OK);
                }
                else
                if (!Int32.TryParse(_window.TxbItemIlosc.Text, out quantity))
                {
                    MessageBox.Show("Zły format ilości", "Bład", MessageBoxButton.OK);
                }
                else
                if (!Decimal.TryParse(_window.TxbItemCenaMin.Text, out price))
                {
                    MessageBox.Show("Zły format Ceny", "Bład", MessageBoxButton.OK);
                }
                else
                if (_window.CmbItemKategoria.SelectedIndex < 0)
                {
                    MessageBox.Show("Bład wyboru kategori", "Bład", MessageBoxButton.OK);
                }
                else
                {
                    Artykul artykul = new Artykul()
                    {
                        Cena = price,
                        Ilosc = quantity,
                        Nazwa = _window.TxbItemINazwa.Text,
                        idKategorii = ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii,
                        Kategorie = new Kategoria()
                        {
                            idKategorii = ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii,
                            Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content
                        }
                    };
                    Task.Factory.StartNew(() =>
                    {
                        _comm.RegisterItem(artykul);
                    }).ContinueWith(x =>
                    Task.Factory.StartNew(() =>
                    {
                        GetData();
                    }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Item Controller AddData: {ex}");
            }

        }

        public void ChangeData()
        {
            try
            {
                int quantity;
                decimal price;
                if (_window.TxbItemINazwa.Text.Length < 5)
                {
                    MessageBox.Show("Nazwa zbyt krótka zbyt krótkie", "Bład", MessageBoxButton.OK);
                }
                else
                if (!Int32.TryParse(_window.TxbItemIlosc.Text, out quantity))
                {
                    MessageBox.Show("Zły format ilości", "Bład", MessageBoxButton.OK);
                }
                else
                if (!Decimal.TryParse(_window.TxbItemCenaMin.Text, out price))
                {
                    MessageBox.Show("Zły format Ceny", "Bład", MessageBoxButton.OK);
                }
                else
                if (_window.CmbItemKategoria.SelectedIndex < 0)
                {
                    MessageBox.Show("Bład wyboru kategori", "Bład", MessageBoxButton.OK);
                }
                else
                {
                    Artykul artykul = new Artykul()
                    {
                        idArtykulu = ((Artykul)_window.DgItemLista.SelectedItem).idArtykulu,
                        Cena = price,
                        Ilosc = quantity,
                        Nazwa = _window.TxbItemINazwa.Text,
                        idKategorii = ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii,
                        Kategorie = new Kategoria()
                        {
                            idKategorii = ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii,
                            Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content
                        }
                    };
                    Task.Factory.StartNew(() =>
                    {
                        _comm.ChangeItem(artykul);
                    }).ContinueWith(x =>
                    Task.Factory.StartNew(() =>
                    {
                        GetData();
                    }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Item Controller ChangeData: {ex}");
            }

        }

        public void DeleteData()
        {
            try
            {
                if (_window.DgItemLista.SelectedIndex >= 0)
                {
                    _comm.DeleteItem(((Artykul)_window.DgItemLista.SelectedItem).idArtykulu);
                    GetData();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Item Controller DeleteData: {ex}");
            }
        }

        public void ShowData()
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _window.DgItemLista.Items.Clear();

                    foreach (Artykul r in art)
                    {
                        _window.DgItemLista.Items.Add(r);
                    }


                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Item Controller ShowData: {ex}");
            }
        }

        public void ShowSelectedData()
        {
            try
            {
                if (_window.DgItemLista.SelectedIndex >= 0)
                {
                    for (int i = 0; i < _window.CmbItemKategoria.Items.Count; i++)
                    {
                        if (((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.Items.GetItemAt(i)).Tag).idKategorii == ((Artykul)_window.DgItemLista.SelectedItem).Kategorie.idKategorii)
                        {
                            _window.CmbItemKategoria.SelectedIndex = i;
                        }
                    }
                    _window.TxbItemCenaMin.Text = ((Artykul)_window.DgItemLista.SelectedItem).Cena + "";
                    _window.TxbItemIlosc.Text = ((Artykul)_window.DgItemLista.SelectedItem).Ilosc + "";
                    _window.TxbItemINazwa.Text = ((Artykul)_window.DgItemLista.SelectedItem).Nazwa;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Item Controller ShowSelectedData: {ex}");
            }
        }

        public void GetData()
        {
            try
            {
                Task<IEnumerable<Artykul>>.Factory.StartNew(() =>
                {
                    return _comm.GetItems();
                }).ContinueWith(x =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        art = x.Result.ToList();
                        _window.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (_window.RbItemSzukaj.IsChecked == false)
                                ShowData();
                        }));
                    });
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Item Controller GetData: {ex}");
            }
        }

        public void SearchData()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    GetData();
                }).ContinueWith(x =>
                Task.Factory.StartNew(() =>
                {
                    _window.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        List<Artykul> list = new List<Artykul>();
                        //1
                        if (_window.ChbItemCena.IsChecked == true)
                        {
                            decimal max, min;
                            if (_window.TxbItemCenaMin.Text.Length == 0)
                                _window.TxbItemCenaMin.Text = "0";
                                if (_window.TxbItemCenaMax.Text.Length==0)
                                _window.TxbItemCenaMax.Text = "9999";
                            if (!Decimal.TryParse(_window.TxbItemCenaMin.Text, out min))
                            {
                                MessageBox.Show("Zły format Ceny Minimalnej", "Bład", MessageBoxButton.OK);
                            }
                            else
                            if (!Decimal.TryParse(_window.TxbItemCenaMax.Text, out max))
                            { MessageBox.Show("Zły format Ceny Maksymalnej", "Bład", MessageBoxButton.OK); }
                            else
                            {
                                foreach (Artykul a in art)
                                {
                                    if (a.Cena < min || a.Cena > max)
                                    {
                                        list.Add(a);
                                    }
                                }
                            }

                        }
                        //2
                        if (_window.ChbItemIlosc.IsChecked == true)
                        {
                            decimal ilosc;
                            if (Decimal.TryParse(_window.TxbItemCenaMin.Text, out ilosc))
                            {
                                foreach (Artykul a in art)
                                {
                                    if (a.Ilosc > ilosc)
                                    {
                                        list.Add(a);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Zły format Ceny", "Bład", MessageBoxButton.OK);
                            }
                        }
                        //3
                        if (_window.ChbItemKategoria.IsChecked == true)
                        {
                            foreach (Artykul a in art)
                            {
                                if (a.idKategorii != ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii)
                                {
                                    list.Add(a);
                                }
                            }
                        }
                        //4
                        if (_window.ChbItemNazwa.IsChecked == true)
                        {
                            foreach (Artykul a in art)
                            {
                                if (!a.Nazwa.ToLower().Contains(_window.TxbItemINazwa.Text.ToLower()))
                                {
                                    list.Add(a);
                                }
                            }
                        }
                        foreach (Artykul a in list)
                            art.RemoveAll(ar => ar.idArtykulu == a.idArtykulu);
                        ShowData();

                    }));

                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Item Controller SearchData: {ex}");
            }
        }

    }
}