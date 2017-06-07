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

        protected Communicator()
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
            try
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ChangeAddress: {ex}");
            }
            throw new Exception("Exception in ChangeAddress");
        }

        public bool ChangeCategory(Kategoria kategoria)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Category";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(kategoria);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ChangeCategory: {ex}");
            }            
            throw new Exception("Exception in ChangeCategory");
        }

        public bool ChangeClient(Klient klient)
        {
            try {
            string baseUrl = $"{urlAddress}/api/Clients";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(klient);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ChangeClient: {ex}");
            }
            throw new Exception("Exception in ChangeClient");
        }

        public bool ChangeItem(Artykul artykul)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Item";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(artykul);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.NotModified)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ChangeItem: {ex}");
            }
            throw new Exception("Exception in ChangeAddress");
        }

        public bool ChangeSupplier(Dostawca dostawca)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Supply";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.PUT);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(dostawca);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.NotModified)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ChangeSupplier: {ex}");
            }
            throw new Exception("Exception in ChangeSupplier");
        }

        public Adres GetAddress(int id)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Adresss/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Adres>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAddress: {ex}");
            }
            throw new Exception("Exception in GetAddress");
        }

        public IEnumerable<Kategoria> GetCategories()
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Category";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Kategoria>>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetCategories: {ex}");
            }
            throw new Exception("Exception in GetCategories");
        }

        public IEnumerable<Artykul> GetItems()
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Item";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Artykul>>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetItems: {ex}");
            }
            throw new Exception("Exception in GetItems");
        }

        public Dostawca GetSupplier(int id)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Supply/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Dostawca>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetSupplier: {ex}");
            }
            throw new Exception("Exception in GetSupplier");
        }

        public IEnumerable<Dostawca> GetSuppliers()
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Supply";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Dostawca>>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetSuppliers: {ex}");
            }
            throw new Exception("Exception in GetSuppliers");
        }

        public Transakcja GetTransaction(int id)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/transaction/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Transakcja>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetTransaction: {ex}");
            }
            throw new Exception("Exception in GetTransaction");
        }

        public IEnumerable<Artykul_w_transakcji> GetTransItems()
        {
            try { 
            string baseUrl = $"{urlAddress}/api/transitem";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Artykul_w_transakcji>>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetTransItems: {ex}");
            }
            throw new Exception("Exception in GetTransItem");
        }

        public IEnumerable<Transakcja> GetTransactions()
        {
            try { 
            string baseUrl = $"{urlAddress}/api/transaction";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Transakcja>>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetTransactions: {ex}");
            }
            throw new Exception("Exception in GetTransactions");
        }

        public int RegisterAddress(Adres adres)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Adresss";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(adres);
            IRestResponse response = client.Execute(request);
            return Int32.Parse(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RegisterAdress: {ex}");
            }
            throw new Exception("Exception in RegisterAddress");
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
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in RegisterCategory: {ex}");
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
                System.Diagnostics.Debug.WriteLine($"Exception in RegisterClient: {ex}");
                return 0;
            }
            return 0;
        }

        public bool RegisterEmployee(PracownikAdress adres)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Employee";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(adres);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.Created)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RegisterEmplyee: {ex}");
            }
            throw new Exception("Exception in RegisterEmployee");
        }

        public bool RegisterItem(Artykul artykul)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Item";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(artykul);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.Created)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RegisterItem: {ex}");
            }
            throw new Exception("Exception in RegisterItem");
        }

        public bool RegisterSupplier(Dostawca dostawca)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Supply";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(dostawca);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.Created)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RegisterSupplier: {ex}");
            }
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
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RegisterTransaction: {ex}");
                return 0;
            }
        }

        public bool RegisterTransItems(IEnumerable<Artykul_w_transakcji> artykul_w_transkacji)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/TransItem";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(artykul_w_transkacji);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
            if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RegisterTransItems: {ex}");
            }
            throw new Exception("Exception in RegisterTransItems");
        }

        public IEnumerable<Klient> GetClients()
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Clients";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Klient>>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetClients: {ex}");
            }
            throw new Exception("Exception in GetClients");
        }

        public void DeleteCategory(Kategoria selectedItem)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Category{selectedItem.idKategorii}";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            client.Execute(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in DeleteCategory: {ex}");
            }
            throw new Exception("Exception in DeleteCategory");
        }

        public void DeleteClient(int id)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Clients/{id}";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            client.Execute(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in DeleteClient: {ex}");
            }
            throw new Exception("Exception in DeleteClient");
        }

        public void ModifyEmployee(PracownikAdress adres)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Employee";
            var client = new RestClient(baseUrl);
            var request = new RestRequest();
            request.Method = Method.PUT;

            System.Diagnostics.Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(adres));
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            request.AddJsonBody(adres);
            client.Execute(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ModifyEmployee: {ex}");
            }
            throw new Exception("Exception in ModifyEmployee");
        }

        public void DeleteEmployee(int idPracownika)
        {
            try { 
            string baseUrl = $"{urlAddress}/api/Employee/{idPracownika}";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");           
            client.Execute(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in DeleteEmployee: {ex}");
            }
            throw new Exception("Exception in DeleteEmployee");
        }

        public IEnumerable<Pracownik> GetEmpoyees()
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Employee";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<Pracownik>>(response.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetEmployees: {ex}");
            }
            throw new Exception("Exception in GetEmployees");
        }

        public void DeleteItem(int idArtykulu)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Item/{idArtykulu}";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("cache-control", "no-cache");

                client.Execute(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in DeleteItem: {ex}");
            }
        }
    }
}