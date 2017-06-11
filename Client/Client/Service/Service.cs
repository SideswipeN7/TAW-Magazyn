using Client.Communication;
using Client.Controller;
using Client.Controller.View;
using Client.Facture;
using Client.Interfaces;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Diagnostics.Debug;

namespace Client.Adapter
{
    public class Service : IAdapter
    {
        private Admin _window;
        private int ID { get; set; }
        private static Service _instance;
        public ICommSupplier _commSupp { get; set; }
        public ICommItems _commItem { get; set; }
        public ICommClient _commClie { get; set; }
        public ICommCategory _commCat { get; set; }

        protected static List<string> states = new List<string>()
        {"Dolonośląskie","Kujawsko-Pomorskie","Lubelskie","Lubuskie","Łódzkie","Małopolskie","Mazowieckie", "Opolskie","Podkarpackie","Podlaskie","Pomorskie","Śląskie","Świętokrzyskie","Warmińsko-Mazurskie","Wielkopolskie","Zachodniopomorskie"};

        public static Service GetInstance(int ID, Admin window)
        {
            if (_instance == null)
            {
                _instance = new Service();
            }
            _instance._commSupp = CommSupplier.GetInstance();
            _instance._commItem = CommItems.GetInstance();
            _instance._commClie = CommClient.GetInstance();
            _instance._commCat = CommCategory.GetInstance();
            _instance.ID = ID;
            _instance._window = window;
            return _instance;
        }

        //Categories
        public void ShowAllCategories()
        {
            IViewController view = CategoryView.GetInstance(_window);
            view.ShowAll();
            IWork work = CategoryController.GetInstance(_window);
            work.GetData();
        }

        public void ShowSearchCategories()
        {
            IViewController view = CategoryView.GetInstance(_window);
            view.ShowSearch();
        }

        public void ShowAddCategories()
        {
            IViewController view = CategoryView.GetInstance(_window);
            view.ShowAdd();
        }

        public void ShowDeleteCategories()
        {
            IViewController view = CategoryView.GetInstance(_window);
            view.ShowDelete();
        }

        public void ShowModifyCategories()
        {
            IViewController view = CategoryView.GetInstance(_window);
            view.ShowModify();
        }

        public void AddCategories()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.AddData();
        }

        public void DeleteCategories()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.DeleteData();
        }

        public void ModifyCategories()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.ChangeData();
        }

        public void SearchCategories()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.SearchData();
        }

        public void SelectedCategories()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.ShowSelectedData();
        }

        //Items
        public void ShowAllItems()
        {
            IViewController view = ItemView.GetInstance(_window);
            view.ShowAll();
            IWork work = ItemsController.GetInstance(_window);
            work.GetData();
        }

        public void ShowSearchItems()
        {
            IViewController view = ItemView.GetInstance(_window);
            view.ShowSearch();
        }

        public void ShowAddItems()
        {
            IViewController view = ItemView.GetInstance(_window);
            view.ShowAdd();
        }

        public void ShowDeleteItems()
        {
            IViewController view = ItemView.GetInstance(_window);
            view.ShowDelete();
        }

        public void ShowModifyItems()
        {
            IViewController view = ItemView.GetInstance(_window);
            view.ShowModify();
        }

        public void AddItems()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.AddData();
        }

        public void DeleteItems()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.DeleteData();
        }

        public void ModifyItems()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.ChangeData();
        }

        public void SearchItems()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.SearchData();
        }

        public void SelectedItems()
        {
            IWork work = ItemsController.GetInstance(_window);
            work.ShowSelectedData();
        }

        //Clients
        public void ShowAllClients()
        {
            IViewController view = ClientsView.GetInstance(_window);
            view.ShowAll();
            IWork work = ClientsController.GetInstance(_window);
            work.GetData();
        }

        public void ShowSearchClients()
        {
            IViewController view = ClientsView.GetInstance(_window);
            view.ShowSearch();
        }

        public void ShowAddClients()
        {
            IViewController view = ClientsView.GetInstance(_window);
            view.ShowAdd();
        }

        public void ShowDeleteClients()
        {
            IViewController view = ClientsView.GetInstance(_window);
            view.ShowDelete();
        }

        public void ShowModifyClients()
        {
            IViewController view = ClientsView.GetInstance(_window);
            view.ShowModify();
        }

        public void AddClients()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.AddData();
        }

        public void DeleteClients()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.DeleteData();
        }

        public void ModifyClients()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.ChangeData();
        }

        public void SearchClients()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.SearchData();
        }

        public void SelectedClients()
        {
            IWork work = ClientsController.GetInstance(_window);
            work.ShowSelectedData();
        }

        //Employee
        public void ShowAllEmployee()
        {
            IViewController view = EmployeeView.GetInstance(_window);
            view.ShowAll();
            IWork work = EmployeeController.GetInstance(_window);
            work.GetData();
        }

        public void ShowSearchEmployee()
        {
            IViewController view = EmployeeView.GetInstance(_window);
            view.ShowSearch();
        }

        public void ShowAddEmployee()
        {
            IViewController view = EmployeeView.GetInstance(_window);
            view.ShowAdd();
        }

        public void ShowDeleteEmployee()
        {
            IViewController view = EmployeeView.GetInstance(_window);
            view.ShowDelete();
        }

        public void ShowModifyEmployee()
        {
            IViewController view = EmployeeView.GetInstance(_window);
            view.ShowModify();
        }

        public void AddEmployee()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.AddData();
        }

        public void DeleteEmployee()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.DeleteData();
        }

        public void ModifyEmployee()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.ChangeData();
        }

        public void SearchEmployee()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.SearchData();
        }

        public void SelectedEmployee()
        {
            IWork work = EmployeeController.GetInstance(_window);
            work.ShowSelectedData();
        }

        //Transaction
        public void ShowAllTransaction()
        {
            IViewController view = TransactionView.GetInstance(_window);
            view.ShowAll();
            IWork work = TransactionController.GetInstance(ID, _window);
            work.GetData();
        }

        public void ShowSearchTransaction()
        {
            IViewController view = TransactionView.GetInstance(_window);
            view.ShowSearch();
        }

        public void ShowAddTransaction()
        {
            IViewController view = TransactionView.GetInstance(_window);
            view.ShowAdd();
        }

        public void ShowDeleteTransaction()
        {
            IViewController view = TransactionView.GetInstance(_window);
            view.ShowDelete();
        }

        public void ShowModifyTransaction()
        {
            IViewController view = TransactionView.GetInstance(_window);
            view.ShowModify();
        }

        public void AddTransaction()
        {
            IWork work = TransactionController.GetInstance(ID, _window);
            work.AddData();
        }

        public void DeleteTransaction()
        {
            IWork work = TransactionController.GetInstance(ID, _window);
            work.DeleteData();
        }

        public void ModifyTransaction()
        {
            IWork work = TransactionController.GetInstance(ID, _window);
            work.ChangeData();
        }

        public void SearchTransaction()
        {
            IWork work = TransactionController.GetInstance(ID, _window);
            work.SearchData();
        }

        public void SelectedTransaction()
        {
            IWork work = TransactionController.GetInstance(ID, _window);
            work.ShowSelectedData();
        }

        //Magzine
        public void ShowAllMagazine()
        {
            IViewController view = MagazineView.GetInstance(_window);
            view.ShowAll();
            IWork work = MagazineController.GetInstance(_window);
            work.GetData();
        }

        public void ShowSearchMagazine()
        {
            IViewController work = MagazineView.GetInstance(_window);
            work.ShowSearch();
        }

        public void AddMagazine()
        {
            IWork work = MagazineController.GetInstance(_window);
            work.AddData();
        }

        public void DeleteMagazine()
        {
            IWork work = MagazineController.GetInstance(_window);
            work.DeleteData();
        }

        public void ModifyMagazine()
        {
            IWork work = MagazineController.GetInstance(_window);
            work.ChangeData();
        }

        public void SearchMagazine()
        {
            IWork work = MagazineController.GetInstance(_window);
            work.SearchData();
        }

        public void SelectedMagazine()
        {
            IWork work = MagazineController.GetInstance(_window);
            work.ShowSelectedData();
        }

        //Other
        public void ShowPassword()
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
               WriteLine($"Error in{nameof(_instance)} {nameof(ShowPassword)}: {ex} ");
            }
        }

        public void TransactionNewSelcted()
        {
            _window.GridDoTwo.IsEnabled = false;
            _window.GridTransactionsDoOne.IsEnabled = true;
        }

        public void TransactionOldSelcted()
        {
            _window.GridDoTwo.IsEnabled = true;
            _window.GridTransactionsDoOne.IsEnabled = false;
        }

        public void GetFacture()
        {
            Transakcja tran = (Transakcja)_window.DgOverviewGridOne.SelectedItem;
            if (_window.DgOverviewGridOne.SelectedItem != null)
            {
                IFacture faktura = Facture.Facture.GetInstace();
                faktura.NewFacture(tran);
            }
            else
            {
                MessageBox.Show("Najpierw wybierz transakcję.");
            }
        }

        public void SelectClientDoTransactionSurname()
        {
            try
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
            catch (Exception ex)
            {
                WriteLine($"Error in {nameof(_instance)} {nameof(SelectClientDoTransactionSurname)}: {ex} ");
            }
        }

        public void SelectClientDoTransactionFirm()
        {
            try
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
            catch (Exception ex)
            {
                WriteLine($"Error in {nameof(_instance)} {nameof(SelectClientDoTransactionFirm)}: {ex} ");
            }
        }

        public void AddToCart()
        {
            try
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
            catch (Exception ex)
            {
               WriteLine($"Error in {nameof(_instance)} {nameof(AddToCart)}: {ex} ");
            }
        }

        public void DeleteFormCart()
        {
            try
            {
                if (_window.DgDoGridFive.SelectedIndex >= 0)
                {
                    _window.DgDoGridFive.Items.RemoveAt(_window.DgDoGridFive.SelectedIndex);
                    UpadtePrice();
                }
            }
            catch (Exception ex)
            {
               WriteLine($"Error in {nameof(_instance)} {nameof(DeleteFormCart)}: {ex} " );
            }
        }

        private void UpadtePrice()
        {
            try
            {
                decimal price = 0m;
                foreach (Artykul_w_Koszyku r in _window.DgDoGridFive.Items)
                {
                    price += r.CenaCalosciowa;
                }
                _window.LblDoFourCena.Content = $"Cena: {price:F2} zł";
            }
            catch (Exception ex)
            {
                WriteLine($"Error in {nameof(_instance)} {nameof(UpadtePrice)}: {ex} ");
            }
        }

        public void CmbCategoryIdChange()
        {
            try
            {
                foreach (Kategoria k in _window.DgCategoryLista.Items)
                {
                    if (k.idKategorii == (int)_window.CmbCategoryId.SelectedItem)
                    {
                        _window.DgCategoryLista.SelectedItem = k;
                    }
                }
            }
            catch (Exception ex)
            {
               WriteLine($"Error in {nameof(_instance)} {nameof(CmbCategoryIdChange)}: {ex} ");
            }
        }

        public void GetAll()
        {
            IWork work = CategoryController.GetInstance(_window);
            work.GetData();
            work = ClientsController.GetInstance(_window);
            work.GetData();
            work = EmployeeController.GetInstance(_window);
            work.GetData();
            work = ItemsController.GetInstance(_window);
            work.GetData();
            work = MagazineController.GetInstance(_window);
            work.GetData();
            work = TransactionController.GetInstance(ID, _window);
            work.GetData();
        }

        public void LoadAll()
        {
            //States
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
            //SUDO
            {
                _window.CmbEmployeeAdmin.Items.Clear();
                _window.CmbEmployeeAdmin.Items.Add(new ComboBoxItem() { Tag = 0, Content = "Nie" });
                _window.CmbEmployeeAdmin.Items.Add(new ComboBoxItem() { Tag = 1, Content = "Tak" });
            }
            //Suppliers
            {
                try
                {
                    Task<IEnumerable<Dostawca>>.Factory.StartNew(() =>
                    {
                        return _commSupp.GetSuppliers();
                    }).ContinueWith(x =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            Load(x.Result);
                        });
                    });
                }
                catch (Exception ex)
                {
                    WriteLine($"Error in {nameof(_instance)} {nameof(LoadAll)}: {ex} ");
                  }
            }
            //Products for transactions
            LoadTransactionsDoProducts();
            //LoadClients to ComboBox
            LoadClients();
            //LoadCategories
            LoadCategories();
        }

        public void SelectaAll()
        {
            try
            {
                _window.RbStateWszystko.IsChecked = true;
                _window.RbClientsWszyscy.IsChecked = true;
                _window.RbItemWszystko.IsChecked = true;
                _window.RdEmployeeWszyscy.IsChecked = true;
                _window.RbCategoryWszystko.IsChecked = true;
                _window.RbOverviewGridOneWszystkie.IsChecked = true;
            }
            catch (Exception ex)
            { WriteLine($"Error in {nameof(_instance)} {nameof(SelectaAll)}: {ex} "); }
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
            { WriteLine($"Error in  {nameof(_instance)} {nameof(Load)}: {ex} "); }
        }

        private void LoadTransactionsDoProducts()
        {
            _window.CmbDoGridThreeNazwa.Items.Clear();
            IEnumerable<Artykul> list = _commItem.GetItems();
            try
            {
                foreach (Artykul r in list)
                {
                    _window.CmbDoGridThreeNazwa.Items.Add(new ComboBoxItem() { Content = r.Nazwa.ToString(), Tag = r });
                }
            }
            catch (Exception ex)
            { WriteLine($"Error  {nameof(_instance)} {nameof(LoadTransactionsDoProducts)}: {ex} " ); }
        }

        public void LoadClients()
        {
            try
            {
                LoadClientsToCmb(_commClie.GetClients());
            }
            catch (Exception ex)
            { WriteLine($"Error in  {nameof(_instance)} {nameof(LoadClients)}: {ex} " ); }
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
            { System.Diagnostics.Debug.WriteLine($"Error in  {nameof(_instance)} {nameof(LoadClientsToCmb)}: {ex} " ); }
        }

        public void LoadCategories()
        {
            try
            {
                IEnumerable<Kategoria> list = _commCat.GetCategories();

                foreach (Kategoria r in list)
                {
                    _window.CmbStateKategoria.Items.Add(new ComboBoxItem() { Content = r.Nazwa, Tag = r });
                    _window.CmbItemKategoria.Items.Add(new ComboBoxItem() { Content = r.Nazwa, Tag = r });
                }
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine($"Error in  {nameof(_instance)} {nameof(LoadCategories)}: {ex} "); }
        }
    }
}