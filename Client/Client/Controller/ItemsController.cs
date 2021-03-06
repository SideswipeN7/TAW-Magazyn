﻿using Client.Communication;
using Client.Interfaces;
using Client.Model;
using Client.Validators;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Diagnostics.Debug;

namespace Client.Controller
{
    public class ItemsController : IWork
    {
        private static ItemsController _instance;
        private Admin _window { get; set; }
        private ICommItems _comm { get; set; } = CommItems.GetInstance();
        private List<Artykul> art;
        private List<Artykul> artSearched;

        protected ItemsController()
        {
            artSearched = new List<Artykul>();
        }

        public static ItemsController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new ItemsController();
            }
            _instance._window = window;
            return _instance;
        }
        public static ItemsController GetInstance(ICommItems _comm)
        {
            if (_instance == null)
            {
                _instance = new ItemsController();
            }
            _instance._comm = _comm;
            return _instance;
        }

        public void AddData()
        {
            try
            {
                int quantity;
                decimal price;
                IValidate validator = ItemAddValidator.GetInstance(_window);
                if (validator.Validate())
                {
                    Int32.TryParse(_window.TxbItemIlosc.Text, out quantity);
                    Decimal.TryParse(_window.TxbItemCenaMin.Text, out price);

                    Artykul artykul = new Artykul()
                    {
                        Cena = price,
                        Ilosc = quantity,
                        Nazwa = _window.TxbItemINazwa.Text,
                        idKategorii = ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii,
                        Kategorie = new Kategoria()
                        {
                            idKategorii = ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii,
                            Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content
                        }
                    };
                    Task.Factory.StartNew(() =>
                    {
                        _comm.RegisterItem(artykul);
                    }).ContinueWith(x =>
                    Task.Factory.StartNew(() =>
                    {
                        GetData();
                    }));
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in {nameof(_instance)}  {nameof(AddData)}: {ex} ");
            }
        }

        public void ChangeData()
        {
            try
            {


                IValidate validator = ItemChangeValidator.GetInstance(_window);
                if (validator.Validate())
                {
                    int quantity = Int32.Parse(_window.TxbItemIlosc.Text);
                    decimal price = Decimal.Parse(_window.TxbItemCenaMin.Text);

                    Artykul artykul = new Artykul()
                    {
                        idArtykulu = ((Artykul)_window.DgItemLista.SelectedItem).idArtykulu,
                        Cena = price,
                        Ilosc = quantity,
                        Nazwa = _window.TxbItemINazwa.Text,
                        idKategorii = ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii,
                        Kategorie = new Kategoria()
                        {
                            idKategorii = ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii,
                            Nazwa = (String)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Content
                        }
                    };
                    Task.Factory.StartNew(() =>
                    {
                        _comm.ChangeItem(artykul);
                    }).ContinueWith(x =>
                    Task.Factory.StartNew(() =>
                    {
                        GetData();
                    }));
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(ChangeData)}: {ex} ");
            }
        }

        public void DeleteData()
        {
            try
            {
                if (_window.DgItemLista.SelectedIndex >= 0)
                {
                    _comm.DeleteItem(((Artykul)_window.DgItemLista.SelectedItem).idArtykulu);
                    GetData();
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(DeleteData)}: {ex} ");
            }
        }

        public void ShowData()
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _window.DgItemLista.Items.Clear();
                    if (_window.RbItemSzukaj.IsChecked == false)
                    {
                        foreach (Artykul r in art)
                        {
                            _window.DgItemLista.Items.Add(r);
                        }
                    }
                    if (_window.RbItemSzukaj.IsChecked == true)
                    {
                        foreach (Artykul r in artSearched)
                        {
                            _window.DgItemLista.Items.Add(r);
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  { nameof(ShowData)}: {ex} ");
            }
        }

        public void ShowSelectedData()
        {
            try
            {
                if (_window.DgItemLista.SelectedIndex >= 0)
                {
                    for (int i = 0; i < _window.CmbItemKategoria.Items.Count; i++)
                    {
                        if (((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.Items.GetItemAt(i)).Tag).idKategorii == ((Artykul)_window.DgItemLista.SelectedItem).Kategorie.idKategorii)
                        {
                            _window.CmbItemKategoria.SelectedIndex = i;
                        }
                    }
                    _window.TxbItemCenaMin.Text = ((Artykul)_window.DgItemLista.SelectedItem).Cena + "";
                    _window.TxbItemIlosc.Text = ((Artykul)_window.DgItemLista.SelectedItem).Ilosc + "";
                    _window.TxbItemINazwa.Text = ((Artykul)_window.DgItemLista.SelectedItem).Nazwa;
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(ShowSelectedData)}: {ex} ");
            }
        }

        public IEnumerable<object> GetData()
        {
            try
            {
                Task<IEnumerable<Artykul>>.Factory.StartNew(() =>
                {
                    return _comm.GetItems();
                }).ContinueWith(x =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        art = x.Result.ToList();
                        ShowData();
                        return art;
                    });
                });
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  {nameof(GetData)}: {ex} ");
            }
            return null;
        }

        public void SearchData()
        {
            try
            {
                artSearched.Clear();
                Task.Factory.StartNew(() =>
                {
                    GetData();
                }).ContinueWith(x =>
                Task.Factory.StartNew(() =>
                {
                    _window.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        List<Artykul> list = new List<Artykul>();
                        //1
                        if (_window.ChbItemCena.IsChecked == true)
                        {
                            decimal max, min;
                            if (_window.TxbItemCenaMin.Text.Length == 0)
                                _window.TxbItemCenaMin.Text = "0";
                            if (_window.TxbItemCenaMax.Text.Length == 0)
                                _window.TxbItemCenaMax.Text = "9999";
                            if (!Decimal.TryParse(_window.TxbItemCenaMin.Text, out min))
                            {
                                MessageBox.Show("Zły format Ceny Minimalnej", "Bład", MessageBoxButton.OK);
                            }
                            else
                            if (!Decimal.TryParse(_window.TxbItemCenaMax.Text, out max))
                            { MessageBox.Show("Zły format Ceny Maksymalnej", "Bład", MessageBoxButton.OK); }
                            else
                            {
                                foreach (Artykul a in art)
                                {
                                    if (a.Cena < min || a.Cena > max)
                                    {
                                        list.Add(a);
                                    }
                                }
                            }
                        }
                        //2
                        if (_window.ChbItemIlosc.IsChecked == true)
                        {
                            decimal ilosc;
                            if (Decimal.TryParse(_window.TxbItemCenaMin.Text, out ilosc))
                            {
                                foreach (Artykul a in art)
                                {
                                    if (a.Ilosc > ilosc)
                                    {
                                        list.Add(a);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Zły format Ceny", "Bład", MessageBoxButton.OK);
                            }
                        }
                        //3
                        if (_window.ChbItemKategoria.IsChecked == true)
                        {
                            foreach (Artykul a in art)
                            {
                                if (a.idKategorii != ((Kategoria)((ComboBoxItem)_window.CmbItemKategoria.SelectedItem).Tag).idKategorii)
                                {
                                    list.Add(a);
                                }
                            }
                        }
                        //4
                        if (_window.ChbItemNazwa.IsChecked == true)
                        {
                            foreach (Artykul a in art)
                            {
                                if (!a.Nazwa.ToLower().Contains(_window.TxbItemINazwa.Text.ToLower()))
                                {
                                    list.Add(a);
                                }
                            }
                        }
                        foreach (Artykul a in art)
                        {
                            if (!list.Contains(a))
                                artSearched.Add(a);
                        }
                        ShowData();
                    }));
                }));
            }
            catch (Exception ex)
            {
                WriteLine($"Error in  {nameof(_instance)}  { nameof(SearchData)}: {ex} ");
            }
        }
    }
}