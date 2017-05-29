using Client.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Client.Communication
{
    public sealed class Communicator : ICommunication
    {
        private static Communicator _instance;

        private Communicator()
        {
        }

        public static Communicator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Communicator();
            }
            return _instance;
        }

        private string urlAddress;

        public void SetUrlAddress(string URL)
        {
            urlAddress = URL;
        }

        public bool ChangeAddress(Adres adres)
        {
            string baseUrl = $"{urlAddress}/api/Adresss";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(adres);
            var response = client.Execute(request);
            if (response.Content.Equals("true")) return true;
            if (response.Content.Equals("false")) return false;
            throw new Exception("Exception in ChangeAddress");
        }

        public bool ChangeCategory(Kategoria kategoria)
        {
            string baseUrl = $"{urlAddress}/api/Category";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(kategoria);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            throw new Exception("Exception in ChangeCategory");
        }

        public bool ChangeClient(Klient klient)
        {
            string baseUrl = $"{urlAddress}/api/Clients";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(klient);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            throw new Exception("Exception in ChangeClient");
        }

        public bool ChangeItem(Artykul artykul)
        {
            string baseUrl = $"{urlAddress}/api/Item";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(artykul);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.NotModified)) return false;
            throw new Exception("Exception in ChangeItem");
        }

        public bool ChangeSupplier(Dostawca dostawca)
        {
            string baseUrl = $"{urlAddress}/api/Supply";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(dostawca);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.NotModified)) return false;
            throw new Exception("Exception in ChangeSupplier");
        }

        public Adres GetAddress(int id)
        {
            string baseUrl = $"{urlAddress}/api/Adresss/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Adres>(response.Content);
        }

        public IEnumerable<Kategoria> GetCategories()
        {
            string baseUrl = $"{urlAddress}/api/Category";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Kategoria>>(response.Content);
        }

        public IEnumerable<Artykul> GetItems()
        {
            string baseUrl = $"{urlAddress}/api/Item";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Artykul>>(response.Content);
        }

        public Dostawca GetSupplier(int id)
        {
            string baseUrl = $"{urlAddress}/api/Supply/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Dostawca>(response.Content);
        }

        public IEnumerable<Dostawca> GetSuppliers()
        {
            string baseUrl = $"{urlAddress}/api/Supply";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Dostawca>>(response.Content);
        }

        public Transakcja GetTransaction(int id)
        {
            string baseUrl = $"{urlAddress}/api/transaction/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Transakcja>(response.Content);
        }

        public IEnumerable<Artykul_w_transakcji> GetTransItems()
        {
            string baseUrl = $"{urlAddress}/api/transitem";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Artykul_w_transakcji>>(response.Content);
        }

        public IEnumerable<Transakcja> GetTransactions()
        {
            string baseUrl = $"{urlAddress}/api/transaction";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Transakcja>>(response.Content);
        }

        public int RegisterAddress(Adres adres)
        {
            string baseUrl = $"{urlAddress}/api/Adresss";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(adres);
            IRestResponse response = client.Execute(request);
            return Int32.Parse(response.Content);
        }

        public bool RegisterCategory(Kategoria kategoria)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Category";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(kategoria);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.Equals(HttpStatusCode.Created)) return true;
                if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Exception in RegisterCategory");
            }
            return false;
        }

        public int RegisterClient(KlientAdress adres)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Clients";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                //request.AddJsonBody(klient);
                request.AddJsonBody(adres);
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Request: {request.ToString()}{Environment.NewLine}");
                IRestResponse response = client.Execute(request);
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}response: {response.Content}{Environment.NewLine}{response}");
                if (response.StatusCode.Equals(HttpStatusCode.Created)) return Int32.Parse(response.Content);
                if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Exception in RegisterClient{Environment.NewLine}{ex}{Environment.NewLine}");
                return 0;
            }
            return 0;
        }

        public bool RegisterEmployee(PracownikAdress adres)
        {
            string baseUrl = $"{urlAddress}/api/Employee";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(adres);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.Created)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            throw new Exception("Exception in RegisterEmployee");
        }

        public bool RegisterItem(Artykul artykul)
        {
            string baseUrl = $"{urlAddress}/api/Item";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(artykul);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.Created)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            throw new Exception("Exception in RegisterItem");
        }

        public bool RegisterSupplier(Dostawca dostawca)
        {
            string baseUrl = $"{urlAddress}/api/Supply";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(dostawca);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.Created)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            throw new Exception("Exception in RegisterSupplier");
        }

        public int RegisterTransaction(Transakcja transakcja)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/transaction";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(transakcja);
                IRestResponse response = client.Execute(request);
                return Int32.Parse(response.Content);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Exception in RegisterTransaction");
                return 0;
            }
        }

        public bool RegisterTransItems(IEnumerable<Artykul_w_transakcji> artykul_w_transkacji)
        {
            string baseUrl = $"{urlAddress}/api/TransItem";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(artykul_w_transkacji);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            throw new Exception("Exception in RegisterTransItems");
        }

        public IEnumerable<Klient> GetClients()
        {
            string baseUrl = $"{urlAddress}/api/Clients";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Klient>>(response.Content);
        }

        public void DeleteCategory(Kategoria selectedItem)
        {
            string baseUrl = $"{urlAddress}/api/Category{selectedItem.idKategorii}";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            client.Execute(request);
        }

        public void DeleteClient(int id)
        {
            string baseUrl = $"{urlAddress}/api/Clients" + id; ;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            client.Execute(request);
        }

        public void ModifyEmployee(PracownikAdress adres)
        {
            string baseUrl = $"{urlAddress}/api/Employee";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            request.AddJsonBody(adres);
            client.Execute(request);
        }

        public void DeleteEmployee(int idPracownika)
        {
            string baseUrl = $"{urlAddress}/api/Employee" + idPracownika;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            client.Execute(request);
        }

        public IEnumerable<Pracownik> GetEmpoyees()
        {
            string baseUrl = $"{urlAddress}/api/Employee";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Pracownik>>(response.Content);
        }

        public void DeleteItem(int idArtykulu)
        {
            string baseUrl = $"{urlAddress}/api/Items" + idArtykulu; ;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            client.Execute(request);
        }
    }
}