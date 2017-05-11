using Client.Communication;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Controller
{
    public sealed class Manager : IManager
    {
        private static Manager _instance;
        private MainWindow _window;
        private ICommunication _comm;

        private Manager()
        {
            _comm = Communicator.GetInstance();
            _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
        }
        public static Manager GetInstance(MainWindow window)
        {
            if (_instance == null)
            {
                _instance = new Manager();
            }
            _instance._window = window;
            return _instance;
        }


        //Magazine State
        public void LoadCategoriesMagazineSate()
        {
            IEnumerable<Kategoria> list = _comm.GetCategories();

            foreach (Kategoria r in list)
            {
                _window.CmbStateKategoria.Items.Add(new ComboBoxItem() { Content = r.Nazwa, Tag = r.idKategorii });

            }
        }



        public void SearchCategoriesSate()
        {
            List<Artykul> list = new List<Artykul>();
            for (int i = 0; i < _window.DgStateLista.Items.Count; i++)
                list.Add((Artykul)_window.DgStateLista.Items.GetItemAt(i));
            _window.DgStateLista.Items.Clear();
            //1
            if (_window.ChbStateCena.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Cena < Decimal.Parse(_window.TxbStateCenaMin.Text) || list[i].Cena > Decimal.Parse(_window.TxbStateCenaMax.Text))
                        list.Remove(list[i]);
                }
            }
            //2
            if (_window.ChbStateIlosc.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Ilosc > Int32.Parse(_window.TxbStateIlosc.Text))
                        list.Remove(list[i]);
                }
            }
            //3
            if (_window.ChbStateKategoria.IsChecked == true)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    //TODO
                    if (list[i].idKategorii != (int)((ComboBoxItem)_window.CmbStateKategoria.SelectedItem).Tag)
                        list.Remove(list[i]);
                }

            }
            //4
            if (_window.ChbStateNazwa.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!(list[i].Nazwa.ToLower().Contains(_window.TxbStateNazwa.Text.ToLower())))
                        list.Remove(list[i]);
                }
            }
            ShowMagazineStateData(list);

        }

        public void ShowMagazineStateAll()
        {
            _window.ChbStateCena.Visibility = Visibility.Hidden;
            _window.ChbStateIlosc.Visibility = Visibility.Hidden;
            _window.ChbStateKategoria.Visibility = Visibility.Hidden;
            _window.ChbStateNazwa.Visibility = Visibility.Hidden;

            _window.LblStateCena.Visibility = Visibility.Hidden;
            _window.LblStateIlosc.Visibility = Visibility.Hidden;
            _window.LblStateKategoria.Visibility = Visibility.Hidden;
            _window.LblStateNazwa.Visibility = Visibility.Hidden;

            _window.TxbStateCenaMin.Visibility = Visibility.Hidden;
            _window.TxbStateCenaMax.Visibility = Visibility.Hidden;
            _window.TxbStateIlosc.Visibility = Visibility.Hidden;
            _window.TxbStateNazwa.Visibility = Visibility.Hidden;
            _window.CmbStateKategoria.Visibility = Visibility.Hidden;

            _window.BtnSateSzukaj.Visibility = Visibility.Hidden;

            GetMagazineState();
        }

        public void ShowMagazineStateSearch()
        {
            _window.ChbStateCena.Visibility = Visibility.Visible;
            _window.ChbStateIlosc.Visibility = Visibility.Visible;
            _window.ChbStateKategoria.Visibility = Visibility.Visible;
            _window.ChbStateNazwa.Visibility = Visibility.Visible;

            _window.LblStateCena.Visibility = Visibility.Visible;
            _window.LblStateIlosc.Visibility = Visibility.Visible;
            _window.LblStateKategoria.Visibility = Visibility.Visible;
            _window.LblStateNazwa.Visibility = Visibility.Visible;

            _window.TxbStateCenaMin.Visibility = Visibility.Visible;
            _window.TxbStateCenaMax.Visibility = Visibility.Visible;
            _window.TxbStateIlosc.Visibility = Visibility.Visible;
            _window.TxbStateNazwa.Visibility = Visibility.Visible;
            _window.CmbStateKategoria.Visibility = Visibility.Visible;

            _window.BtnSateSzukaj.Visibility = Visibility.Visible;
        }

        public void GetMagazineState()
        {
            _window.DgStateLista.Items.Clear();
            Task<IEnumerable<Artykul>>.Factory.StartNew(() =>
            {
                return _comm.GetItems();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowMagazineStateData(x.Result);
                });
            });
        }
        public void ShowMagazineStateData(IEnumerable<Artykul> categories)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Artykul r in categories)
                    _window.DgStateLista.Items.Add(r);

            }));
        }
        //Categories
        public void GetCategoryData()
        {
            Task<IEnumerable<Kategoria>>.Factory.StartNew(() =>
            {
                return _comm.GetCategories();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowCategoryData(x.Result);
                });
            });
        }
        public void ShowCategoryData(IEnumerable<Kategoria> categories)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Kategoria r in categories)
                    _window.DgCategoryLista.Items.Add(r);

            }));
        }

        public void ShowCategoriesSate()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Hidden;
            _window.LblCategoryNazwa.Visibility = Visibility.Hidden;

            _window.TxbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.CmbCategoryId.Visibility = Visibility.Hidden;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        public void ShowCategoriesAdd()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Hidden;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Hidden;

            _window.BtnCategoryDodaj.Visibility = Visibility.Visible;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        public void ShowCategoriesSearch()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Visible;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Visible;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        public void ShowCategoriesDelete()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Visible;
        }

        public void ShowCategoriesModify()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Visible;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        public void SetCategoryData()
        {
            if (_window.TxbCategoryNazwa.Text.Length > 5)
            {
                if (_comm.RegisterCategory(new Kategoria() { Nazwa = _window.TxbCategoryNazwa.Text }))
                {
                    GetCategoryData();
                }
            }
        }

        public void ChangeCategoryData()
        {
            if (_window.TxbCategoryNazwa.Text.Length > 5)
            {
                if (_window.CmbCategoryId.SelectedIndex > 0)
                {
                    if (_comm.ChangeCategory(new Kategoria() { idKategorii = (int)_window.CmbCategoryId.SelectedItem, Nazwa = _window.TxbCategoryNazwa.Text }))
                    {
                        GetCategoryData();
                    }

                }
            }
        }

        public void CategoriesDelete()
        {
            //TODO
            throw new NotImplementedException();
        }

        public void CategoriesSearch()
        {
            List<Kategoria> list = new List<Kategoria>();
            for (int i = 0; i < _window.DgCategoryLista.Items.Count; i++)
                list.Add((Kategoria)_window.DgCategoryLista.Items.GetItemAt(i));
            _window.DgCategoryLista.Items.Clear();
            //1
            if (_window.ChbCategoryNazwa.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Nazwa.ToLower().Contains(_window.TxbCategoryNazwa.Text.ToLower()))
                        list.Remove(list[i]);
                }
                GetCategoryData();
            }
        }

        //Items
        public void ShowItemsSearch()
        {
            _window.ChbItemCena.Visibility = Visibility.Visible;
            _window.ChbItemIlosc.Visibility = Visibility.Visible;
            _window.ChbItemKategoria.Visibility = Visibility.Visible;
            _window.ChbItemNazwa.Visibility = Visibility.Visible;

            _window.LblItemCena.Visibility = Visibility.Visible;
            _window.LblItemIlosc.Visibility = Visibility.Visible;
            _window.LblItemKategoria.Visibility = Visibility.Visible;
            _window.LblItemNazwa.Visibility = Visibility.Visible;

            _window.TxbItemCenaMax.Visibility = Visibility.Visible;
            _window.TxbItemCenaMin.Visibility = Visibility.Visible;
            _window.TxbItemIlosc.Visibility = Visibility.Visible;
            _window.TxbItemINazwa.Visibility = Visibility.Visible;

            _window.CmbItemKategoria.Visibility = Visibility.Visible;

            _window.BtnItemDodaj.Visibility = Visibility.Hidden;
            _window.BtnItemModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnItemSzukaj.Visibility = Visibility.Visible;
        }

        public void ShowItemsModify()
        {
            _window.ChbItemCena.Visibility = Visibility.Hidden;
            _window.ChbItemIlosc.Visibility = Visibility.Hidden;
            _window.ChbItemKategoria.Visibility = Visibility.Hidden;
            _window.ChbItemNazwa.Visibility = Visibility.Hidden;

            _window.LblItemCena.Visibility = Visibility.Visible;
            _window.LblItemIlosc.Visibility = Visibility.Visible;
            _window.LblItemKategoria.Visibility = Visibility.Visible;
            _window.LblItemNazwa.Visibility = Visibility.Visible;

            _window.TxbItemCenaMax.Visibility = Visibility.Hidden;
            _window.TxbItemCenaMin.Visibility = Visibility.Visible;
            _window.TxbItemIlosc.Visibility = Visibility.Visible;
            _window.TxbItemINazwa.Visibility = Visibility.Visible;

            _window.CmbItemKategoria.Visibility = Visibility.Visible;

            _window.BtnItemDodaj.Visibility = Visibility.Hidden;
            _window.BtnItemModyfikuj.Visibility = Visibility.Visible;
            _window.BtnItemSzukaj.Visibility = Visibility.Hidden;
        }

        public void ShowItemsAdd()
        {
            _window.ChbItemCena.Visibility = Visibility.Hidden;
            _window.ChbItemIlosc.Visibility = Visibility.Hidden;
            _window.ChbItemKategoria.Visibility = Visibility.Hidden;
            _window.ChbItemNazwa.Visibility = Visibility.Hidden;

            _window.LblItemCena.Visibility = Visibility.Visible;
            _window.LblItemIlosc.Visibility = Visibility.Visible;
            _window.LblItemKategoria.Visibility = Visibility.Visible;
            _window.LblItemNazwa.Visibility = Visibility.Visible;

            _window.TxbItemCenaMax.Visibility = Visibility.Hidden;
            _window.TxbItemCenaMin.Visibility = Visibility.Visible;
            _window.TxbItemIlosc.Visibility = Visibility.Visible;
            _window.TxbItemINazwa.Visibility = Visibility.Visible;

            _window.CmbItemKategoria.Visibility = Visibility.Visible;

            _window.BtnItemDodaj.Visibility = Visibility.Visible;
            _window.BtnItemModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnItemSzukaj.Visibility = Visibility.Hidden;
        }

        public void ShowItemsAll()
        {
            _window.ChbItemCena.Visibility = Visibility.Hidden;
            _window.ChbItemIlosc.Visibility = Visibility.Hidden;
            _window.ChbItemKategoria.Visibility = Visibility.Hidden;
            _window.ChbItemNazwa.Visibility = Visibility.Hidden;

            _window.LblItemCena.Visibility = Visibility.Hidden;
            _window.LblItemIlosc.Visibility = Visibility.Hidden;
            _window.LblItemKategoria.Visibility = Visibility.Hidden;
            _window.LblItemNazwa.Visibility = Visibility.Hidden;

            _window.TxbItemCenaMax.Visibility = Visibility.Hidden;
            _window.TxbItemCenaMin.Visibility = Visibility.Hidden; _window.TxbItemCenaMax.Visibility = Visibility.Hidden;
            _window.TxbItemIlosc.Visibility = Visibility.Hidden;
            _window.TxbItemINazwa.Visibility = Visibility.Hidden;

            _window.CmbItemKategoria.Visibility = Visibility.Hidden;

            _window.BtnItemDodaj.Visibility = Visibility.Hidden;
            _window.BtnItemModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnItemSzukaj.Visibility = Visibility.Hidden;

        }

        public void ChangeItemData()
        {
            int quantity;
            decimal price;
            int id = ((Artykul)_window.DgItemLista.SelectedItem).idArtykulu;
            if (_window.TxbItemINazwa.Text.Length > 5 &&
                Int32.TryParse(_window.TxbItemIlosc.Text, out quantity) &&
                Decimal.TryParse(_window.TxbItemCenaMin.Text, out price) &&
                _window.CmbItemKategoria.SelectedIndex > 0)
            {
                if (_comm.ChangeItem(new Artykul() { idArtykulu = id, Cena = price, Ilosc = quantity, Nazwa = _window.TxbItemINazwa.Text, idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Kategorie = new Kategoria() { idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content } }))
                {
                    GetItemData();
                }
            }
        }

        public void SetItemData()
        {
            int quantity;
            decimal price;
            if (_window.TxbItemINazwa.Text.Length > 5 &&
                 Int32.TryParse(_window.TxbItemIlosc.Text, out quantity) &&
                 Decimal.TryParse(_window.TxbItemCenaMin.Text, out price) &&
                 _window.CmbItemKategoria.SelectedIndex > 0)
            {
                if (_comm.RegisterItem(new Artykul() { Cena = price, Ilosc = quantity, Nazwa = _window.TxbItemINazwa.Text, idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Kategorie = new Kategoria() { idKategorii = (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag, Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content } }))
                {
                    GetItemData();
                }
            }
        }

        public void SearchItems()
        {
            List<Artykul> list = new List<Artykul>();
            for (int i = 0; i < _window.DgItemLista.Items.Count; i++)
                list.Add((Artykul)_window.DgItemLista.Items.GetItemAt(i));
            _window.DgItemLista.Items.Clear();
            //1
            if (_window.ChbItemCena.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Cena < Decimal.Parse(_window.TxbItemCenaMin.Text) || list[i].Cena > Decimal.Parse(_window.TxbItemCenaMax.Text))
                        list.Remove(list[i]);
                }
            }
            //2
            if (_window.ChbItemIlosc.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Ilosc > Int32.Parse(_window.TxbItemIlosc.Text))
                        list.Remove(list[i]);
                }
            }
            //3
            if (_window.ChbItemKategoria.IsChecked == true)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    //TODO
                    if (list[i].idKategorii != (int)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag)
                        list.Remove(list[i]);
                }

            }
            //4
            if (_window.ChbItemNazwa.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!(list[i].Nazwa.ToLower().Contains(_window.TxbItemINazwa.Text.ToLower())))
                        list.Remove(list[i]);
                }
            }
            ShowItemData(list);
        }
        public void GetItemData()
        {
            Task<IEnumerable<Artykul>>.Factory.StartNew(() =>
            {
                return _comm.GetItems();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowItemData(x.Result);
                });
            });
        }

        public void ShowItemData(IEnumerable<Artykul> categories)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Artykul r in categories)
                    _window.DgItemLista.Items.Add(r);

            }));
        }
        //Client
        internal void ShowClitentsAdd()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Visible;
        }

        internal void ShowClitentsModify()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Visible;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
        }

        internal void ShowClitentsDelete()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Visible;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
        }

        internal void ShowClitentsSearch()
        {
            _window.GridClientsSearch.Visibility = Visibility.Visible;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Visible;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
        }

        internal void ShowClitentsAll()
        {
            _window.GridClientsSearch.Visibility = Visibility.Hidden;
            _window.BtnClientsModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnClientsSzukaj.Visibility = Visibility.Hidden;
            _window.BtnClientsUsun.Visibility = Visibility.Hidden;
            _window.BtnClientsDodaj.Visibility = Visibility.Hidden;
        }


        public void ChangeClientData()
        {
            int id = ((Klient)_window.DgClientsLista.SelectedItem).idKlienta;
            if (_window.TxbClientsImie.Text.Length > 5 &&
                _window.TxbClientsNazwisko.Text.Length > 5 &&
                (_window.TxbClientsFirma.Text.Length > 5 || _window.TxbClientsFirma.Text.Length == 0) &&
               _window.TxbClientsWojewodztwo.Text.Length > 5 &&
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
                    GetClientData();
                }
            }
        }

        public void ShowClientData(IEnumerable<Klient> clients)
        {

            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Klient r in clients)
                    _window.DgClientsLista.Items.Add(r);
            }));
        }

        public void GetClientData()
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



        public void SetClientData()
        {
            if (_window.TxbClientsImie.Text.Length > 5 &&
              _window.TxbClientsNazwisko.Text.Length > 5 &&
              (_window.TxbClientsFirma.Text.Length > 5 || _window.TxbClientsFirma.Text.Length == 0) &&
             _window.TxbClientsWojewodztwo.Text.Length > 5 &&
              _window.TxbClientsKodPocztowy.Text.Length == 6 &&
              _window.TxbClientsMiejscowosc.Text.Length > 5)
            {
                int id = _comm.RegisterAddress(new Adres()
                {
                    Kod_pocztowy = _window.TxbClientsKodPocztowy.Text,
                    Miejscowosc = _window.TxbClientsMiejscowosc.Text,
                    Wojewodztwo = _window.TxbClientsWojewodztwo.Text
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
                        GetClientData();
                    }
            }
        }

        public void ClientDelete()
        {
            //TODO
            throw new NotImplementedException();
        }

        public void SearchClients()
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

        //Transactions

        public void GetClientTransactionData()
        {
            throw new NotImplementedException();
        }
        public void SetClientTransactionData()
        {
            throw new NotImplementedException();
        }
        public void ShowClientTransactionData()
        {
            throw new NotImplementedException();
        }

        public void ShowClientData()
        {
            throw new NotImplementedException();
        }
        //DO
        public void LoadTransactionsDoStates()
        {
            _window.CmbDoGridOneWojewodztwo.Items.Clear();
            _window.CmbDoGridTwoWojewodztwo.Items.Clear();
            foreach (String r in states)
            {
                _window.CmbDoGridOneWojewodztwo.Items.Add(r);
                _window.CmbDoGridTwoWojewodztwo.Items.Add(r);
            }
        }

        public void LoadTransactionsDoSupplier()
        {
            _window.CmbDoGridOneWojewodztwo.Items.Clear();
            _window.CmbDoGridTwoWojewodztwo.Items.Clear();
            IEnumerable<Dostawca> list = _comm.GetSuppliers();
            foreach (Dostawca r in list)
            {
                _window.CmbDoGridOneWojewodztwo.Items.Add(new ComboBoxItem() { Name = r.Nazwa, Tag = r.idDostawcy });
                _window.CmbDoGridTwoWojewodztwo.Items.Add(new ComboBoxItem() { Name = r.Nazwa, Tag = r.idDostawcy });
            }
        }
        public void LoadTransactionsDoProducts()
        {
            _window.CmbDoGridThreeNazwa.Items.Clear();
            IEnumerable<Artykul> list = _comm.GetItems();
            foreach (Artykul r in list)
            {
                _window.CmbDoGridThreeNazwa.Items.Add(new ComboBoxItem() { Name = r.Nazwa, Tag = r });
            }
        }
        public void LoadClients()
        {
            LoadClientsToCmb(_comm.GetClients());
        }
        private void LoadClientsToCmb(IEnumerable<Klient> clients)
        {
            _window.CmbDoGridTwoNazwisko.Items.Clear();
            _window.CmbDoGridTwoFirma.Items.Clear();
            _window.CmbDoGridTwoFirma.Items.Add(new ComboBoxItem() { Name = "" });
            foreach (Klient r in clients)
            {
                _window.CmbDoGridTwoNazwisko.Items.Add(new ComboBoxItem() { Name = r.Nazwisko, Tag = r });
                if (r.Nazwa_firmy.Length > 0)
                {
                    _window.CmbDoGridTwoFirma.Items.Add(new ComboBoxItem() { Name = r.Nazwa_firmy, Tag = r });
                }
            }
        }

        public void AddToCart()
        {
            if (_window.CmbDoGridThreeNazwa.SelectedIndex > 1)
            {
                int quantity = ((Artykul)((ComboBoxItem)_window.CmbDoGridThreeNazwa.SelectedItem).Tag).Ilosc;
                int selQuan;
                if (Int32.TryParse(_window.TxbDoGridThreeIlosc.Text, out selQuan))
                    if (selQuan <= quantity && selQuan > 0)
                    {
                        _window.DgDoGridFive.Items.Add(new Artykul_w_Koszyku() { Artykul = (Artykul)((ComboBoxItem)_window.CmbDoGridThreeNazwa.SelectedItem).Tag, CenaCalosciowa = ((Artykul)((ComboBoxItem)_window.CmbDoGridThreeNazwa.SelectedItem).Tag).Cena * selQuan, Cena = ((Artykul)((ComboBoxItem)_window.CmbDoGridThreeNazwa.SelectedItem).Tag).Cena, Ilosc = selQuan });
                        UpadtePrice();
                    }
            }
        }
        private void UpadtePrice()
        {
            decimal price = 0m;
            foreach (Artykul_w_Koszyku r in _window.DgDoGridFive.Items)
            {
                price += r.CenaCalosciowa;
            }
            _window.LblDoFourCena.Content = $"Cena: {price:2f}zł";
        }

        public void DeleteFormCart()
        {
            if (_window.DgDoGridFive.SelectedIndex > 0)
            {
                _window.DgDoGridFive.Items.RemoveAt(_window.DgDoGridFive.SelectedIndex);
                UpadtePrice();
            }
        }

        public void SelectClientDoTransactionFirm()
        {
            if (_window.CmbDoGridTwoFirma.SelectedIndex > 0)
            {
                Klient k = ((Klient)((ComboBoxItem)_window.CmbDoGridTwoFirma.SelectedItem).Tag);
                _window.TxbDoGridTwoImie.Text = k.Imie;
                _window.TxbDoGridTwoKodPocztowy.Text = k.Ksiazka_adresow.Kod_pocztowy;
                _window.TxbDoGridTwoMiejscowosc.Text = k.Ksiazka_adresow.Miejscowosc;
                _window.CmbDoGridTwoFirma.SelectedIndex = 1;
                for (int i=0;i< _window.CmbDoGridTwoWojewodztwo.Items.Count;i++)
                {
                    if (_window.CmbDoGridTwoWojewodztwo.Items.GetItemAt(i).Equals(k.Ksiazka_adresow.Wojewodztwo))
                        {
                        _window.CmbDoGridTwoWojewodztwo.SelectedItem = i;
                            }
                }
                for (int i = 0; i < _window.CmbDoGridTwoNazwisko.Items.Count; i++)
                {
                    if (((ComboBoxItem)_window.CmbDoGridTwoNazwisko.Items.GetItemAt(i)).Name.Equals(k.Nazwisko))
                    {
                        _window.CmbDoGridTwoNazwisko.SelectedItem = i;
                    }
                }
            }
        }
        public void SelectClientDoTransactionSurname()
        {
            if (_window.CmbDoGridTwoNazwisko.SelectedIndex > 0)
            {
                Klient k = ((Klient)((ComboBoxItem)_window.CmbDoGridTwoNazwisko.SelectedItem).Tag);
                _window.TxbDoGridTwoImie.Text = k.Imie;
                _window.TxbDoGridTwoKodPocztowy.Text = k.Ksiazka_adresow.Kod_pocztowy;
                _window.TxbDoGridTwoMiejscowosc.Text = k.Ksiazka_adresow.Miejscowosc;
                _window.CmbDoGridTwoFirma.SelectedIndex = 1;
                for (int i = 0; i < _window.CmbDoGridTwoWojewodztwo.Items.Count; i++)
                {
                    if (_window.CmbDoGridTwoWojewodztwo.Items.GetItemAt(i).Equals(k.Ksiazka_adresow.Wojewodztwo))
                    {
                        _window.CmbDoGridTwoWojewodztwo.SelectedItem = i;
                    }
                }
                for (int i = 0; i < _window.CmbDoGridTwoFirma.Items.Count; i++)
                {
                    if (((ComboBoxItem)_window.CmbDoGridTwoWojewodztwo.Items.GetItemAt(i)).Name.Equals(k.Nazwa_firmy))
                    {
                        _window.CmbDoGridTwoFirma.SelectedItem = i;
                    }
                }
            }
        }

        public void RegisterTransaction()
        {
            if (_window.DgDoGridFive.Items.Count > 0)
            {
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

        private void TransactionOld(List<Artykul_w_Koszyku> cart)
        {
            throw new NotImplementedException();
        }

        private void TransactionNew(List<Artykul_w_Koszyku> cart)
        {
            throw new NotImplementedException();
        }

        //DONE




        //Lists
        private IEnumerable<Klient> clients;

        private static List<string> states = new List<string>()
        {"Dolonośląskie","Kujawsko-Pomorskie","Lubelskie","Lubuskie","Łódzkie","Małopolskie","Mazowieckie", "Opolskie","Podkarpackie","Podlaskie","Pomorskie","Śląskie","Świętokrzyskie","Warmińsko-Mazurskie","Wielkopolskie","Zachodniopomorskie"};
    }
}
