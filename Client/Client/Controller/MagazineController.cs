using Client.Communication;
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
        private ICommunication _comm;
        private List<Artykul> art;

        private MagazineController()
        {
            _comm = Communicator.GetInstance();
            _comm.SetUrlAddress("http://c414305-001-site1.btempurl.com");
            //_comm.SetUrlAddress("http://localhost:52992");
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
                    foreach (Artykul r in art)
                        _window.DgStateLista.Items.Add(r);
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Magazine Controller ShowData: {ex}");
            }
        }

        public void ShowSelectedData()
        {
            throw new NotImplementedException();
        }

        public void ChangeData()
        {
            throw new NotImplementedException();
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
                        foreach (Artykul a in list)
                            art.RemoveAll(ar => ar.idArtykulu == a.idArtykulu);
                        ShowData();
                    }));
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Magazine Controller SearchData: {ex}");
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
                            if (_window.RbStateSzukaj.IsChecked == false)
                                ShowData();
                        }));
                        ShowData();
                    });
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Magazine Controller GetData: {ex}");
            }
        }
    }
}