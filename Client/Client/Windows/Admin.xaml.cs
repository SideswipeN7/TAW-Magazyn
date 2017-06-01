using Client.Controller;
using System.Diagnostics;
using System.Threading;
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

        private Adapter.Service service { get; set; }

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

            ID = 0;
            service = Adapter.Service.GetInstance(ID, this);
            service.GetAll();
            service.LoadAll();
            //Thread.Sleep(250);
            service.SelectaAll();
        }

        //Magazine State
        private void DgStateLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.SelectedMagazine();
        }
        private void RbStateSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowSearchMagazine();
        }

        private void RbStateWszystko_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAllMagazine();
        }

        private void BtnSateSzukaj_Click(object sender, RoutedEventArgs e)
        {
            service.SearchMagazine();
        }

        //Categories
        private void CmbCategoryId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.CmbCategoryIdChange();
        }

        private void DgCategoryLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.SelectedCategories();
        }

        private void RbCategoryWszystko_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAllCategories();
        }

        private void RbCategoryDodaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAddCategories();
        }

        private void RbCategoryModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowModifyCategories();
        }

        private void RbCategoryUsun_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowDeleteCategories();
        }

        private void RbCategorySzukaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowSearchCategories();
        }

        private void BtnCategoryDodaj_Click(object sender, RoutedEventArgs e)
        {
            service.AddCategories();
        }

        private void BtnCategoryModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            service.ModifyCategories();
        }

        private void BtnCategoryUsun_Click(object sender, RoutedEventArgs e)
        {
            service.DeleteCategories();
        }

        private void BtnCategorySzukaj_Click(object sender, RoutedEventArgs e)
        {
            service.SearchCategories();
        }

        //Items
        private void DgItemLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.SelectedItems();
        }

        private void RbItemWszystko_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAllItems();
        }

        private void RbItemSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowSearchItems();
        }

        private void RbItemModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowModifyItems();
        }

        private void RbItemDodaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAddItems();
        }

        private void BtnItemModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            service.ModifyItems();
        }

        private void BtnItemDodaj_Click(object sender, RoutedEventArgs e)
        {
            service.AddItems();
        }

        private void BtnItemSzukaj_Click(object sender, RoutedEventArgs e)
        {
            service.SearchItems();
        }

        private void RbItemUsun_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowDeleteItems();
        }

        private void BtnItemUsun_Click(object sender, RoutedEventArgs e)
        {
            service.DeleteItems();
        }

        //Clients
        private void DgClientsLista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.SelectedClients(); ;
        }

        private void RbClientsWszyscy_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAllClients();
        }

        private void RbClientsSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowSearchClients();
        }

        private void RbClientsDodaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAddClients();
        }

        private void RbClientsModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowModifyClients();
        }

        private void RbClientsUsun_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowDeleteClients();
        }

        private void BtnClientsSzukaj_Click(object sender, RoutedEventArgs e)
        {
            service.SearchClients();
        }

        private void BtnClientsModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            service.ModifyClients();
        }

        private void BtnClientsUsun_Click(object sender, RoutedEventArgs e)
        {
            service.DeleteClients();
        }

        private void BtnClientsDodaj_Click(object sender, RoutedEventArgs e)
        {
            service.AddClients();
        }

        //Transactions
        private void RbDoGridOneNowyKlient_Checked(object sender, RoutedEventArgs e)
        {
            service.TransactionNewSelcted();
        }

        private void RbDoGridOneStalyKlient_Checked(object sender, RoutedEventArgs e)
        {
            service.TransactionOldSelcted();
        }

        private void CmbDoGridTwoNazwisko_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.SelectClientDoTransactionSurname();
        }

        private void CmbDoGridTwoFirma_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.SelectClientDoTransactionFirm();
        }

        private void BtnDoGridThreeDodajProdukty_Click(object sender, RoutedEventArgs e)
        {
            service.AddToCart();
        }

        private void BtnDoGridFiveUsun_Click(object sender, RoutedEventArgs e)
        {
            service.DeleteFormCart();
        }

        private void DgOverviewGridOne_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.SelectedTransaction();
        }

        private void RbOverviewGridOneWszystkie_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAllTransaction();
        }

        private void RbOverviewGridOneSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowSearchTransaction();
        }

        private void BtnDoGridFourRealizujZamowienie_Click(object sender, RoutedEventArgs e)
        {
            service.AddTransaction();
        }

        private void BtnOverviewGridTwoSzukaj_Click(object sender, RoutedEventArgs e)
        {
            service.SearchTransaction();
        }

        //Employee
        private void DgEmployeesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            service.SelectedEmployee();
        }

        private void BtnEmployeeDodaj_Click(object sender, RoutedEventArgs e)
        {
            service.AddEmployee();
        }

        private void BtnEmployeeModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            service.ModifyEmployee();
        }

        private void BtnEmployeeUsun_Click(object sender, RoutedEventArgs e)
        {
            service.DeleteEmployee();
        }

        private void RdEmployeeWszyscy_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAllEmployee();
        }

        private void RdEmployeeDodaj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowAddEmployee();
        }

        private void RdEmployeeModyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowModifyEmployee();
        }

        private void RdEmployeeUsun_Checked(object sender, RoutedEventArgs e)
        {
            service.ShowDeleteEmployee();
        }

        private void BtnEmployeeHaslo_Click(object sender, RoutedEventArgs e)
        {
            service.ShowPassword();
        }

        private void btnTabOverviewFaktura_Click(object sender, RoutedEventArgs e)
        {
            service.GetFacture();
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

       
    }
}