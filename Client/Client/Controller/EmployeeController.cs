using Client.Communication;
using Client.Interfaces;
using Client.Model;
using Client.Validators;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Diagnostics.Debug;

namespace Client.Controller
{
    public class EmployeeController : IWork
    {
        private static EmployeeController _instance;
        private Admin _window;
        private ICommEmployee _comm { get; set; } = CommEmployee.GetInstance();
        private IEnumerable<Pracownik> employees;

        protected EmployeeController() { }

        public static EmployeeController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new EmployeeController();
            }
            _instance._window = window;
            return _instance;
        }
        public static EmployeeController GetInstance(ICommEmployee _comm)
        {
            if (_instance == null)
            {
                _instance = new EmployeeController();
            }
            _instance._comm = _comm;
            return _instance;
        }

        //Dodaj pracownika
        public void AddData()
        {
            try
            {
                IValidate validator = EmployeeAddValidator.GetInstance(_window);
                if (validator.Validate())
                {
                    int wiek = Int32.Parse(_window.TxbEmployeeWiek.Text);
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
                IValidate validator = EmployeeChangeValidator.GetInstance(_window);
                if (validator.Validate())
                {
                    int wiek = Int32.Parse(_window.TxbEmployeeWiek.Text);
                    {
                        Pracownik pracownik = new Pracownik()
                        {
                            idPracownika = ((Pracownik)_window.DgEmployeesList.SelectedItem).idPracownika,
                            Haslo = _window.TxbEmployeeHaslo.Text,
                            Imie = _window.TxbEmployeeImie.Text,
                            Login = _window.TxbEmployeeLogin.Text,
                            Nazwisko = _window.TxbEmployeeNazwisko.Text,
                            Sudo = (int)((ComboBoxItem)_window.CmbEmployeeAdmin.SelectedItem).Tag,
                            Wiek = wiek
                        };
                        Adres adres = new Adres()
                        {
                            idAdresu = ((Pracownik)_window.DgEmployeesList.SelectedItem).idAdresu,
                            Kod_pocztowy = _window.TxbEmployeeKodPocztowy.Text,
                            Miejscowosc = _window.TxbEmployeeMiejscowosc.Text,
                            Wojewodztwo = _window.CmbEmployeeWojewodztwo.SelectedItem + ""
                        };

                        Task.Factory.StartNew(() =>
                        {
                            _comm.ModifyEmployee(new PracownikAdress() { Pracownik = pracownik, Adres = adres }); ;
                        }).ContinueWith(x =>
                            Task.Factory.StartNew(() =>
                            {
                                GetData();
                            }));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(ChangeData)}: {ex} ");
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
                WriteLine($"Error in {nameof(_instance)}  {nameof(DeleteData)}: {ex} ");
            }
        }

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
                WriteLine($"Error in  {nameof(_instance)}  {nameof(ShowData)}: {ex} ");
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
                    for (int i = 0; i < _window.CmbEmployeeWojewodztwo.Items.Count; i++)
                    {
                        if (_window.CmbEmployeeWojewodztwo.Items.GetItemAt(i).Equals(pracownik.Ksiazka_adresow.Wojewodztwo))
                        {
                            _window.CmbEmployeeWojewodztwo.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(ShowSelectedData)}: {ex} ");
            }
        }

        //pobierz dane z BD a potem je pokaż
        public IEnumerable<object> GetData()
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
                  return employees;
              }));
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  { nameof(GetData)}: {ex} ");
            }
            return null;
        }

        public void SearchData()
        {
            throw new NotImplementedException();
        }
    }
}