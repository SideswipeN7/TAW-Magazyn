using Client.Communication;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void GetMagazineState()
        {
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
