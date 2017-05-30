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
    class ClientsController : IWork
    {
        private static ClientsController _instance;
        private Admin _window;
        private ICommunication _comm;
        private IEnumerable<Klient> clients;

        private ClientsController()
        {
            _comm = Communicator.GetInstance();
            _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
            //_comm.SetUrlAddress("http://localhost:52992");
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

        public void GetData()
        {
            Task<IEnumerable<Klient>>.Factory.StartNew(() =>
            {
                return _comm.GetClients();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowClientData(x.Result);
                });
            });
        }

        public void AddData()
        {
            if (_window.TxbClientsImie.Text.Length > 5 &&
              _window.TxbClientsNazwisko.Text.Length > 5 &&
              (_window.TxbClientsFirma.Text.Length > 5 || _window.TxbClientsFirma.Text.Length == 0) &&
             _window.CmbClientsWojewodztwo.SelectedIndex >= 0 &&
              _window.TxbClientsKodPocztowy.Text.Length == 6 &&
              _window.TxbClientsMiejscowosc.Text.Length > 5)
            {
                int id = _comm.RegisterAddress(new Adres()
                {
                    Kod_pocztowy = _window.TxbClientsKodPocztowy.Text,
                    Miejscowosc = _window.TxbClientsMiejscowosc.Text,
                    Wojewodztwo = _window.CmbClientsWojewodztwo.SelectedItem + ""
                });
                if (id > 0)
                    if (_comm.ChangeClient(new Klient()
                    {

                        Nazwa_firmy = _window.TxbClientsFirma.Text,
                        Imie = _window.TxbClientsImie.Text,
                        Nazwisko = _window.TxbClientsNazwisko.Text,
                        idAdresu = id,
                        Ksiazka_adresow = _comm.GetAddress(id)
                    }))
                    {
                        GetData();
                    }
            }
        }

        public void ChangeDate()
        {
            int id = ((Klient)_window.DgClientsLista.SelectedItem).idKlienta;
            if (_window.TxbClientsImie.Text.Length > 5 &&
                _window.TxbClientsNazwisko.Text.Length > 5 &&
                (_window.TxbClientsFirma.Text.Length > 5 || _window.TxbClientsFirma.Text.Length == 0) &&
               _window.CmbClientsWojewodztwo.SelectedIndex >= 0 &&
                _window.TxbClientsKodPocztowy.Text.Length == 6)
            {
                if (_comm.ChangeClient(new Klient()
                {
                    idKlienta = id,
                    Nazwa_firmy = _window.TxbClientsFirma.Text,
                    Imie = _window.TxbClientsImie.Text,
                    Nazwisko = _window.TxbClientsNazwisko.Text,
                    Transakcje = ((Klient)_window.DgClientsLista.SelectedItem).Transakcje,
                    idAdresu = ((Klient)_window.DgClientsLista.SelectedItem).idAdresu,
                    Ksiazka_adresow = ((Klient)_window.DgClientsLista.SelectedItem).Ksiazka_adresow
                }))
                {
                    GetData();
                }
            }
        }

        public void DeleteData()
        {
            if (_window.DgClientsLista.SelectedIndex >= 0)
            {
                _comm.DeleteClient(((Klient)_window.DgClientsLista.SelectedItem).idKlienta);
                GetData();
            }
        }

        public void ShowData()
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Klient r in clients)
                _window.DgClientsLista.Items.Add(r);
            }));
        }

        public void ShowSelectedData()
        {
            if (_window.DgClientsLista.SelectedIndex >= 0)
            {
                try
                {
                    _window.TxbClientsImie.Text = ((Klient)_window.DgClientsLista.SelectedItem).Imie;
                    _window.TxbClientsKodPocztowy.Text = ((Klient)_window.DgClientsLista.SelectedItem).Ksiazka_adresow.Kod_pocztowy;
                    _window.TxbClientsMiejscowosc.Text = ((Klient)_window.DgClientsLista.SelectedItem).Ksiazka_adresow.Miejscowosc;
                    _window.TxbClientsNazwisko.Text = ((Klient)_window.DgClientsLista.SelectedItem).Nazwisko;
                    _window.CmbClientsWojewodztwo.SelectedItem = ((Klient)_window.DgClientsLista.SelectedItem).Ksiazka_adresow.Wojewodztwo;
                    _window.LblClientsIloscTransakcji.Content = $"Ilość transakcji: {((Klient)_window.DgClientsLista.SelectedItem).Transakcje.Count}";
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}ERROR: {ex}");
                }
            }
        }

        public void SearchData()
        {
            List<Klient> list = new List<Klient>();
            for (int i = 0; i < _window.DgClientsLista.Items.Count; i++)
                list.Add((Klient)_window.DgClientsLista.Items.GetItemAt(i));
            _window.DgClientsLista.Items.Clear();
            //1
            if (_window.ChbClientsImie.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!list[i].Imie.ToLower().Equals(_window.TxbClientsImieSearch.Text.ToLower()))
                        list.Remove(list[i]);
                }
            }
            //2
            if (_window.ChbClientsKodPocztowy.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!list[i].Ksiazka_adresow.Kod_pocztowy.Equals(_window.TxbClientsKodPocztowySearch.Text))
                        list.Remove(list[i]);
                }
            }
            //3
            if (_window.ChbClientsMiejscowosc.IsChecked == true)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    if (!list[i].Ksiazka_adresow.Miejscowosc.ToLower().Equals(_window.TxbClientsMiejscowoscSearch.Text.ToLower()))
                        list.Remove(list[i]);
                }

            }
            //4
            if (_window.ChbClientsWojewodztwo.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!list[i].Ksiazka_adresow.Wojewodztwo.ToLower().Equals(_window.TxbClientsWojewodztwoSearch.Text.ToLower()))
                        list.Remove(list[i]);
                }
            }
            ShowClientData(list);
        }
    }
}
