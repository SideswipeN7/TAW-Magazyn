using Client.Communication;
using Client.Model;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    class CategoryController : IWork
    {
        private static CategoryController _instance;
        Admin _window { get; set; }
        private ICommunication _comm;
        private IEnumerable<Kategoria> categories;

        private CategoryController()
        {
            _comm = Communicator.GetInstance();
            _comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
            //_comm.SetUrlAddress("http://localhost:52992");
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
            if (_window.TxbCategoryNazwa.Text.Length > 5)
            {
                if (_comm.RegisterCategory(new Kategoria() { Nazwa = _window.TxbCategoryNazwa.Text }))
                {
                    GetCategoryData();
                }
            }
        }

        public void ChangeDate()
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

        public void DeleteData()
        {
            if (_window.DgCategoryLista.SelectedIndex >= 0)
            {
                _comm.DeleteCategory((Kategoria)_window.DgCategoryLista.SelectedItem);
                GetCategoryData();
            }
        }



        public void ShowCategoryData(IEnumerable<Kategoria> categories)
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

        public void ShowData()
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

        public void CategoriesSearch()
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

        public void ShowSelectedData()
        {
            throw new NotImplementedException();
        }
    }
}
