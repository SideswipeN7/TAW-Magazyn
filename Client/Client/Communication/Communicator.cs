﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Model;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
namespace Client.Communication
{
    public class Communicator : ICommunication
    {
        string urlAddress;
        public void SetUrlAddress(string URL)
        {
            urlAddress = URL;
        }

        public bool ChangeAddress(Adres adres)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            string baseUrl = $"{urlAddress}/api/Adresss/"+id;
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

        public int GetSupplier(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dostawca> GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public int GetTransaction(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool RegisterCategory(Kategoria kategoria)
        {
            throw new NotImplementedException();
        }

        public bool RegisterClient(Klient klient, Adres adres)
        {
            throw new NotImplementedException();
        }

        public bool RegisterEmployee(Pracownik pracownik, Adres adres)
        {
            throw new NotImplementedException();
        }

        public bool RegisterItem(Artykul artykul)
        {
            throw new NotImplementedException();
        }

        public bool RegisterSupplier(Dostawca dostawca)
        {
            throw new NotImplementedException();
        }

        public int RegisterTransaction(Transakcja transakcja)
        {
            throw new NotImplementedException();
        }

        public bool RegisterTransItems(IEnumerable<Artykul_w_transakcji> artykul_w_transkacji)
        {
            throw new NotImplementedException();
        }
    }
}
