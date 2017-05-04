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
        }

        //Magazine State
        private void RbStateSzukaj_Checked(object sender, RoutedEventArgs e)
        {
            ChbStateCena.Visibility = Visibility.Visible;
            ChbStateIlosc.Visibility = Visibility.Visible;
            ChbStateKategoria.Visibility = Visibility.Visible;
            ChbStateNazwa.Visibility = Visibility.Visible;

            LblStateCena.Visibility = Visibility.Visible;
            LblStateIlosc.Visibility = Visibility.Visible;
            LblStateKategoria.Visibility = Visibility.Visible;
            LblStateNazwa.Visibility = Visibility.Visible;

            TxbStateCena.Visibility = Visibility.Visible;
            TxbStateIlosc.Visibility = Visibility.Visible;
            TxbStateNazwa.Visibility = Visibility.Visible;
            CmbStateKategoria.Visibility = Visibility.Visible;

            BtnSateSzukaj.Visibility = Visibility.Visible;
        }

        private void RbStateDodaj_Checked(object sender, RoutedEventArgs e)
        {
            ChbStateCena.Visibility = Visibility.Hidden;
            ChbStateIlosc.Visibility = Visibility.Hidden;
            ChbStateKategoria.Visibility = Visibility.Hidden;
            ChbStateNazwa.Visibility = Visibility.Hidden;

            LblStateCena.Visibility = Visibility.Hidden;
            LblStateIlosc.Visibility = Visibility.Hidden;
            LblStateKategoria.Visibility = Visibility.Hidden;
            LblStateNazwa.Visibility = Visibility.Hidden;

            TxbStateCena.Visibility = Visibility.Hidden;
            TxbStateIlosc.Visibility = Visibility.Hidden;
            TxbStateNazwa.Visibility = Visibility.Hidden;
            CmbStateKategoria.Visibility = Visibility.Hidden;

            BtnSateSzukaj.Visibility = Visibility.Hidden;
        }
    }
}