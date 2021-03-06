﻿using Client.Communication;
using Client.Interfaces;
using Client.Model;
using Client.Validators;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;

namespace Client.Controller
{
    public class ClientsController : IWork
    {
        private static ClientsController _instance;
        private Admin _window;
        private ICommClient _comm { get; set; } = CommClient.GetInstance();
        private List<Klient> clients;
        private List<Klient> clientsSearched;

        protected ClientsController()
        {
            clientsSearched = new List<Klient>();
        }

        public static ClientsController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new ClientsController();
            }
            _instance._window = window;
            return _instance;
        }
        public static ClientsController GetInstance(ICommClient _comm)
        {
            if (_instance == null)
            {
                _instance = new ClientsController();
            }
            _instance._comm = _comm;
            return _instance;
        }

        public IEnumerable<object> GetData()
        {
            try
            {
                Task<IEnumerable<Klient>>.Factory.StartNew(() =>
                {
                    return _comm.GetClients();
                }).ContinueWith(x =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        clients = x.Result.ToList();
                        ShowData();
                        return clients;
                    });

                });
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(GetData)}: {ex} ");
            }
            return null;
        }

        public void AddData()
        {
            try
            {
                IValidate validator = ClientValidator.GetInstance(_window);
                if (validator.Validate())
                {
                    Adres adres = new Adres()
                    {
                        Kod_pocztowy = _window.TxbClientsKodPocztowy.Text,
                        Miejscowosc = _window.TxbClientsMiejscowosc.Text,
                        Wojewodztwo = _window.CmbClientsWojewodztwo.SelectedItem + ""
                    };
                    Klient klient = new Klient()
                    {
                        Nazwa_firmy = _window.TxbClientsFirma.Text,
                        Imie = _window.TxbClientsImie.Text,
                        Nazwisko = _window.TxbClientsNazwisko.Text,
                    };

                    _comm.RegisterClient(new KlientAdress() { Adres = adres, Klient = klient });
                    GetData();
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(AddData)}: {ex} ");
            }
        }

        public void ChangeData()
        {
            try
            {
                IValidate validator = ClientValidator.GetInstance(_window);
                if (validator.Validate())
                {
                    Adres adres = new Adres()
                    {
                        idAdresu = ((Klient)_window.DgClientsLista.SelectedItem).Ksiazka_adresow.idAdresu,
                        Kod_pocztowy = _window.TxbClientsKodPocztowy.Text,
                        Miejscowosc = _window.TxbClientsMiejscowosc.Text,
                        Wojewodztwo = _window.CmbClientsWojewodztwo.SelectedItem + ""
                    };
                    Klient klient = new Klient()
                    {
                        idKlienta = ((Klient)_window.DgClientsLista.SelectedItem).idKlienta,
                        Nazwa_firmy = _window.TxbClientsFirma.Text,
                        Imie = _window.TxbClientsImie.Text,
                        Nazwisko = _window.TxbClientsNazwisko.Text,
                        idAdresu = adres.idAdresu,
                        Ksiazka_adresow = adres
                    };

                    _comm.ChangeAddress(adres);
                    _comm.ChangeClient(klient);
                    GetData();
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(ChangeData)}: {ex} ");
            }
        }

        public void DeleteData()
        {
            try
            {
                if (_window.DgClientsLista.SelectedIndex >= 0)
                {
                    _comm.DeleteClient(((Klient)_window.DgClientsLista.SelectedItem).idKlienta);
                    GetData();
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in {nameof(_instance)}  { nameof(DeleteData)}: {ex} ");
            }
        }

        public void ShowData()
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _window.DgClientsLista.Items.Clear();
                    if (_window.RbClientsSzukaj.IsChecked == false)
                    {
                        foreach (Klient r in clients)
                            _window.DgClientsLista.Items.Add(r);
                    }
                    if (_window.RbClientsSzukaj.IsChecked == true)
                    {
                        foreach (Klient r in clientsSearched)
                            _window.DgClientsLista.Items.Add(r);
                    }
                }));
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(ShowData)}: {ex} ");
            }
        }

        public void ShowSelectedData()
        {
            try
            {
                if (_window.DgClientsLista.SelectedIndex >= 0)
                {
                    _window.TxbClientsImie.Text = ((Klient)_window.DgClientsLista.SelectedItem).Imie;
                    _window.TxbClientsKodPocztowy.Text = ((Klient)_window.DgClientsLista.SelectedItem).Ksiazka_adresow.Kod_pocztowy;
                    _window.TxbClientsMiejscowosc.Text = ((Klient)_window.DgClientsLista.SelectedItem).Ksiazka_adresow.Miejscowosc;
                    _window.TxbClientsNazwisko.Text = ((Klient)_window.DgClientsLista.SelectedItem).Nazwisko;
                    _window.CmbClientsWojewodztwo.SelectedItem = ((Klient)_window.DgClientsLista.SelectedItem).Ksiazka_adresow.Wojewodztwo;
                    // _window.LblClientsIloscTransakcji.Content = $"Ilość transakcji: {((Klient)_window.DgClientsLista.SelectedItem).Transakcje.Count}";
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in {nameof(_instance)}  {nameof(ShowSelectedData)}: {ex} ");
            }
        }

        public void SearchData()
        {
            try
            {
                clientsSearched.Clear();
                Task.Factory.StartNew(() =>
                {
                    GetData();
                }).ContinueWith(x =>
                Task.Factory.StartNew(() =>
                {
                    _window.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        List<Klient> list = new List<Klient>();
                        //1
                        if (_window.ChbClientsNazwisko.IsChecked == true)
                        {
                            if (_window.TxbClientsNazwiskoSearch.Text.Length > 0)
                                foreach (Klient a in clients)
                                {
                                    if (!a.Nazwisko.ToLower().Contains(_window.TxbClientsNazwiskoSearch.Text.ToLower()))
                                    {
                                        list.Add(a);
                                    }
                                }
                        }
                        //2
                        if (_window.ChbClientsMiejscowosc.IsChecked == true)
                        {
                            if (_window.TxbClientsMiejscowosc.Text.Length > 0)
                                foreach (Klient a in clients)
                                {
                                    if (!a.Ksiazka_adresow.Miejscowosc.ToLower().Contains(_window.TxbClientsMiejscowosc.Text.ToLower()))
                                    {
                                        list.Add(a);
                                    }
                                }
                        }
                        //3
                        if (_window.ChbClientsFirma.IsChecked == true)
                        {
                            if (_window.TxbClientsFirmaSearch.Text.Length > 0)
                                foreach (Klient a in clients)
                                {
                                    if (!a.Nazwa_firmy.ToLower().Contains(_window.TxbClientsFirmaSearch.Text.ToLower()))
                                    {
                                        list.Add(a);
                                    }
                                }
                        }
                        //4
                        if (_window.ChbClientsWojewodztwo.IsChecked == true)
                        {
                            if (_window.CmbClientsWojewodztwoSearch.SelectedIndex >= 0)
                                foreach (Klient a in clients)
                                {
                                    if (!a.Ksiazka_adresow.Wojewodztwo.Equals((string)_window.CmbClientsWojewodztwoSearch.SelectedItem))
                                    {
                                        list.Add(a);
                                    }
                                }
                        }
                        foreach (Klient a in clients)
                        {
                            if (!list.Contains(a))
                                clientsSearched.Add(a);
                        }
                        ShowData();
                    }));
                }));
            }
            catch (Exception ex)
            {
                WriteLine($"Error in C {nameof(_instance)}  {nameof(SearchData)}: {ex} ");
            }
        }
    }
}