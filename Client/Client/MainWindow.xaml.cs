using System.Windows;
using Client.Controller;

namespace Client
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Manager _controller;
        public MainWindow()
        {
            InitializeComponent();
            _controller = Manager.GetInstance(this);
            //Show Data
            _controller.GetCategoryData();
            _controller.GetMagazineState();
            _controller.GetItemData();
            //LoadData
            _controller.LoadCategoriesMagazineSate();
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
    }
}