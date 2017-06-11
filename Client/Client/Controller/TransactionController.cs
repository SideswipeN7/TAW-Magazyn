using Client.Communication;
using Client.Interfaces;
using Client.Model;
using Client.Validators;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Controller
{
    public class TransactionController : IWork
    {
        private InProgress inProg;
        private static TransactionController _instance;
        private Admin _window { get; set; }
        private ICommTransaction _comm { get; set; } = CommTransaction.GetInstance();
        private List<Transakcja> transakcje;
        private List<Transakcja> transakcjeSearched;
        private int ID { get; set; }

        protected TransactionController()
        {
            transakcjeSearched = new List<Transakcja>();
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

        public static TransactionController GetInstance(ICommTransaction _comm)
        {
            if (_instance == null)
            {
                _instance = new TransactionController();
            }
            _instance._comm = _comm;
            return _instance;
        }

        public void AddData()
        {
            try
            {
                if (_window.DgDoGridFive.Items.Count >= 0)
                {
                    inProg = InProgress.GetInstance();
                    ///inProg.Show();
                    List<Artykul_w_Koszyku> cart = new List<Artykul_w_Koszyku>();
                    foreach (Artykul_w_Koszyku r in _window.DgDoGridFive.Items)
                        cart.Add(r);
                    if (_window.RbDoGridOneNowyKlient.IsChecked == true)
                    {
                        if (_window.CmbDoGridOneDostawca.SelectedIndex < 0)
                        {
                            MessageBox.Show("Proszę wybrać dostawcę", "Bład", MessageBoxButton.OK);
                        }
                        else if (_window.CmbDoGridOneWojewodztwo.SelectedIndex < 0)
                        {
                            MessageBox.Show("Proszę wybrać Województwo", "Bład", MessageBoxButton.OK);
                        }
                        else
                        {
                            inProg.Show();
                            TransactionNew(cart);
                        }
                    }
                    if (_window.RbDoGridOneStalyKlient.IsChecked == true)
                    {
                        if (_window.CmbDoGridTwoDostawca.SelectedIndex < 0)
                        {
                            MessageBox.Show("Proszę wybrać dostawcę", "Bład", MessageBoxButton.OK);
                        }
                        else if (_window.CmbDoGridTwoWojewodztwo.SelectedIndex < 0)
                        {
                            MessageBox.Show("Proszę wybrać Województwo", "Bład", MessageBoxButton.OK);
                        }
                        else
                        {
                            inProg.Show();
                            TransactionOld(cart);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Proszę wybrać rodzaj klienta", "Bład", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller AddData: {ex} " + nameof(AddData));
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

        public IEnumerable<object> GetData()
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

                        ShowData();
                        return transakcje;
                    });
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller GetData: {ex} " + nameof(GetData));
            }
            return null;
        }

        public void SearchData()
        {
            try
            {
                transakcjeSearched.Clear();
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

                        foreach (Transakcja t in transakcje)
                        {
                            if (!list.Contains(t))
                            {
                                transakcjeSearched.Add(t);
                            }
                        }
                        ShowData();
                    }));
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller SearchData: {ex} " + nameof(SearchData));
            }
        }

        public void ShowData()
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _window.DgOverviewGridOne.Items.Clear();
                    if (_window.RbOverviewGridOneSzukaj.IsChecked == true)
                    {
                        foreach (Transakcja r in transakcjeSearched)
                            _window.DgOverviewGridOne.Items.Add(r);
                    }
                    if (_window.RbOverviewGridOneSzukaj.IsChecked == false)
                    {
                        foreach (Transakcja r in transakcje)
                            _window.DgOverviewGridOne.Items.Add(r);
                    }
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller ShowData: {ex} " + nameof(ShowData));
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
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller ShowSelectedData: {ex} " + nameof(ShowSelectedData));
            }
        }

        private void TransactionNew(List<Artykul_w_Koszyku> cart)
        {
            try
            {
                IValidate validator = TransactionNewValidator.GetInstance(_window);
                if (validator.Validate())
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
                        Nazwisko = _window.TxbDoGridOneNazwisko.Text,
                        Ksiazka_adresow = a
                    };
                    Dostawca d = (Dostawca)((ComboBoxItem)_window.CmbDoGridOneDostawca.SelectedItem).Tag;
                    Transakcja t = new Transakcja()
                    {
                        idKlienta = k.idKlienta,
                        idDostawcy = ((Dostawca)((ComboBoxItem)_window.CmbDoGridOneDostawca.SelectedItem).Tag).idDostawcy,
                        idPracownika = ID,
                        Data = DateTime.Now,
                        Klienci = k
                    };
                    foreach (Artykul_w_Koszyku r in _window.DgDoGridFive.Items)
                    {
                        for (int i = 0; i < r.Ilosc; i++)
                            t.Artykuly_w_transakcji.Add(new Artykul_w_transakcji() { Artykuly = r.Artykul, Cena = r.Cena, idArtykulu = r.Artykul.idKategorii });
                    }

                    TransactionRegister(t);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller TransactionNew: {ex} " + nameof(TransactionNew));
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
                            inProg = InProgress.GetInstance();
                            inProg.Hide();
                        }));

                        _window.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            GetData();
                            if (x.Result > 0)
                                MessageBox.Show("Wykonano pomyślnie!", "Transakcja", MessageBoxButton.OK);
                            if (x.Result == 0)
                                MessageBox.Show("Bład Transakcji", "Transakcja", MessageBoxButton.OK);
                        }));
                        return 0;
                    });
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller TransactionRegister: {ex} " + nameof(TransactionRegister));
            }
        }

        private void TransactionOld(List<Artykul_w_Koszyku> cart)
        {
            try
            {
                IValidate validator = TransactionOldValidator.GetInstance(_window);
                if (validator.Validate())
                {
                    Adres a = new Adres()
                    {
                        Kod_pocztowy = _window.TxbDoGridTwoKodPocztowy.Text,
                        Miejscowosc = _window.TxbDoGridTwoMiejscowosc.Text,
                        Wojewodztwo = _window.CmbDoGridTwoWojewodztwo.SelectedItem.ToString()
                    };
                    Klient k = ((Klient)((ComboBoxItem)_window.CmbDoGridTwoNazwisko.SelectedItem).Tag);
                    k.Ksiazka_adresow = a;
                    Dostawca d = (Dostawca)(((ComboBoxItem)_window.CmbDoGridTwoDostawca.SelectedItem).Tag);
                    System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}klient= {  k.idKlienta }");
                    System.Diagnostics.Debug.WriteLine($"{ Environment.NewLine}ID DOSTWAWCY= {((Dostawca)((ComboBoxItem)_window.CmbDoGridTwoDostawca.SelectedItem).Tag).idDostawcy}{Environment.NewLine}");
                    Transakcja t = new Transakcja()
                    {
                        idKlienta = k.idKlienta,
                        idDostawcy = ((Dostawca)((ComboBoxItem)_window.CmbDoGridTwoDostawca.SelectedItem).Tag).idDostawcy,
                        idPracownika = ID,
                        Data = DateTime.Now,
                        Klienci = k
                    };
                    foreach (Artykul_w_Koszyku r in _window.DgDoGridFive.Items)
                    {
                        for (int i = 0; i < r.Ilosc; i++)
                            t.Artykuly_w_transakcji.Add(new Artykul_w_transakcji() { Artykuly = r.Artykul, Cena = r.Cena, idArtykulu = r.Artykul.idKategorii });
                    }
                    TransactionRegister(t);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Transaction Controller TransactionOld: {ex} " + nameof(TransactionOld));
            }
        }
    }
}