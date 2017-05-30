﻿﻿using Client.Communication;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Client.Controller
{
    internal class CategoryController : IWork
    {
        private static CategoryController _instance;
        Admin _window { get; set; }
        private ICommunication _comm;
        private List<Kategoria> categories;

        private CategoryController()
        {
            _comm = Communicator.GetInstance();
            // _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
            _comm.SetUrlAddress("http://localhost:52992");
        }
        public static CategoryController GetInstance(Admin window)
        {
            if (_instance == null)
            {
                _instance = new CategoryController();
            }
            _instance._window = window;
            return _instance;
        }

        public void AddData()
        {
            try
            {
                if (_window.TxbCategoryNazwa.Text.Length > 5)
                {
                    _comm.RegisterCategory(new Kategoria() { Nazwa = _window.TxbCategoryNazwa.Text });
                    GetData();
                }
                else
                {
                    MessageBox.Show("Nazwa zbyt krótka", "Bład", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Category Controller AddData: {ex}");
            }
        }

        public void ChangeData()

        {
            try
            {
                if (_window.TxbCategoryNazwa.Text.Length > 5)
                {
                    if (_window.CmbCategoryId.SelectedIndex > 0)
                    {
                        _comm.ChangeCategory(new Kategoria() { idKategorii = (int)_window.CmbCategoryId.SelectedItem, Nazwa = _window.TxbCategoryNazwa.Text });
                        GetData();
                    }
                }
                else
                {
                    MessageBox.Show("Nazwa zbyt krótka", "Bład", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Category Controller ChangeData: {ex}");
            }
        }

        public void DeleteData()
        {
            try
            {
                if (_window.DgCategoryLista.SelectedIndex >= 0)
                {
                    _comm.DeleteCategory((Kategoria)_window.DgCategoryLista.SelectedItem);
                    GetData();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Category Controller DeleteData: {ex}");
            }
        }



        public void ShowCategoryData(IEnumerable<Kategoria> categories)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                _window.CmbCategoryId.Items.Clear();
                _window.DgCategoryLista.Items.Clear();
                foreach (Kategoria r in categories)
                {
                    _window.DgCategoryLista.Items.Add(r);
                    _window.CmbCategoryId.Items.Add(r.idKategorii);
                }
            }));
        }


        public void GetData()
        {
            try
            {
                Task<IEnumerable<Kategoria>>.Factory.StartNew(() =>
                {
                    return _comm.GetCategories();
                }).ContinueWith(x =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        categories = x.Result.ToList();
                        _window.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (_window.ChbCategoryNazwa.IsChecked == false)
                                ShowData();
                        }));
                    });
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Category Controller GetData: {ex}");
            }
        }
              

        public void ShowData()
        {
            try
            {
                _window.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _window.CmbCategoryId.Items.Clear();
                    foreach (Kategoria r in categories)
                    {
                        _window.DgCategoryLista.Items.Add(r);
                        _window.CmbCategoryId.Items.Add(r.idKategorii);
                    }
                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Category Controller ShowData: {ex}");
            }
        }


        public void ShowSelectedData()
        {
            try
            {
                if (_window.DgCategoryLista.SelectedIndex >= 0)
                {
                    _window.TxbCategoryNazwa.Text = ((Kategoria)_window.DgCategoryLista.SelectedItem).Nazwa;
                    _window.CmbCategoryId.SelectedItem = ((Kategoria)_window.DgCategoryLista.SelectedItem).idKategorii;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Category Controller ShowSelectedData: {ex}");
            }

        }

        public void SearchData()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    GetData();
                }).ContinueWith(x =>
                Task.Factory.StartNew(() =>
                {
                    _window.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        List<Kategoria> list = new List<Kategoria>();
                        if (_window.ChbCategoryNazwa.IsChecked == true)
                        {
                            foreach (Kategoria k in categories)
                                if (!k.Nazwa.ToLower().Contains(_window.TxbCategoryNazwa.Text.ToLower()))
                                {
                                    list.Add(k);
                                }
                            foreach (Kategoria a in list)
                                categories.RemoveAll(ar => ar.idKategorii == a.idKategorii);
                            ShowData();
                        }
                    }));

                }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in Category Controller SearchData: {ex}");
            }
        }
    }
}