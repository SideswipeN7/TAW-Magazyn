using Client.Communication;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.Controller
{
    public sealed class Manager :IManager
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


        internal void SearchMagazineSate()
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
               
                for(int i =0;i< list.Count;i++)
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
