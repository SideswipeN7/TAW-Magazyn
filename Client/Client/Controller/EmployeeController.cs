﻿using Client.Communication;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.Controller
{
    internal class EmployeeController : IWork
    {
        private static EmployeeController _instance;
        private Admin _window;
        private ICommunication _comm;
        private IEnumerable<Pracownik> employees;

        private EmployeeController()
        {
            _comm = Communicator.GetInstance();
            _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
            //_comm.SetUrlAddress("http://localhost:52992");
        }

        public static EmployeeController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new EmployeeController();
            }
            _instance._window = window;
            return _instance;
        }

        //Dodaj pracownika
        public void AddData()
        {
            try
            {
                int wiek;
                if (_window.TxbEmployeeHaslo.Text.Length > 5
                    && _window.TxbEmployeeLogin.Text.Length > 5
                    && _window.TxbEmployeeImie.Text.Length > 5
                    && _window.TxbEmployeeNazwisko.Text.Length > 5
                    && _window.TxbEmployeeMiejscowosc.Text.Length > 5
                    && _window.TxbEmployeeKodPocztowy.Text.Length == 6
                    && Int32.TryParse(_window.TxbEmployeeWiek.Text, out wiek)
                    && _window.CmbEmployeeAdmin.SelectedIndex > 0
                    && _window.CmbEmployeeWojewodztwo.SelectedIndex >= 0)
                {
                    Pracownik pracownik = new Pracownik()
                    {
                        Haslo = _window.TxbEmployeeHaslo.Text,
                        Imie = _window.TxbEmployeeImie.Text,
                        Login = _window.TxbEmployeeLogin.Text,
                        Nazwisko = _window.TxbEmployeeNazwisko.Text,
                        Sudo = (int)((ComboBoxItem)_window.CmbEmployeeAdmin.SelectedItem).Tag,
                        Wiek = wiek
                    };
                    Adres adres = new Adres()
                    {
                        Kod_pocztowy = _window.TxbEmployeeKodPocztowy.Text,
                        Miejscowosc = _window.TxbEmployeeMiejscowosc.Text,
                        Wojewodztwo = _window.CmbEmployeeWojewodztwo.SelectedItem + ""
                    };

                    Task<bool>.Factory.StartNew(() =>
                    {
                        return _comm.RegisterEmployee(new PracownikAdress() { Pracownik = pracownik, Adres = adres }); ;
                    }).ContinueWith(x =>
                        Task.Factory.StartNew(() =>
                        {
                            GetData();
                        }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Employee Controller AddData: {ex}");
            }
        }

        //Zmodyfikuj pracownika
        public void ChangeDate()
        {
            try
            {
                int wiek;
                if (_window.TxbEmployeeHaslo.Text.Length > 5
                    && _window.TxbEmployeeLogin.Text.Length > 5
                    && _window.TxbEmployeeImie.Text.Length > 5
                    && _window.TxbEmployeeNazwisko.Text.Length > 5
                    && _window.TxbEmployeeMiejscowosc.Text.Length > 5
                    && _window.TxbEmployeeKodPocztowy.Text.Length == 6
                    && Int32.TryParse(_window.TxbEmployeeWiek.Text, out wiek)
                    && _window.CmbEmployeeAdmin.SelectedIndex > 0
                    && _window.CmbEmployeeWojewodztwo.SelectedIndex >= 0)
                {
                    Pracownik pracownik = new Pracownik()
                    {
                        Haslo = _window.TxbEmployeeHaslo.Text,
                        Imie = _window.TxbEmployeeImie.Text,
                        Login = _window.TxbEmployeeLogin.Text,
                        Nazwisko = _window.TxbEmployeeNazwisko.Text,
                        Sudo = (int)((ComboBoxItem)_window.CmbEmployeeAdmin.SelectedItem).Tag,
                        Wiek = wiek
                    };
                    Adres adres = new Adres()
                    {
                        Kod_pocztowy = _window.TxbEmployeeKodPocztowy.Text,
                        Miejscowosc = _window.TxbEmployeeMiejscowosc.Text,
                        Wojewodztwo = _window.CmbEmployeeWojewodztwo.SelectedItem + ""
                    };

                    Task<bool>.Factory.StartNew(() =>
                    {
                        return _comm.RegisterEmployee(new PracownikAdress() { Pracownik = pracownik, Adres = adres }); ;
                    }).ContinueWith(x =>
                        Task.Factory.StartNew(() =>
                        {
                            GetData();
                        }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Employee Controller ChangeData: {ex}");
            }
        }

        //Usuń pracownika
        public void DeleteData()
        {
            try
            {
                if (_window.DgEmployeesList.SelectedIndex >= 0)
                {
                    _comm.DeleteEmployee(((Pracownik)_window.DgEmployeesList.SelectedItem).idPracownika);
                    GetData();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Employee Controller DeleteData: {ex}");
            }
        }

        //Pokaż listę dostępnych pracowników
        public void ShowData()
        {
            try
            {
                if (employees != null)
                {
                    _window.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _window.DgEmployeesList.Items.Clear();
                        foreach (Pracownik p in employees)
                        {
                            _window.DgEmployeesList.Items.Add(p);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Employee Controller ShowData: {ex}");
            }
        }

        //Pokaż dane jednego wybranego pracownika
        public void ShowSelectedData()
        {
            try
            {
                if (_window.DgEmployeesList.SelectedIndex >= 0)
                {
                    Pracownik pracownik = (Pracownik)_window.DgEmployeesList.SelectedItem;
                    _window.TxbEmployeeHaslo.Text = "*********";
                    _window.TxbEmployeeLogin.Text = pracownik.Login;
                    _window.TxbEmployeeImie.Text = pracownik.Imie;
                    _window.TxbEmployeeNazwisko.Text = pracownik.Nazwisko;
                    _window.TxbEmployeeMiejscowosc.Text = pracownik.Ksiazka_adresow.Miejscowosc;
                    _window.TxbEmployeeKodPocztowy.Text = pracownik.Ksiazka_adresow.Kod_pocztowy;
                    _window.TxbEmployeeWiek.Text = pracownik.Wiek + "";
                    if (pracownik.Sudo == 0)
                    {
                        _window.CmbEmployeeAdmin.SelectedIndex = 0;
                    }
                    if (pracownik.Sudo == 1)
                    {
                        _window.CmbEmployeeAdmin.SelectedIndex = 1;
                    }
                    _window.CmbEmployeeWojewodztwo.SelectedItem = pracownik.Ksiazka_adresow.Wojewodztwo;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Employee Controller ShowSelectedData: {ex}");
            }
        }

        //pobierz dane z BD a potem je pokaż
        public void GetData()
        {
            try
            {
                Task<IEnumerable<Pracownik>>.Factory.StartNew(() =>
                {
                    return _comm.GetEmpoyees();
                }).ContinueWith(x => Task.Factory.StartNew(() =>
              {
                  employees = x.Result;
                  ShowData();
              }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Employee Controller GetData: {ex}");
            }
        }
    }
}