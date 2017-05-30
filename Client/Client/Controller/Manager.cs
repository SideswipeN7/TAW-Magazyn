using Client.Communication;
using Client.Model;
using Client.Windows;
using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Controller
{
    public sealed class Manager : IManager
    {
        private static Manager _instance;
        private Admin _window;
        private ICommunication _comm;
        private int ID { get; set; }
        private InProgress inProg;

        private Manager()
        {
            _comm = Communicator.GetInstance();
           // _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
            _comm.SetUrlAddress("http://localhost:52992");
        }
        public static Manager GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new Manager();
            }
            _instance._window = window;
            _instance.ID = window.ID;
            return _instance;
        }

        internal void GetAll()
        {
            GetCategoryData();
            GetMagazineState();
            GetItemData();
            GetClientData();
            GetClientTransactionData();
            GetEmployeeData();
        }

        internal void SelectaAll()
        {
            _window.RbStateWszystko.IsChecked = true;
            _window.RbOverviewGridOneWszystkie.IsChecked = true;
            _window.RbClientsWszyscy.IsChecked = true;
            _window.RbCategoryWszystko.IsChecked = true;
            _window.RbItemWszystko.IsChecked = true;
            _window.RdEmployeeWszyscy.IsChecked = true;
        }

        internal void LoadAll()
        {
            LoadCategoriesMagazineSate();
            LoadClients();
            LoadTransactionsDoProducts();
            LoadTransactionsDoStates();
            LoadTransactionsDoSupplier();
            LoadSudo();
        }

        private void LoadSudo()
        {
            _window.CmbEmployeeAdmin.Items.Clear();
            _window.CmbEmployeeAdmin.Items.Add(new ComboBoxItem() { Tag = 0, Content = "Nie" });
            _window.CmbEmployeeAdmin.Items.Add(new ComboBoxItem() { Tag = 1, Content = "Tak" });
        }


        //Magazine State
        public void LoadCategoriesMagazineSate()
        {
            IEnumerable<Kategoria> list = _comm.GetCategories();

            foreach (Kategoria r in list)
            {
                _window.CmbStateKategoria.Items.Add(new ComboBoxItem() { Content = r.Nazwa, Tag = r.idKategorii });
                _window.CmbItemKategoria.Items.Add(new ComboBoxItem() { Content = r.Nazwa, Tag = r });

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
            _window.LblState_.Visibility = Visibility.Hidden;

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
            _window.LblState_.Visibility = Visibility.Visible;
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
            IWork work = CategoryController.GetInstance(_window);
            work.GetData();
        }

        internal void ShowCategoriesData()
        {
            if (_window.DgCategoryLista.SelectedIndex >= 0)
            {
                try
                {
                    _window.TxbCategoryNazwa.Text = ((Kategoria)_window.DgCategoryLista.SelectedItem).Nazwa;
                    _window.CmbCategoryId.SelectedItem = ((Kategoria)_window.DgCategoryLista.SelectedItem).idKategorii;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}ERROR: {ex}");
                }
            }
        }


        internal void ShowItemsData()
        {
            if (_window.DgItemLista.SelectedIndex >= 0)
            {
                try
                {

                    for (int i = 0; i < _window.CmbItemKategoria.Items.Count; i++)
                    {
                        if (((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.Items.GetItemAt(i)).Tag).idKategorii == ((Artykul)_window.DgItemLista.SelectedItem).Kategorie.idKategorii)
                        {
                            _window.CmbItemKategoria.SelectedIndex = i;
                        }
                    }
                    //_window.CmbItemKategoria.SelectedItem = new ComboBoxItem() { Content = ((Artykul)_window.DgItemLista.SelectedItem).Kategorie.Nazwa, Tag = ((Artykul)_window.DgItemLista.SelectedItem).Kategorie };
                    _window.TxbItemCenaMin.Text = ((Artykul)_window.DgItemLista.SelectedItem).Cena + "";
                    _window.TxbItemIlosc.Text = ((Artykul)_window.DgItemLista.SelectedItem).Ilosc + "";
                    _window.TxbItemINazwa.Text = ((Artykul)_window.DgItemLista.SelectedItem).Nazwa;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}ERROR: {ex}");
                }
            }
        }


        internal void ShowClientsData()
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


        public void ShowCategoryData(IEnumerable<Kategoria> categories)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                _window.CmbCategoryId.Items.Clear();
                foreach (Kategoria r in categories)
                {
                    _window.DgCategoryLista.Items.Add(r);
                    _window.CmbCategoryId.Items.Add(r.idKategorii);
                }
            }));
        }


        internal void CmbCategoryIdChange()
        {
            //foreach (Kategoria k in _window.DgCategoryLista.Items)
            //{
            //    if (k.idKategorii == (int)_window.CmbCategoryId.SelectedItem)
            //    {
            //        _window.DgCategoryLista.SelectedItem = k;
            //    }
            //}
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
            IWork work = CategoryController.GetInstance(_window);
            work.AddData();
        }

        public void ChangeCategoryData()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.ChangeData();
        }

        public void CategoriesDelete()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.DeleteData();
        }

        public void CategoriesSearch()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.SearchData();
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
                _window.CmbItemKategoria.SelectedIndex >= 0)
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
                 _window.CmbItemKategoria.SelectedIndex >= 0)
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
                // _window.CmbCategoryId.Items.Clear();
                foreach (Artykul r in categories)
                {
                    _window.DgItemLista.Items.Add(r);

                }


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
            IWork work = CategoryController.GetInstance(_window);
            work.GetData();
        }



        public void SetClientData()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.AddData();
        }

        public void ClientDelete()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.DeleteData();
        }

        public void SearchClients()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.SearchData();
        }

        //Transactions

        //DO NEW
        public void LoadTransactionsDoStates()
        {
            _window.CmbDoGridOneWojewodztwo.Items.Clear();
            _window.CmbDoGridTwoWojewodztwo.Items.Clear();
            _window.CmbClientsWojewodztwo.Items.Clear();
            _window.CmbEmployeeWojewodztwo.Items.Clear();
            foreach (string r in states)
            {
                _window.CmbDoGridOneWojewodztwo.Items.Add(r);
                _window.CmbDoGridTwoWojewodztwo.Items.Add(r);
                _window.CmbClientsWojewodztwo.Items.Add(r);
                _window.CmbEmployeeWojewodztwo.Items.Add(r);
            }
        }

        public void LoadTransactionsDoSupplier()
        {
            try
            {
                Task<IEnumerable<Dostawca>>.Factory.StartNew(() =>
         {
             return _comm.GetSuppliers();
         }).ContinueWith(x =>
         {
             Task.Factory.StartNew(() =>
             {
                 Load1(x.Result);
                 Load2(x.Result);
             });
         });

            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"\nERROR {ex}"); }

        }
        void Load1(IEnumerable<Dostawca> list)
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(()=>
                {
                    {
                        _window.CmbDoGridOneDostawca.Items.Clear();
                        foreach (Dostawca r in list)
                        {
                            _window.CmbDoGridOneDostawca.Items.Add(new ComboBoxItem() { Content = r.Nazwa, Tag = r });
                        }
                    }
                }));
                
            }
            
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"\nERROR {ex}"); }
}
        void Load2(IEnumerable<Dostawca> list)
        {
            try
            {

                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                
                    _window.CmbDoGridTwoDostawca.Items.Clear();
                foreach (Dostawca r in list)
                {
                    _window.CmbDoGridTwoDostawca.Items.Add(new ComboBoxItem() { Content = r.Nazwa.ToString(), Tag = r });
                }
                }));
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"\nERROR {ex}"); }
        }
        public void LoadTransactionsDoProducts()
        {
            _window.CmbDoGridThreeNazwa.Items.Clear();
            IEnumerable<Artykul> list = _comm.GetItems();
            try
            {

                foreach (Artykul r in list)
                {

                    _window.CmbDoGridThreeNazwa.Items.Add(new ComboBoxItem() { Content = r.Nazwa.ToString(), Tag = r });
                }
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"\nERROR {ex}"); }
        }
        public void LoadClients()
        {
            try
            {
                LoadClientsToCmb(_comm.GetClients());
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"\nERROR {ex}"); }
        }
        private void LoadClientsToCmb(IEnumerable<Klient> clients)
        {
            try
            {
                _window.CmbDoGridTwoNazwisko.Items.Clear();
                _window.CmbDoGridTwoFirma.Items.Clear();
                _window.CmbDoGridTwoFirma.Items.Add(new ComboBoxItem() { Content = "", Tag = null });
                foreach (Klient r in clients)
                {
                    _window.CmbDoGridTwoNazwisko.Items.Add(new ComboBoxItem() { Content = r.Nazwisko, Tag = r });
                    if (r.Nazwa_firmy.Length > 0)
                    {
                        _window.CmbDoGridTwoFirma.Items.Add(new ComboBoxItem() { Content = r.Nazwa_firmy, Tag = r });
                    }
                }
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"ERROR {ex}"); }
        }

        public void AddToCart()
        {
            if (_window.CmbDoGridThreeNazwa.SelectedIndex >= 0)
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
            _window.LblDoFourCena.Content = $"Cena: {price:F2} zł";
        }

        public void DeleteFormCart()
        {
            if (_window.DgDoGridFive.SelectedIndex >= 0)
            {
                _window.DgDoGridFive.Items.RemoveAt(_window.DgDoGridFive.SelectedIndex);
                UpadtePrice();
            }
        }

        public void SelectClientDoTransactionFirm()
        {
            if (_window.CmbDoGridTwoFirma.SelectedIndex >= 0)
            {
                Klient k = ((Klient)((ComboBoxItem)_window.CmbDoGridTwoFirma.SelectedItem).Tag);
                _window.TxbDoGridTwoImie.Text = k.Imie;
                _window.TxbDoGridTwoKodPocztowy.Text = k.Ksiazka_adresow.Kod_pocztowy;
                _window.TxbDoGridTwoMiejscowosc.Text = k.Ksiazka_adresow.Miejscowosc;
                //_window.CmbDoGridTwoFirma.SelectedIndex = 1;
                for (int i = 0; i < _window.CmbDoGridTwoWojewodztwo.Items.Count; i++)
                {
                    if (_window.CmbDoGridTwoWojewodztwo.Items.GetItemAt(i).Equals(k.Ksiazka_adresow.Wojewodztwo))
                    {
                        _window.CmbDoGridTwoWojewodztwo.SelectedIndex = i;
                    }
                }
                for (int i = 0; i < _window.CmbDoGridTwoNazwisko.Items.Count; i++)
                {
                    if (((Klient)((ComboBoxItem)_window.CmbDoGridTwoNazwisko.Items.GetItemAt(i)).Tag).idKlienta.Equals(k.idKlienta))
                    {
                        if (_window.CmbDoGridTwoNazwisko.SelectedIndex != i)
                            _window.CmbDoGridTwoNazwisko.SelectedIndex = i;
                    }
                }
            }
        }
        public void SelectClientDoTransactionSurname()
        {
            if (_window.CmbDoGridTwoNazwisko.SelectedIndex >= 0)
            {
                Klient k = ((Klient)((ComboBoxItem)_window.CmbDoGridTwoNazwisko.SelectedItem).Tag);
                _window.TxbDoGridTwoImie.Text = k.Imie;
                _window.TxbDoGridTwoKodPocztowy.Text = k.Ksiazka_adresow.Kod_pocztowy;
                _window.TxbDoGridTwoMiejscowosc.Text = k.Ksiazka_adresow.Miejscowosc;
                bool chage = true;
                for (int i = 0; i < _window.CmbDoGridTwoWojewodztwo.Items.Count; i++)
                {

                    if (_window.CmbDoGridTwoWojewodztwo.Items.GetItemAt(i).Equals(k.Ksiazka_adresow.Wojewodztwo))
                    {
                        _window.CmbDoGridTwoWojewodztwo.SelectedIndex = i;
                    }
                }

                for (int i = 0; i < _window.CmbDoGridTwoFirma.Items.Count; i++)
                {
                    if ((((ComboBoxItem)(_window.CmbDoGridTwoFirma.Items.GetItemAt(i))).Tag) != null && ((Klient)((ComboBoxItem)(_window.CmbDoGridTwoFirma.Items.GetItemAt(i))).Tag).idKlienta.Equals(k.idKlienta))
                    {
                        if (_window.CmbDoGridTwoFirma.SelectedIndex != i)
                        {
                            _window.CmbDoGridTwoFirma.SelectedIndex = i;

                        }
                        chage = false;
                    }
                }
                if (chage) _window.CmbDoGridTwoFirma.SelectedIndex = 0;
            }
        }

        public void RegisterTransaction()
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

        private void TransactionNew(List<Artykul_w_Koszyku> cart)
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

        private void TransactionRegister(Transakcja t)
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
                         GetAll();
                         MessageBox.Show("Wykonano pomyślnie!", "Transakcja", MessageBoxButton.OK);

                     }));
                     return 0;
                 });


             });
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
                                //System.Diagnostics.Debug.WriteLine($"{Environment.NewLine} ERRRROR {((Dostawca)(((ComboBoxItem)_window.CmbDoGridTwoDostawca.SelectedItem).Tag)).Nazwa}");
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
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine} ERRRROR {ex}");
            }
        }


        internal void TransactionNewSelcted()
        {
            _window.GridDoTwo.IsEnabled = false;
            _window.GridTransactionsDoOne.IsEnabled = true;
            //_window.GridDoTwo.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            // _window.GridTransactionsDoOne.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        internal void TransactionOldSelcted()
        {
            _window.GridDoTwo.IsEnabled = true;
            _window.GridTransactionsDoOne.IsEnabled = false;
            // _window.GridDoTwo.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));            
            // _window.GridTransactionsDoOne.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
        }

        //DONE
        public void GetClientTransactionData()
        {
            Task<IEnumerable<Transakcja>>.Factory.StartNew(() =>
            {
                return _comm.GetTransactions();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowClientTransactionData(x.Result);
                });
            });
        }

        public void ShowClientTransactionData(IEnumerable<Transakcja> transactions)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Transakcja r in transactions)
                    _window.DgOverviewGridOne.Items.Add(r);

            }));
        }

        public void ShowClientData()
        {
            if (_window.DgOverviewGridOne.SelectedIndex >= 0)
            {
                try
                {
                    Transakcja t = (Transakcja)_window.DgOverviewGridOne.SelectedItem;
                    _window.LblOverviewGridTwoNazwisko.Content = $"Nazwisko: {t.Klienci.Nazwisko}";
                    _window.LblOverviewGridTwoNazwaFirmy.Content = $"Nazwa Firmy: {t.Klienci.Nazwa_firmy}";
                    _window.LblOverviewGridTwoDostawca.Content = $"Dostawca: {t.Dostawcy.Nazwa}";
                    _window.LblOverviewGridTwoData.Content = $"Data: {t.Data}";
                    _window.LblOverviewGridTwoMiejscowosc.Content = $"Miejscowość: {t.Klienci.Ksiazka_adresow.Miejscowosc}";
                    _window.LblOverviewGridTwoKodPocztowy.Content = $"Kod Pocztowy: {t.Klienci.Ksiazka_adresow.Kod_pocztowy}";
                    _window.LblOverviewGridTwoWojewodztwo.Content = $"Województwo: {t.Klienci.Ksiazka_adresow.Wojewodztwo}";
                    //TODO                _window.LblOverviewGridTwoTransakcja.Content = $"Transakcja: {t.Nazwa}";
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
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}ERROR: {ex}");
                }
            }
        }

        public void TransactionsSearch()
        {
            List<Transakcja> list = new List<Transakcja>();
            for (int i = 0; i < _window.DgOverviewGridOne.Items.Count; i++)
                list.Add((Transakcja)_window.DgOverviewGridOne.Items.GetItemAt(i));
            _window.DgOverviewGridOne.Items.Clear();
            //1
            if (_window.ChbOverviewGridTwoData.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Data != _window.DpOverviewGridTwoDataPicker.SelectedDate)
                        list.Remove(list[i]);
                }
            }
            //2
            if (_window.ChbOverviewGridTwoFirma.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!list[i].Klienci.Nazwa_firmy.ToLower().Equals(_window.TxbOverviewGridTwoFirma.Text.ToLower()))
                        list.Remove(list[i]);
                }
            }
            //3
            if (_window.ChbOverviewGridTwoNazwisko.IsChecked == true)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    if (!list[i].Klienci.Nazwisko.ToLower().Equals(_window.TxbOverviewGridTwoNazwisko.Text.ToLower()))
                        list.Remove(list[i]);
                }

            }
            ShowClientTransactionData(list);

        }
        internal void ShowTransactionsAll()
        {
            
            _window.GridOverwviewSearch.Visibility = Visibility.Hidden;
        }

        internal void ShowTransactionsSearch()
        {
            _window.GridOverwviewSearch.Visibility = Visibility.Visible;
        }

        public void ShowFacture(Transakcja tran)
        {


            System.Collections.Generic.IEnumerable<Artykul_w_transakcji> art = tran.Artykuly_w_transakcji;

            MigraDoc.DocumentObjectModel.Document doc = new MigraDoc.DocumentObjectModel.Document();
            MigraDoc.DocumentObjectModel.Section sec = doc.AddSection();

            Paragraph paragraph = sec.Headers.Primary.AddParagraph();
            paragraph.AddText("Szczegółowe dane transakcji");
            paragraph.Format.Font.Size = 18;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            MigraDoc.DocumentObjectModel.Tables.Table table = new MigraDoc.DocumentObjectModel.Tables.Table();
            table.Borders.Width = 0;

            MigraDoc.DocumentObjectModel.Tables.Column column = table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(5));
            column.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;

            column = table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(7));
            column.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;

            MigraDoc.DocumentObjectModel.Tables.Row row = table.AddRow();
            MigraDoc.DocumentObjectModel.Tables.Cell cell = row.Cells[0];

            cell = row.Cells[0];
            cell.AddParagraph("Nr transakcji");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 12;
            cell = row.Cells[1];
            cell.AddParagraph(tran.idTransakcji.ToString());

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("Data");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 12;
            cell = row.Cells[1];
            cell.AddParagraph(tran.Data.ToString());

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("Imię");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 12;
            cell = row.Cells[1];
            cell.AddParagraph(tran.Klienci.Imie);

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("Nazwisko");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 12;
            cell = row.Cells[1];
            cell.AddParagraph(tran.Klienci.Nazwisko);

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("Nazwa firmy");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 12;
            cell = row.Cells[1];
            cell.AddParagraph(tran.Klienci.Nazwa_firmy);

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("Nazwa dostawcy");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 12;
            cell = row.Cells[1];
            cell.AddParagraph(tran.Dostawcy.Nazwa);

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("Przedmioty");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 12;
            cell = row.Cells[1];

            double suma = 0;
            foreach (Artykul_w_transakcji awt in art)
            {
                cell = row.Cells[1];
                cell.AddParagraph(awt.Artykuly.Nazwa + ", " + awt.Artykuly.Cena + "zl" + "\n");
                suma = suma + (double)awt.Artykuly.Cena;
            }

            doc.LastSection.Add(table);

            sec.AddParagraph();
            sec.AddParagraph();
            doc.AddSection();
            sec.AddParagraph("Suma: " + suma + "zł");
            sec.AddParagraph();

            MigraDoc.Rendering.PdfDocumentRenderer docRend = new MigraDoc.Rendering.PdfDocumentRenderer(false);
            docRend.Document = doc;
            docRend.RenderDocument();

            string name = "TransInfo.pdf";

            docRend.PdfDocument.Save(name);
            Process.Start(name);
        }

        //EMPLOYEE
        internal void ShowEmployeeDelete()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeUsun.Visibility = Visibility.Visible;
        }

        internal void ShowEmployeeModify()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Visible;
            _window.BtnEmployeeUsun.Visibility = Visibility.Hidden;
        }

        internal void ShowEmployeeAdd()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Visible;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeUsun.Visibility = Visibility.Hidden;
        }

        internal void ShowEmployeeAll()
        {
            _window.BtnEmployeeDodaj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnEmployeeUsun.Visibility = Visibility.Hidden;
        }

        public void SetEmployeeData()
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
                        GetEmployeeData();
                    }));
            }

        }

        public void ChangeEmployeeData()
        {
            int wiek;
            if (_window.DgEmployeesList.SelectedIndex >= 0
                && _window.TxbEmployeeHaslo.Text.Length > 5
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
                    Kod_pocztowy = _window.TxbEmployeeKodPocztowy.Text,
                    Miejscowosc = _window.TxbEmployeeMiejscowosc.Text,
                    Wojewodztwo = _window.CmbEmployeeWojewodztwo.SelectedItem + ""
                };

                Task.Factory.StartNew(() =>
                {
                    _comm.ModifyEmployee(new PracownikAdress() { Pracownik = pracownik, Adres = adres });
                }).ContinueWith(x=>
                    Task.Factory.StartNew(() =>
                    {
                        GetEmployeeData();
                    }));
                
            }
        }

        public void GetEmployeeData()
        {
            Task<IEnumerable<Pracownik>>.Factory.StartNew(() =>
            {
                return _comm.GetEmpoyees();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowEmployeeData(x.Result);
                });
            });
        }

        internal void ShowEmployee()
        {
            if (_window.DgEmployeesList.SelectedIndex >= 0)
            {
                try
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
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}ERROR: {ex}");
                }
            }
        }

        public void ShowEmployeeData(IEnumerable<Pracownik> empolyees)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                _window.DgEmployeesList.Items.Clear();
                foreach (Pracownik p in empolyees)
                {
                    _window.DgEmployeesList.Items.Add(p);
                }
            }));

        }

        internal void DeleteEmployee()
        {
            if (_window.DgEmployeesList.SelectedIndex >= 0)
            {
                _comm.DeleteEmployee(((Pracownik)_window.DgEmployeesList.SelectedItem).idPracownika);
                GetEmployeeData();
            }
        }

        internal void ShowPassword()
        {
            try
            {
                switch (_window.TxbEmployeeHaslo.Text)
                {
                    case "*********":
                        _window.TxbEmployeeHaslo.Text = ((Pracownik)_window.DgEmployeesList.SelectedItem).Haslo;
                        _window.BtnEmployeeHaslo.Content = "Ukryj Hasło";
                        break;
                    default:
                        _window.TxbEmployeeHaslo.Text = "*********";
                        _window.BtnEmployeeHaslo.Content = "Pokaż Hasło";
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}ERROR: {ex}");
            }

        }



        //Lists

        private static List<string> states = new List<string>()
        {"Dolonośląskie","Kujawsko-Pomorskie","Lubelskie","Lubuskie","Łódzkie","Małopolskie","Mazowieckie", "Opolskie","Podkarpackie","Podlaskie","Pomorskie","Śląskie","Świętokrzyskie","Warmińsko-Mazurskie","Wielkopolskie","Zachodniopomorskie"};
    }
}
