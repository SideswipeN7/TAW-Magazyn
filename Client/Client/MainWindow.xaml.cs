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
<<<<<<< HEAD
<<<<<<< HEAD
            lblid.Visibility = Visibility.Collapsed;
            lblnazwa.Visibility = Visibility.Collapsed;
            btndodaj.Visibility = Visibility.Collapsed;
            txt_nazwa.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;
            cb_kat_id.Visibility = Visibility.Collapsed;
        }

        private void rb_kat_dodaj_Checked(object sender, RoutedEventArgs e)
        {
            lblid.Visibility = Visibility.Collapsed;
            lblnazwa.Visibility = Visibility.Visible;
            btndodaj.Visibility = Visibility.Visible;
            txt_nazwa.Visibility = Visibility.Visible;
            cb_kat_id.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
=======
            lblkategoria.Visibility = Visibility.Collapsed;
            lblnazwa.Visibility = Visibility.Collapsed;
            lblcena.Visibility = Visibility.Collapsed;
            lblilosc.Visibility = Visibility.Collapsed;

            btndodaj.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;

            txt_nazwa.Visibility = Visibility.Collapsed;
            txt_cena.Visibility = Visibility.Collapsed;
            txt_ilosc.Visibility = Visibility.Collapsed;

            cb_nazwakat.Visibility = Visibility.Collapsed;
>>>>>>> origin/TabItem-Artykul
        }

        private void rb_kat_wszystko_Checked(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            lblid.Visibility = Visibility.Collapsed;
            lblnazwa.Visibility = Visibility.Collapsed;
            btndodaj.Visibility = Visibility.Collapsed;
            txt_nazwa.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;
            cb_kat_id.Visibility = Visibility.Collapsed;
=======
            lblkategoria.Visibility = Visibility.Collapsed;
            lblnazwa.Visibility = Visibility.Collapsed;
            lblcena.Visibility = Visibility.Collapsed;
            lblilosc.Visibility = Visibility.Collapsed;

            btndodaj.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;

            txt_nazwa.Visibility = Visibility.Collapsed;
            txt_cena.Visibility = Visibility.Collapsed;
            txt_ilosc.Visibility = Visibility.Collapsed;

            cb_nazwakat.Visibility = Visibility.Collapsed;
        }

        private void rb_kat_dodaj_Checked(object sender, RoutedEventArgs e)
        {
            lblkategoria.Visibility = Visibility.Visible;
            lblnazwa.Visibility = Visibility.Visible;
            lblcena.Visibility = Visibility.Visible;
            lblilosc.Visibility = Visibility.Visible;

            btndodaj.Visibility = Visibility.Visible;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Collapsed;

            txt_nazwa.Visibility = Visibility.Visible;
            txt_cena.Visibility = Visibility.Visible;
            txt_ilosc.Visibility = Visibility.Visible;

            cb_nazwakat.Visibility = Visibility.Visible;
>>>>>>> origin/TabItem-Artykul
        }

        private void rb_kat_modyfikuj_Checked(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            lblid.Visibility = Visibility.Visible;
            lblnazwa.Visibility = Visibility.Visible;
            cb_kat_id.Visibility = Visibility.Visible;
            btnmodyfikuj.Visibility = Visibility.Visible;
            btnusun.Visibility = Visibility.Collapsed;
            btndodaj.Visibility = Visibility.Collapsed;
            txt_nazwa.Visibility = Visibility.Visible;
=======
            lblkategoria.Visibility = Visibility.Visible;
            lblnazwa.Visibility = Visibility.Visible;
            lblcena.Visibility = Visibility.Visible;
            lblilosc.Visibility = Visibility.Visible;

            btndodaj.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Visible;
            btnusun.Visibility = Visibility.Collapsed;

            txt_nazwa.Visibility = Visibility.Visible;
            txt_cena.Visibility = Visibility.Visible;
            txt_ilosc.Visibility = Visibility.Visible;

            cb_nazwakat.Visibility = Visibility.Visible;
>>>>>>> origin/TabItem-Artykul
        }

        private void rb_kat_usun_Checked(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            lblid.Visibility = Visibility.Visible;
            lblnazwa.Visibility = Visibility.Visible;
            btnusun.Visibility = Visibility.Visible;
            txt_nazwa.Visibility = Visibility.Visible;
            cb_kat_id.Visibility = Visibility.Visible;
            btndodaj.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
=======

        }


        private void btnDodajArtykuly_Click(object sender, RoutedEventArgs e)
        {

>>>>>>> origin/TabItem_Stan_Magazynu
=======
            lblkategoria.Visibility = Visibility.Collapsed;
            lblnazwa.Visibility = Visibility.Visible;
            lblcena.Visibility = Visibility.Collapsed;
            lblilosc.Visibility = Visibility.Collapsed;

            btndodaj.Visibility = Visibility.Collapsed;
            btnmodyfikuj.Visibility = Visibility.Collapsed;
            btnusun.Visibility = Visibility.Visible;

            txt_nazwa.Visibility = Visibility.Visible;
            txt_cena.Visibility = Visibility.Collapsed;
            txt_ilosc.Visibility = Visibility.Collapsed;

            cb_nazwakat.Visibility = Visibility.Collapsed;
>>>>>>> origin/TabItem-Artykul
        }
    }
}