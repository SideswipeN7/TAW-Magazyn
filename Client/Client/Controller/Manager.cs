using Client.Communication;
using Client.Facture;
using Client.Model;
using Client.Windows;
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
            IWork work = MagazineController.GetInstance(_window);
            work.SearchData();
        }

        public void GetMagazineState()
        {
            IWork work = MagazineController.GetInstance(_window);
            work.GetData();
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

        //Categories
        public void GetCategoryData()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.GetData();
        }

        internal void ShowItemsData()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.ShowSelectedData();
        }

        internal void ShowClientsData()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.ShowSelectedData();
        }

        internal void DeleteItems()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.DeleteData();
        }

        //TOMODIFY
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

        internal void ItemsShowDelete()
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
            _window.BtnItemUsun.Visibility = Visibility.Visible;
        }

        //Categories
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

        //Items
        public void ChangeItemData()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.ChangeData();
        }

        public void SetItemData()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.AddData();
        }

        public void SearchItems()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.SearchData();
        }

        public void GetItemData()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.GetData();
        }

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
            _window.BtnItemUsun.Visibility = Visibility.Hidden;
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
            _window.BtnItemUsun.Visibility = Visibility.Hidden;
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
            _window.BtnItemUsun.Visibility = Visibility.Hidden;
        }

        public void ShowItemsAll()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.GetData();

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
            _window.BtnItemUsun.Visibility = Visibility.Hidden;
        }

        //Client
        public void ChangeClientData()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.ChangeData();
        }

        public void GetClientData()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.GetData();
        }

        public void SetClientData()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.AddData();
        }

        public void ClientDelete()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.DeleteData();
        }

        public void SearchClients()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.SearchData();
        }

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

        //Transactions

        //DO NEW
        public void LoadTransactionsDoStates()
        {
            _window.CmbDoGridOneWojewodztwo.Items.Clear();
            _window.CmbDoGridTwoWojewodztwo.Items.Clear();
            _window.CmbClientsWojewodztwo.Items.Clear();
            _window.CmbEmployeeWojewodztwo.Items.Clear();
            _window.CmbClientsWojewodztwoSearch.Items.Clear();
            foreach (string r in states)
            {
                _window.CmbDoGridOneWojewodztwo.Items.Add(r);
                _window.CmbDoGridTwoWojewodztwo.Items.Add(r);
                _window.CmbClientsWojewodztwo.Items.Add(r);
                _window.CmbEmployeeWojewodztwo.Items.Add(r);
                _window.CmbClientsWojewodztwoSearch.Items.Add(r);
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
                        Load(x.Result);
                    });
                });
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"\nERROR {ex}"); }
        }

        private void Load(IEnumerable<Dostawca> list)
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _window.CmbDoGridTwoDostawca.Items.Clear();
                    _window.CmbDoGridOneDostawca.Items.Clear();
                    foreach (Dostawca r in list)
                    {
                        _window.CmbDoGridTwoDostawca.Items.Add(new ComboBoxItem() { Content = r.Nazwa.ToString(), Tag = r });
                        _window.CmbDoGridOneDostawca.Items.Add(new ComboBoxItem() { Content = r.Nazwa, Tag = r });
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
            IWork work = TransactionController.GetInstance(ID, _window);
            work.AddData();
        }

        internal void TransactionNewSelcted()
        {
            _window.GridDoTwo.IsEnabled = false;
            _window.GridTransactionsDoOne.IsEnabled = true;
        }

        internal void TransactionOldSelcted()
        {
            _window.GridDoTwo.IsEnabled = true;
            _window.GridTransactionsDoOne.IsEnabled = false;
        }

        //DONE
        public void GetClientTransactionData()
        {
            IWork work = TransactionController.GetInstance(ID, _window);
            work.GetData();
        }

        public void ShowClientData()
        {
            IWork work = TransactionController.GetInstance(ID, _window);
            work.ShowSelectedData();
        }

        public void TransactionsSearch()
        {
            IWork work = TransactionController.GetInstance(ID, _window);
            work.SearchData();
        }

        internal void ShowTransactionsAll()
        {
            _window.GridOverwviewSearch.Visibility = Visibility.Hidden;
        }

        internal void ShowTransactionsSearch()
        {
            _window.GridOverwviewSearch.Visibility = Visibility.Visible;
        }

        //FAKTURA
        public void ShowFacture(Transakcja tran)
        {
            IFacture faktura = Facture.Facture.GetInstace();
            faktura.NewFacture(tran);
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
            IWork work = EmployeeController.GetInstance(_window);
            work.AddData();
        }

        public void ChangeEmployeeData()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.ChangeData();
        }

        public void GetEmployeeData()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.GetData();
        }

        internal void ShowEmployee()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.ShowSelectedData();
        }

        internal void DeleteEmployee()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.DeleteData();
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