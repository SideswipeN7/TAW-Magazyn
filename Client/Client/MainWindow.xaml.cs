﻿using System.Windows;
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
            _controller.GetClientData();
            _controller.GetClientTransactionData();
            //LoadData
            _controller.LoadCategoriesMagazineSate();
            _controller.LoadClients();//ERROR
            _controller.LoadTransactionsDoProducts();
            _controller.LoadTransactionsDoStates();
            _controller.LoadTransactionsDoSupplier();
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
    }
}