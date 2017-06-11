using Client.Interfaces;
using System;
using System.Collections.Generic;
using Client.Model;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using static System.Diagnostics.Debug;

namespace Client.Communication
{
    class CommItems: ICommItems
    {
        private static CommItems _instance;
        private string urlAddress {  get; } = ("http://c414305-001-site1.btempurl.com");

        protected CommItems() { }

        public static CommItems GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommItems();
            }
            return _instance;
        }

        public IEnumerable<Artykul> GetItems()
        {
            try
            {
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
               WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  GetItems: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in GetItems");
        }

        public bool ChangeItem(Artykul artykul)
        {
            try
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
            }
            catch (Exception ex)
            {
                WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  ChangeItem: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in ChangeAddress");
        }

        public bool RegisterItem(Artykul artykul)
        {
            try
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
            }
            catch (Exception ex)
            {
               WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  RegisterItem: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in RegisterItem");
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
               WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  DeleteItem: {ex}{Environment.NewLine}");
            }
        }
    }
}
