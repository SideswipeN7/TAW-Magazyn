using Client.Controller;
using Client.Model;
using System.Diagnostics;
using System.Windows;

namespace Client.Windows

{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private static Admin _instance;

        private Manager _controller;
        private ItemsController _itemController = ItemsController.GetInstance(_instance);
        public int ID { get; set; }

        public static Admin GetInstance(int id, bool sudo)
        {
            if (_instance == null)
            {
                _instance = new Admin();
            }
            _instance.ID = id;
            if (sudo)
            {
                _instance.TabEmployees.Visibility = Visibility.Visible;
                _instance.Title = "Tryb Adminitratora";
            }
            else
            {
                _instance.TabEmployees.Visibility = Visibility.Hidden;
                _instance.Title = "Tryb Użytkownika";
            }
            return _instance;
        }

        public Admin()
        {
            InitializeComponent();
            _controller = Manager.GetInstance(this);
            //Show Data
            _controller.GetAll();
            //LoadData
            _controller.LoadAll();
            _controller.SelectaAll();
        }

        //Magazine State
        private void RbStateSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowMagazineStateSearch();
        }

        private void RbStateWszystko_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowMagazineStateAll();
        }

        private void BtnSateSzukaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.SearchCategoriesSate();
        }

        //Categories
        private void RbCategoryWszystko_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowCategoriesSate();
        }

        private void RbCategoryDodaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowCategoriesAdd();
        }

        private void RbCategoryModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowCategoriesModify();
        }

        private void RbCategoryUsun_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowCategoriesDelete();
        }

        private void RbCategorySzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowCategoriesSearch();
        }

        private void BtnCategoryDodaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.SetCategoryData();
        }

        private void BtnCategoryModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _controller.ChangeCategoryData();
        }

        private void BtnCategoryUsun_Click(object sender, RoutedEventArgs e)
        {
            _controller.CategoriesDelete();
        }

        private void BtnCategorySzukaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.CategoriesSearch();
        }

        //Items
        private void RbItemWszystko_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowItemsAll();
        }

        private void RbItemSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowItemsSearch();
        }

        private void RbItemModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowItemsModify();
        }

        private void RbItemDodaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowItemsAdd();
        }

        private void BtnItemModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _controller.ChangeItemData();
        }

        private void BtnItemDodaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.SetItemData();
        }

        private void BtnItemSzukaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.SearchItems();
        }

        //Clients
        private void RbClientsWszyscy_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowClitentsAll();
        }

        private void RbClientsSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowClitentsSearch();
        }

        private void RbClientsDodaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowClitentsAdd();
        }

        private void RbClientsModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowClitentsModify();
        }

        private void RbClientsUsun_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowClitentsDelete();
        }

        private void BtnClientsSzukaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.SearchClients();
        }

        private void BtnClientsModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _controller.ChangeClientData();
        }

        private void BtnClientsUsun_Click(object sender, RoutedEventArgs e)
        {
            _controller.ClientDelete();
        }

        private void BtnClientsDodaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.SetClientData();
        }

        private void RbDoGridOneNowyKlient_Checked(object sender, RoutedEventArgs e)
        {
            _controller.TransactionNewSelcted();
        }

        private void RbDoGridOneStalyKlient_Checked(object sender, RoutedEventArgs e)
        {
            _controller.TransactionOldSelcted();
        }

        private void CmbDoGridTwoNazwisko_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _controller.SelectClientDoTransactionSurname();
        }

        private void CmbDoGridTwoFirma_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _controller.SelectClientDoTransactionFirm();
        }

        private void BtnDoGridThreeDodajProdukty_Click(object sender, RoutedEventArgs e)
        {
            _controller.AddToCart();
        }

        private void BtnDoGridFiveUsun_Click(object sender, RoutedEventArgs e)
        {
            _controller.DeleteFormCart();
        }

        private void DgOverviewGridOne_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _controller.ShowClientData();
        }

        private void RbOverviewGridOneWszystkie_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowTransactionsAll();
        }

        private void RbOverviewGridOneSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowTransactionsSearch();
        }

        private void BtnOverviewGridTwoSzukaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.TransactionsSearch();
        }

        private void DgCategoryLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _controller.ShowCategoriesData();
        }

        private void DgItemLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _controller.ShowItemsData();
        }

        private void DgClientsLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _controller.ShowClientsData();
        }

        private void DgEmployeesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _controller.ShowEmployee();
        }

        private void BtnEmployeeDodaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.SetEmployeeData();
        }

        private void BtnEmployeeModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _controller.SetEmployeeData();
        }

        private void BtnEmployeeUsun_Click(object sender, RoutedEventArgs e)
        {
            _controller.DeleteEmployee();
        }

        private void RdEmployeeWszyscy_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowEmployeeAll();
        }

        private void RdEmployeeDodaj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowEmployeeAdd();
        }

        private void RdEmployeeModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowEmployeeModify();
        }

        private void RdEmployeeUsun_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ShowEmployeeDelete();
        }

        private void btnTabOverviewFaktura_Click(object sender, RoutedEventArgs e)
        {
            Transakcja tran = (Transakcja)DgOverviewGridOne.SelectedItem;
            if (DgOverviewGridOne.SelectedItem != null)
            {
                _controller.ShowFacture(tran);
            }
            else
            {
                MessageBox.Show("Najpierw wybierz transakcję.");
            }
        }

        private void MnAbout_Click(object sender, RoutedEventArgs e)
        {
            About window = About.GetInstance();
            window.ShowDialog();
        }

        private void MnCreators_Click(object sender, RoutedEventArgs e)
        {
            Creators window = Creators.GetInstance();
            window.ShowDialog();
        }

        private void MnVersion_Click(object sender, RoutedEventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            MessageBox.Show("Obecna wersja programu to: " + version, "Wersja Aplikacji", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MnLogOff_Click(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Visibility = Visibility.Visible;
            Close();
        }

        private void BtnEmployeeHaslo_Click(object sender, RoutedEventArgs e)
        {
            _controller.ShowPassword();
        }

        private void CmbCategoryId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _controller.CmbCategoryIdChange();
        }

        private void BtnDoGridFourRealizujZamowienie_Click(object sender, RoutedEventArgs e)
        {
            _controller.RegisterTransaction();
        }

        private void RbItemUsun_Checked(object sender, RoutedEventArgs e)
        {
            _controller.ItemsShowDelete();
        }

        private void BtnItemUsun_Click(object sender, RoutedEventArgs e)
        {
            _controller.DeleteItems();
        }
    }
}