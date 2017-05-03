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

namespace Client
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btndodaj.Visibility = Visibility.Collapsed;
            txt_nazwa.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;
            cb_kat_id.Visibility = Visibility.Collapsed;
        }

        private void rb_kat_dodaj_Checked(object sender, RoutedEventArgs e)
        {
            btndodaj.Visibility = Visibility.Visible;
            txt_nazwa.Visibility = Visibility.Visible;
            cb_kat_id.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
        }

        private void rb_kat_wszystko_Checked(object sender, RoutedEventArgs e)
        {
            btndodaj.Visibility = Visibility.Collapsed;
            txt_nazwa.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;
            cb_kat_id.Visibility = Visibility.Collapsed;
        }

        private void rb_kat_modyfikuj_Checked(object sender, RoutedEventArgs e)
        {
            cb_kat_id.Visibility = Visibility.Visible;
            btnmodyfikuj.Visibility = Visibility.Visible;
            btnusun.Visibility = Visibility.Collapsed;
            btndodaj.Visibility = Visibility.Collapsed;
            txt_nazwa.Visibility = Visibility.Visible;
        }

        private void rb_kat_usun_Checked(object sender, RoutedEventArgs e)
        {
            btnusun.Visibility = Visibility.Visible;
            txt_nazwa.Visibility = Visibility.Visible;
            cb_kat_id.Visibility = Visibility.Visible;
            btndodaj.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
        }
    }
}