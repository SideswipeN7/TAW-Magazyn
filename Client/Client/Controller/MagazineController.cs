using Client.Communication;
using Client.Interfaces;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Controller
{
    internal class MagazineController : IWork
    {
        private Admin _window { get; set; }
        private static MagazineController _instance;
        private ICommItems _comm;
        private List<Artykul> art;
        private List<Artykul> artSearched;

        protected MagazineController()
        {
            _comm = CommItems.GetInstance();
            artSearched = new List<Artykul>();
        }

        public static MagazineController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new MagazineController();
            }
            _instance._window = window;
            return _instance;
        }

        public void AddData()
        {
            throw new NotImplementedException();
        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        public void ShowData()
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _window.DgStateLista.Items.Clear();
                    if (_window.RbStateSzukaj.IsChecked == false)
                    {
                        foreach (Artykul r in art)
                            _window.DgStateLista.Items.Add(r);
                    }
                    if (_window.RbStateSzukaj.IsChecked == true)
                    {
                        foreach (Artykul r in artSearched)
                            _window.DgStateLista.Items.Add(r);
                    }
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Magazine Controller ShowData: {ex} " + nameof(ShowData));
            }
        }

        public void ShowSelectedData()
        {
            try
            {
                Artykul a = (Artykul)_window.DgStateLista.SelectedItem;
                _window.TxbStateCenaMin.Text = a.Cena + "";
                _window.TxbStateIlosc.Text = a.Ilosc + "";
                _window.TxbStateNazwa.Text = a.Nazwa;
                for (int i = 0; i < _window.CmbStateKategoria.Items.Count; i++)
                {
                    if (((Kategoria)((ComboBoxItem)_window.CmbStateKategoria.Items.GetItemAt(i)).Tag).idKategorii == a.idKategorii)
                    {
                        _window.CmbStateKategoria.SelectedIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Magazine Controller ShowSelectedData: {ex} " + nameof(ShowSelectedData));
            }
        }

        public void ChangeData()
        {
            throw new NotImplementedException();
        }

        public void SearchData()
        {
            try
            {
                artSearched.Clear();
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
                        if (_window.ChbStateCena.IsChecked == true)
                        {
                            decimal max, min;
                            if (_window.TxbStateCenaMin.Text.Length == 0)
                                _window.TxbStateCenaMin.Text = "0";
                            if (_window.TxbStateCenaMax.Text.Length == 0)
                                _window.TxbStateCenaMax.Text = "9999";
                            if (!Decimal.TryParse(_window.TxbStateCenaMin.Text, out min))
                            {
                                MessageBox.Show("Zły format Ceny Minimalnej", "Bład", MessageBoxButton.OK);
                            }
                            else
                            if (!Decimal.TryParse(_window.TxbStateCenaMax.Text, out max))
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
                        if (_window.ChbStateIlosc.IsChecked == true)
                        {
                            decimal ilosc;
                            if (Decimal.TryParse(_window.TxbStateIlosc.Text, out ilosc))
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
                        if (_window.ChbStateKategoria.IsChecked == true)
                        {
                            foreach (Artykul a in art)
                            {
                                if (a.idKategorii != ((Kategoria)((ComboBoxItem)_window.CmbStateKategoria.SelectedItem).Tag).idKategorii)
                                {
                                    list.Add(a);
                                }
                            }
                        }
                        //4
                        if (_window.ChbStateNazwa.IsChecked == true)
                        {
                            foreach (Artykul a in art)
                            {
                                if (!a.Nazwa.ToLower().Contains(_window.TxbStateNazwa.Text.ToLower()))
                                {
                                    list.Add(a);
                                }
                            }
                        }
                        //foreach (Artykul a in list)
                        //    art.RemoveAll(ar => ar.idArtykulu == a.idArtykulu);
                        foreach (Artykul a in art)
                        {
                            if (!list.Contains(a))
                                artSearched.Add(a);
                        }
                        ShowData();
                    }));
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Magazine Controller SearchData: {ex} " + nameof(SearchData));
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
                        ShowData();
                    });
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Magazine Controller GetData: {ex} " + nameof(GetData));
            }
        }
    }
}