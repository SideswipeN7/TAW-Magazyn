﻿using Client.Communication;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Controller
{
    public sealed class Manager : IManager
    {
        private static Manager _instance;
        private MainWindow _window;
        private ICommunication _comm;

        private Manager()
        {
            _comm = Communicator.GetInstance();
            _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
        }
        public static Manager GetInstance(MainWindow window)
        {
            if (_instance == null)
            {
                _instance = new Manager();
            }
            _instance._window = window;
            return _instance;
        }


        //Magazine State
        public void LoadCategoriesMagazineSate()
        {
            IEnumerable<Kategoria> list = _comm.GetCategories();

            foreach (Kategoria r in list)
            {
                _window.CmbStateKategoria.Items.Add(new ComboBoxItem() { Content = r.Nazwa, Tag = r.idKategorii });

            }
        }



        internal void SearchCategoriesSate()
        {
            List<Artykul> list = new List<Artykul>();
            for (int i = 0; i < _window.DgStateLista.Items.Count; i++)
                list.Add((Artykul)_window.DgStateLista.Items.GetItemAt(i));
            _window.DgStateLista.Items.Clear();
            //1
            if (_window.ChbStateCena.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Cena < Decimal.Parse(_window.TxbStateCenaMin.Text) || list[i].Cena > Decimal.Parse(_window.TxbStateCenaMax.Text))
                        list.Remove(list[i]);
                }
            }
            //2
            if (_window.ChbStateIlosc.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Ilosc > Int32.Parse(_window.TxbStateIlosc.Text))
                        list.Remove(list[i]);
                }
            }
            //3
            if (_window.ChbStateKategoria.IsChecked == true)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    //TODO
                    if (list[i].idKategorii != (int)((ComboBoxItem)_window.CmbStateKategoria.SelectedItem).Tag)
                        list.Remove(list[i]);
                }

            }
            //4
            if (_window.ChbStateNazwa.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!(list[i].Nazwa.ToLower().Contains(_window.TxbStateNazwa.Text.ToLower())))
                        list.Remove(list[i]);
                }
            }
            ShowMagazineStateData(list);

        }

        internal void ShowMagazineStateAll()
        {
            _window.ChbStateCena.Visibility = Visibility.Hidden;
            _window.ChbStateIlosc.Visibility = Visibility.Hidden;
            _window.ChbStateKategoria.Visibility = Visibility.Hidden;
            _window.ChbStateNazwa.Visibility = Visibility.Hidden;

            _window.LblStateCena.Visibility = Visibility.Hidden;
            _window.LblStateIlosc.Visibility = Visibility.Hidden;
            _window.LblStateKategoria.Visibility = Visibility.Hidden;
            _window.LblStateNazwa.Visibility = Visibility.Hidden;

            _window.TxbStateCenaMin.Visibility = Visibility.Hidden;
            _window.TxbStateCenaMax.Visibility = Visibility.Hidden;
            _window.TxbStateIlosc.Visibility = Visibility.Hidden;
            _window.TxbStateNazwa.Visibility = Visibility.Hidden;
            _window.CmbStateKategoria.Visibility = Visibility.Hidden;

            _window.BtnSateSzukaj.Visibility = Visibility.Hidden;

            GetMagazineState();
        }
        internal void ShowMagazineStateSearch()
        {
            _window.ChbStateCena.Visibility = Visibility.Visible;
            _window.ChbStateIlosc.Visibility = Visibility.Visible;
            _window.ChbStateKategoria.Visibility = Visibility.Visible;
            _window.ChbStateNazwa.Visibility = Visibility.Visible;

            _window.LblStateCena.Visibility = Visibility.Visible;
            _window.LblStateIlosc.Visibility = Visibility.Visible;
            _window.LblStateKategoria.Visibility = Visibility.Visible;
            _window.LblStateNazwa.Visibility = Visibility.Visible;

            _window.TxbStateCenaMin.Visibility = Visibility.Visible;
            _window.TxbStateCenaMax.Visibility = Visibility.Visible;
            _window.TxbStateIlosc.Visibility = Visibility.Visible;
            _window.TxbStateNazwa.Visibility = Visibility.Visible;
            _window.CmbStateKategoria.Visibility = Visibility.Visible;

            _window.BtnSateSzukaj.Visibility = Visibility.Visible;
        }

        public void GetMagazineState()
        {
            _window.DgStateLista.Items.Clear();
            Task<IEnumerable<Artykul>>.Factory.StartNew(() =>
            {
                return _comm.GetItems();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowMagazineStateData(x.Result);
                });
            });
        }
        private void ShowMagazineStateData(IEnumerable<Artykul> categories)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Artykul r in categories)
                    _window.DgStateLista.Items.Add(r);

            }));
        }
        //Categories
        public void GetCategoryData()
        {
            Task<IEnumerable<Kategoria>>.Factory.StartNew(() =>
            {
                return _comm.GetCategories();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowCategoryData(x.Result);
                });
            });
        }
        private void ShowCategoryData(IEnumerable<Kategoria> categories)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Kategoria r in categories)
                    _window.DgCategoryLista.Items.Add(r);

            }));
        }

        internal void ShowCategoriesSate()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Hidden;
            _window.LblCategoryNazwa.Visibility = Visibility.Hidden;

            _window.TxbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.CmbCategoryId.Visibility = Visibility.Hidden;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        internal void ShowCategoriesAdd()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Hidden;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Hidden;

            _window.BtnCategoryDodaj.Visibility = Visibility.Visible;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        internal void ShowCategoriesSearch()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Visible;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Visible;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        internal void ShowCategoriesDelete()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Visible;
        }

        internal void ShowCategoriesModify()
        {
            _window.ChbCategoryNazwa.Visibility = Visibility.Hidden;

            _window.LblCategoryId.Visibility = Visibility.Visible;
            _window.LblCategoryNazwa.Visibility = Visibility.Visible;

            _window.TxbCategoryNazwa.Visibility = Visibility.Visible;

            _window.CmbCategoryId.Visibility = Visibility.Visible;

            _window.BtnCategoryDodaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryModyfikuj.Visibility = Visibility.Visible;
            _window.BtnCategorySzukaj.Visibility = Visibility.Hidden;
            _window.BtnCategoryUsun.Visibility = Visibility.Hidden;
        }

        internal void CategoriesAdd()
        {
            if (_window.TxbCategoryNazwa.Text.Length > 5)
            {
                if (_comm.RegisterCategory(new Kategoria() { Nazwa = _window.TxbCategoryNazwa.Text }))
                {
                    GetCategoryData();
                }
            }
        }

        internal void CategoriesModify()
        {
            if (_window.TxbCategoryNazwa.Text.Length > 5)
            {
                if (_window.CmbCategoryId.SelectedIndex > 0)
                {
                    if (_comm.ChangeCategory(new Kategoria() { idKategorii = (int)_window.CmbCategoryId.SelectedItem, Nazwa = _window.TxbCategoryNazwa.Text }))
                    {
                        GetCategoryData();
                    }

                }
            }
        }

        internal void CategoriesDelete()
        {
            //TODO
            throw new NotImplementedException();
        }

        internal void CategoriesSearch()
        {
            List<Kategoria> list = new List<Kategoria>();
            for (int i = 0; i < _window.DgCategoryLista.Items.Count; i++)
                list.Add((Kategoria)_window.DgCategoryLista.Items.GetItemAt(i));
            _window.DgCategoryLista.Items.Clear();
            //1
            if (_window.ChbCategoryNazwa.IsChecked == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Nazwa.ToLower().Contains(_window.TxbCategoryNazwa.Text.ToLower()))
                        list.Remove(list[i]);
                }
                GetCategoryData();
            }
        }

        //Items
        internal void ShowItemsSearch()
        {
            _window.ChbItemCena.Visibility = Visibility.Visible;
            _window.ChbItemIlosc.Visibility = Visibility.Visible;
            _window.ChbItemKategoria.Visibility = Visibility.Visible;
            _window.ChbItemNazwa.Visibility = Visibility.Visible;

            _window.LblItemCena.Visibility = Visibility.Visible;
            _window.LblItemIlosc.Visibility = Visibility.Visible;
            _window.LblItemKategoria.Visibility = Visibility.Visible;
            _window.LblItemNazwa.Visibility = Visibility.Visible;

            _window.TxbItemCenaMax.Visibility = Visibility.Visible;
            _window.TxbItemCenaMin.Visibility = Visibility.Visible;
            _window.TxbItemIlosc.Visibility = Visibility.Visible;
            _window.TxbItemINazwa.Visibility = Visibility.Visible;

            _window.CmbItemKategoria.Visibility = Visibility.Visible;

            _window.BtnItemDodaj.Visibility = Visibility.Hidden;
            _window.BtnItemModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnItemSzukaj.Visibility = Visibility.Visible;
        }

        internal void ShowItemsModify()
        {
            _window.ChbItemCena.Visibility = Visibility.Hidden;
            _window.ChbItemIlosc.Visibility = Visibility.Hidden;
            _window.ChbItemKategoria.Visibility = Visibility.Hidden;
            _window.ChbItemNazwa.Visibility = Visibility.Hidden;

            _window.LblItemCena.Visibility = Visibility.Visible;
            _window.LblItemIlosc.Visibility = Visibility.Visible;
            _window.LblItemKategoria.Visibility = Visibility.Visible;
            _window.LblItemNazwa.Visibility = Visibility.Visible;

            _window.TxbItemCenaMax.Visibility = Visibility.Hidden;
            _window.TxbItemCenaMin.Visibility = Visibility.Visible;
            _window.TxbItemIlosc.Visibility = Visibility.Visible;
            _window.TxbItemINazwa.Visibility = Visibility.Visible;

            _window.CmbItemKategoria.Visibility = Visibility.Visible;

            _window.BtnItemDodaj.Visibility = Visibility.Hidden;
            _window.BtnItemModyfikuj.Visibility = Visibility.Visible;
            _window.BtnItemSzukaj.Visibility = Visibility.Hidden;
        }

        internal void ShowItemsAdd()
        {
            _window.ChbItemCena.Visibility = Visibility.Hidden;
            _window.ChbItemIlosc.Visibility = Visibility.Hidden;
            _window.ChbItemKategoria.Visibility = Visibility.Hidden;
            _window.ChbItemNazwa.Visibility = Visibility.Hidden;

            _window.LblItemCena.Visibility = Visibility.Visible;
            _window.LblItemIlosc.Visibility = Visibility.Visible;
            _window.LblItemKategoria.Visibility = Visibility.Visible;
            _window.LblItemNazwa.Visibility = Visibility.Visible;

            _window.TxbItemCenaMax.Visibility = Visibility.Hidden;
            _window.TxbItemCenaMin.Visibility = Visibility.Visible; 
            _window.TxbItemIlosc.Visibility = Visibility.Visible;
            _window.TxbItemINazwa.Visibility = Visibility.Visible;

            _window.CmbItemKategoria.Visibility = Visibility.Visible;

            _window.BtnItemDodaj.Visibility = Visibility.Visible;
            _window.BtnItemModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnItemSzukaj.Visibility = Visibility.Hidden;
        }

        internal void ShowItemsAll()
        {
            _window.ChbItemCena.Visibility = Visibility.Hidden;
            _window.ChbItemIlosc.Visibility = Visibility.Hidden;
            _window.ChbItemKategoria.Visibility = Visibility.Hidden;
            _window.ChbItemNazwa.Visibility = Visibility.Hidden;

            _window.LblItemCena.Visibility = Visibility.Hidden;
            _window.LblItemIlosc.Visibility = Visibility.Hidden;
            _window.LblItemKategoria.Visibility = Visibility.Hidden;
            _window.LblItemNazwa.Visibility = Visibility.Hidden;

            _window.TxbItemCenaMax.Visibility = Visibility.Hidden;
            _window.TxbItemCenaMin.Visibility = Visibility.Hidden; _window.TxbItemCenaMax.Visibility = Visibility.Hidden;
            _window.TxbItemIlosc.Visibility = Visibility.Hidden;
            _window.TxbItemINazwa.Visibility = Visibility.Hidden;

            _window.CmbItemKategoria.Visibility = Visibility.Hidden;

            _window.BtnItemDodaj.Visibility = Visibility.Hidden;
            _window.BtnItemModyfikuj.Visibility = Visibility.Hidden;
            _window.BtnItemSzukaj.Visibility = Visibility.Hidden;
            
        }

        //OTHERS TODO

        public void ChangeCategoryData()
        {
            throw new NotImplementedException();
        }

        public void ChangeClientData()
        {
            throw new NotImplementedException();
        }

        public void ChangeItemData()
        {
            throw new NotImplementedException();
        }



        public void GetClientData()
        {
            throw new NotImplementedException();
        }

        public void GetClientTransactionData()
        {
            throw new NotImplementedException();
        }

        public void GetItemData()
        {
            Task<IEnumerable<Artykul>>.Factory.StartNew(() =>
            {
                return _comm.GetItems();
            }).ContinueWith(x =>
            {
                Task.Factory.StartNew(() =>
                {
                    ShowItemsData(x.Result);
                });
            });
        }

        private void ShowItemsData(IEnumerable<Artykul> categories)
        {
            _window.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Artykul r in categories)
                    _window.DgItemLista.Items.Add(r);

            }));
        }





        public void SetCategoryData()
        {
            throw new NotImplementedException();
        }

        public void SetClientData()
        {
            throw new NotImplementedException();
        }

        public void SetClientTransactionData()
        {
            throw new NotImplementedException();
        }

        public void SetItemData()
        {
            throw new NotImplementedException();
        }

        public void UpdateCategoryData()
        {
            throw new NotImplementedException();
        }

        public void UpdateClientData()
        {
            throw new NotImplementedException();
        }

        public void UpdateClientTransactionData()
        {
            throw new NotImplementedException();
        }

        public void UpdateItemData()
        {
            throw new NotImplementedException();
        }

        public void UpdateMagazineState()
        {
            throw new NotImplementedException();
        }
    }
}
