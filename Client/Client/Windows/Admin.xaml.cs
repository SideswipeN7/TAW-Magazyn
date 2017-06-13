using Client.Adapter;
using Client.Controller;
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
        private ItemsController _itemController = ItemsController.GetInstance(_instance);
        public int ID { get; set; }

        public Adapter.Service _service { get; set; }

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
                _instance.Title = "Tryb Administratora";
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

            ID = 0;
            _service = Service.GetInstance(ID, this);
            _service.GetAll();
            _service.LoadAll();
            _service.SelectaAll();
        }

        //Magazine State
        private void DgStateLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.SelectedMagazine();
        }

        private void RbStateSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowSearchMagazine();
        }

        private void RbStateWszystko_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAllMagazine();
        }

        private void BtnSateSzukaj_Click(object sender, RoutedEventArgs e)
        {
            _service.SearchMagazine();
        }

        //Categories
        private void CmbCategoryId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.CmbCategoryIdChange();
        }

        private void DgCategoryLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.SelectedCategories();
        }

        private void RbCategoryWszystko_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAllCategories();
        }

        private void RbCategoryDodaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAddCategories();
        }

        private void RbCategoryModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowModifyCategories();
        }

        private void RbCategoryUsun_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowDeleteCategories();
        }

        private void RbCategorySzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowSearchCategories();
        }

        private void BtnCategoryDodaj_Click(object sender, RoutedEventArgs e)
        {
            _service.AddCategories();
        }

        private void BtnCategoryModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _service.ModifyCategories();
        }

        private void BtnCategoryUsun_Click(object sender, RoutedEventArgs e)
        {
            _service.DeleteCategories();
        }

        private void BtnCategorySzukaj_Click(object sender, RoutedEventArgs e)
        {
            _service.SearchCategories();
        }

        //Items
        private void DgItemLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.SelectedItems();
        }

        private void RbItemWszystko_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAllItems();
        }

        private void RbItemSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowSearchItems();
        }

        private void RbItemModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowModifyItems();
        }

        private void RbItemDodaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAddItems();
        }

        private void BtnItemModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _service.ModifyItems();
        }

        private void BtnItemDodaj_Click(object sender, RoutedEventArgs e)
        {
            _service.AddItems();
        }

        private void BtnItemSzukaj_Click(object sender, RoutedEventArgs e)
        {
            _service.SearchItems();
        }

        private void RbItemUsun_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowDeleteItems();
        }

        private void BtnItemUsun_Click(object sender, RoutedEventArgs e)
        {
            _service.DeleteItems();
        }

        //Clients
        private void DgClientsLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.SelectedClients(); ;
        }

        private void RbClientsWszyscy_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAllClients();
        }

        private void RbClientsSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowSearchClients();
        }

        private void RbClientsDodaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAddClients();
        }

        private void RbClientsModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowModifyClients();
        }

        private void RbClientsUsun_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowDeleteClients();
        }

        private void BtnClientsSzukaj_Click(object sender, RoutedEventArgs e)
        {
            _service.SearchClients();
        }

        private void BtnClientsModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _service.ModifyClients();
        }

        private void BtnClientsUsun_Click(object sender, RoutedEventArgs e)
        {
            _service.DeleteClients();
        }

        private void BtnClientsDodaj_Click(object sender, RoutedEventArgs e)
        {
            _service.AddClients();
        }

        //Transactions
        private void RbDoGridOneNowyKlient_Checked(object sender, RoutedEventArgs e)
        {
            _service.TransactionNewSelcted();
        }

        private void RbDoGridOneStalyKlient_Checked(object sender, RoutedEventArgs e)
        {
            _service.TransactionOldSelcted();
        }

        private void CmbDoGridTwoNazwisko_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.SelectClientDoTransactionSurname();
        }

        private void CmbDoGridTwoFirma_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.SelectClientDoTransactionFirm();
        }

        private void BtnDoGridThreeDodajProdukty_Click(object sender, RoutedEventArgs e)
        {
            _service.AddToCart();
        }

        private void BtnDoGridFiveUsun_Click(object sender, RoutedEventArgs e)
        {
            _service.DeleteFormCart();
        }

        private void DgOverviewGridOne_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.SelectedTransaction();
        }

        private void RbOverviewGridOneWszystkie_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAllTransaction();
        }

        private void RbOverviewGridOneSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowSearchTransaction();
        }

        private void BtnDoGridFourRealizujZamowienie_Click(object sender, RoutedEventArgs e)
        {
            _service.AddTransaction();
        }

        private void BtnOverviewGridTwoSzukaj_Click(object sender, RoutedEventArgs e)
        {
            _service.SearchTransaction();
        }

        //Employee
        private void DgEmployeesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _service.SelectedEmployee();
        }

        private void BtnEmployeeDodaj_Click(object sender, RoutedEventArgs e)
        {
            _service.AddEmployee();
        }

        private void BtnEmployeeModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _service.ModifyEmployee();
        }

        private void BtnEmployeeUsun_Click(object sender, RoutedEventArgs e)
        {
            _service.DeleteEmployee();
        }

        private void RdEmployeeWszyscy_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAllEmployee();
        }

        private void RdEmployeeDodaj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowAddEmployee();
        }

        private void RdEmployeeModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowModifyEmployee();
        }

        private void RdEmployeeUsun_Checked(object sender, RoutedEventArgs e)
        {
            _service.ShowDeleteEmployee();
        }

        private void BtnEmployeeHaslo_Click(object sender, RoutedEventArgs e)
        {
            _service.ShowPassword();
        }

#pragma warning disable IDE1006 // Naming Styles

        private void btnTabOverviewFaktura_Click(object sender, RoutedEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            _service.GetFacture();
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
#pragma warning disable IDE0017 // Simplify object initialization
            Login window = new Login();
#pragma warning restore IDE0017 // Simplify object initialization
            window.Visibility = Visibility.Visible;
            Close();
        }

        private void TabCtrlTransactions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
    }
}