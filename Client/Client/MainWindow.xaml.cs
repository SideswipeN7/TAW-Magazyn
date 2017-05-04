using System.Windows;
using System;
using System.IO;
using System.Reflection;
using PluginExecutor;
using System.Collections;
using Client.Model;
using System.Collections.Generic;
using System.Threading;
using System.Data;
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
            _controller.CategoriesAdd();
        }

        private void BtnCategoryModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            _controller.CategoriesModify();
        }

        private void BtnCategoryUsun_Click(object sender, RoutedEventArgs e)
        {
            _controller.CategoriesDelete();
        }

        private void BtnCategorySzukaj_Click(object sender, RoutedEventArgs e)
        {
            _controller.CategoriesSearch();
        }
    }
}