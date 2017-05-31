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
    internal class TransactionController : IWork
    {
        private InProgress inProg;
        private static TransactionController _instance;
        private Admin _window { get; set; }
        private ICommunication _comm;
        private List<Transakcja> transakcje;
        private int ID { get; set; }

        private TransactionController()
        {
            _comm = Communicator.GetInstance();
            _comm.SetUrlAddress("http://c414305-001-site1.btempurl.com");
            //_comm.SetUrlAddress("http://localhost:52992");
        }

        public static TransactionController GetInstance(int id, Admin window)
        {
            if (_instance == null)
            {
                _instance = new TransactionController();
            }
            _instance._window = window;
            _instance.ID = id;
            return _instance;
        }

        public void AddData()
        {
            try
            {
                if (_window.DgDoGridFive.Items.Count >= 0)
                {
                    inProg = InProgress.GetInstance();
                    inProg.Show();
                    List<Artykul_w_Koszyku> cart = new List<Artykul_w_Koszyku>();
                    foreach (Artykul_w_Koszyku r in _window.DgDoGridFive.Items)
                        cart.Add(r);
                    if (_window.RbDoGridOneNowyKlient.IsChecked == true)
                    {
                        TransactionNew(cart);
                    }
                    if (_window.RbDoGridOneStalyKlient.IsChecked == true)
                    {
                        TransactionOld(cart);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller AddData: {ex}");
            }
        }

        public void ChangeData()
        {
            throw new NotImplementedException();
        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        public void GetData()
        {
            try
            {
                Task<IEnumerable<Transakcja>>.Factory.StartNew(() =>
                {
                    return _comm.GetTransactions();
                }).ContinueWith(x =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        transakcje = x.Result.ToList();
                        _window.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (_window.RbOverviewGridOneSzukaj.IsChecked == false)
                                ShowData();
                        }));
                    });
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller GetData: {ex}");
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
                        List<Transakcja> list = new List<Transakcja>();
                        if (_window.ChbOverviewGridTwoData.IsChecked == true)
                        {
                            foreach (Transakcja t in transakcje)
                                if (t.Data != _window.DpOverviewGridTwoDataPicker.SelectedDate)
                                {
                                    list.Add(t);
                                }
                        }
                        //2
                        if (_window.ChbOverviewGridTwoFirma.IsChecked == true)
                        {
                            foreach (Transakcja t in transakcje)
                                if (t.Klienci.Nazwa_firmy.ToLower().Contains(_window.TxbOverviewGridTwoFirma.Text.ToLower()))
                                {
                                    list.Add(t);
                                }
                        }
                        //3
                        if (_window.ChbOverviewGridTwoNazwisko.IsChecked == true)
                        {
                            foreach (Transakcja t in transakcje)
                                if (t.Klienci.Nazwisko.ToLower().Contains(_window.TxbOverviewGridTwoNazwisko.Text.ToLower()))
                                {
                                    list.Add(t);
                                }
                        }

                        foreach (Transakcja a in list)
                            transakcje.RemoveAll(ar => ar.idTransakcji == a.idTransakcji);
                        ShowData();
                    }));
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller SearchData: {ex}");
            }
        }

        public void ShowData()
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (Transakcja r in transakcje)
                        _window.DgOverviewGridOne.Items.Add(r);
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller ShowData: {ex}");
            }
        }

        public void ShowSelectedData()
        {
            try
            {
                if (_window.DgOverviewGridOne.SelectedIndex >= 0)
                {
                    Transakcja t = (Transakcja)_window.DgOverviewGridOne.SelectedItem;
                    _window.LblOverviewGridTwoNazwisko.Content = $"Nazwisko: {t.Klienci.Nazwisko}";
                    _window.LblOverviewGridTwoNazwaFirmy.Content = $"Nazwa Firmy: {t.Klienci.Nazwa_firmy}";
                    _window.LblOverviewGridTwoDostawca.Content = $"Dostawca: {t.Dostawcy.Nazwa}";
                    _window.LblOverviewGridTwoData.Content = $"Data: {t.Data}";
                    _window.LblOverviewGridTwoMiejscowosc.Content = $"Miejscowość: {t.Klienci.Ksiazka_adresow.Miejscowosc}";
                    _window.LblOverviewGridTwoKodPocztowy.Content = $"Kod Pocztowy: {t.Klienci.Ksiazka_adresow.Kod_pocztowy}";
                    _window.LblOverviewGridTwoWojewodztwo.Content = $"Województwo: {t.Klienci.Ksiazka_adresow.Wojewodztwo}";

                    int quantity = t.Artykuly_w_transakcji.Count;
                    decimal cost = 0m;
                    foreach (Artykul_w_transakcji r in t.Artykuly_w_transakcji)
                    {
                        cost += r.Cena;

                        if (_window.DgOverviewGridThree.Items.Count == 0)
                        {
                            _window.DgOverviewGridThree.Items.Add(r.Artykuly);
                        }

                        if (_window.DgOverviewGridThree.Items.Count > 0)
                        {
                            foreach (Artykul a in _window.DgOverviewGridThree.Items)
                            {
                                if (!r.Artykuly.Equals(a))
                                {
                                    _window.DgOverviewGridThree.Items.Add(r.Artykuly);
                                }
                            }
                        }
                    }
                    _window.LblOverviewGridTwoCena.Content = $"Cena: {cost:F2} zł";
                    _window.LblOverviewGridTwoIloscProduktow.Content = $"Ilość Produktów: {quantity}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller ShowSelectedData: {ex}");
            }
        }

        private void TransactionNew(List<Artykul_w_Koszyku> cart)
        {
            try
            {
                if (_window.TxbDoGridOneImie.Text.Length > 2 &&
                     _window.TxbDoGridOneNazwisko.Text.Length > 2 &&
                     (_window.TxbDoGridOneFirma.Text.Length > 5 || _window.TxbDoGridOneFirma.Text.Length == 0) &&
                     _window.TxbDoGridOneKodPocztowy.Text.Length == 6 &&
                     _window.TxbDoGridOneMiejscowosc.Text.Length > 2 &&
                     _window.CmbDoGridOneWojewodztwo.SelectedIndex >= 0 &&
                     _window.CmbDoGridOneDostawca.SelectedIndex >= 0)
                {
                    Adres a = new Adres()
                    {
                        Kod_pocztowy = _window.TxbDoGridOneKodPocztowy.Text,
                        Miejscowosc = _window.TxbDoGridOneMiejscowosc.Text,
                        Wojewodztwo = _window.CmbDoGridOneWojewodztwo.SelectedItem.ToString()
                    };
                    Klient k = new Klient()
                    {
                        Imie = _window.TxbDoGridOneImie.Text,
                        Nazwisko = _window.TxbDoGridOneNazwisko.Text
                    };
                    Task<int>.Factory.StartNew(() =>
                    {
                        return _comm.RegisterClient(new KlientAdress() { Klient = k, Adres = a });
                    }).ContinueWith(x =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            k.idKlienta = x.Result;
                            Transakcja t = null;
                            _window.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                t = new Transakcja()
                                {
                                    idKlienta = k.idKlienta,
                                    idDostawcy = ((Dostawca)((ComboBoxItem)_window.CmbDoGridOneDostawca.SelectedItem).Tag).idDostawcy,
                                    idPracownika = ID,
                                    Data = DateTime.Now
                                };
                                foreach (Artykul_w_Koszyku r in _window.DgDoGridFive.Items)
                                {
                                    for (int i = 0; i < r.Ilosc; i++)
                                        t.Artykuly_w_transakcji.Add(new Artykul_w_transakcji() { Artykuly = r.Artykul, Cena = r.Cena, idArtykulu = r.Artykul.idKategorii });
                                }
                            }));
                            TransactionRegister(t);
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller TransactionNew: {ex}");
            }
        }

        private void TransactionRegister(Transakcja t)
        {
            try
            {
                Task<int>.Factory.StartNew(() =>
                {
                    return _comm.RegisterTransaction(t);
                }).ContinueWith(x =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        inProg.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            inProg.Hide();
                        }));
                        _window.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            GetData();
                            MessageBox.Show("Wykonano pomyślnie!", "Transakcja", MessageBoxButton.OK);
                        }));
                        return 0;
                    });
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller TransactionRegister: {ex}");
            }
        }

        private void TransactionOld(List<Artykul_w_Koszyku> cart)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}_window.TxbDoGridTwoImie.Text.Length= { _window.TxbDoGridTwoImie.Text.Length }{ Environment.NewLine}_window.CmbDoGridTwoNazwisko.SelectedIndex= {_window.CmbDoGridTwoNazwisko.SelectedIndex}{Environment.NewLine}_window.CmbDoGridTwoFirma.SelectedIndex= {_window.CmbDoGridTwoFirma.SelectedIndex}{Environment.NewLine}_window.TxbDoGridTwoKodPocztowy.Text.Length= {_window.TxbDoGridTwoKodPocztowy.Text.Length}{Environment.NewLine}_window.TxbDoGridTwoMiejscowosc.Text.Length= {_window.TxbDoGridTwoMiejscowosc.Text.Length}{Environment.NewLine}_window.CmbDoGridTwoWojewodztwo.SelectedIndex= {_window.CmbDoGridTwoWojewodztwo.SelectedIndex}{Environment.NewLine}_window.CmbDoGridTwoDostawca.SelectedIndex= {_window.CmbDoGridTwoDostawca.SelectedIndex}{Environment.NewLine}");
                if (_window.TxbDoGridTwoImie.Text.Length > 2 &&
                     _window.CmbDoGridTwoNazwisko.SelectedIndex >= 0 &&
                     _window.CmbDoGridTwoFirma.SelectedIndex >= 0 &&
                     _window.TxbDoGridTwoKodPocztowy.Text.Length == 6 &&
                     _window.TxbDoGridTwoMiejscowosc.Text.Length > 2 &&
                     _window.CmbDoGridTwoWojewodztwo.SelectedIndex >= 0 &&
                     _window.CmbDoGridTwoDostawca.SelectedIndex >= 0)
                {
                    Adres a = new Adres()
                    {
                        Kod_pocztowy = _window.TxbDoGridTwoKodPocztowy.Text,
                        Miejscowosc = _window.TxbDoGridTwoMiejscowosc.Text,
                        Wojewodztwo = _window.CmbDoGridTwoWojewodztwo.SelectedItem.ToString()
                    };
                    Klient k = ((Klient)((ComboBoxItem)_window.CmbDoGridTwoNazwisko.SelectedItem).Tag);
                    Task<int>.Factory.StartNew(() =>
                    {
                        return _comm.RegisterClient(new KlientAdress() { Klient = k, Adres = a });
                    }).ContinueWith(x =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            k.idKlienta = x.Result;
                            Transakcja t = new Transakcja();
                            try
                            {
                                t.idKlienta = k.idKlienta;
                                _window.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    Dostawca d = (Dostawca)(((ComboBoxItem)_window.CmbDoGridTwoDostawca.SelectedItem).Tag);
                                    t.idDostawcy = d.idDostawcy;
                                }));

                                t.idPracownika = ID;
                                t.Data = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine} ERRRROR {ex}");
                            }
                            _window.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                foreach (Artykul_w_Koszyku r in _window.DgDoGridFive.Items)
                                {
                                    for (int i = 0; i < r.Ilosc; i++)
                                        t.Artykuly_w_transakcji.Add(new Artykul_w_transakcji() { Artykuly = r.Artykul, Cena = r.Cena, idArtykulu = r.Artykul.idKategorii });
                                }
                            }));
                            TransactionRegister(t);
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller TransactionOld: {ex}");
            }
        }
    }
}