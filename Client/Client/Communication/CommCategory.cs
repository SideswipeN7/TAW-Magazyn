using Client.Interfaces;
using System;
using System.Collections.Generic;
using Client.Model;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace Client.Communication
{
    class CommCategory : ICommCategory
    {
        private static CommCategory _instance;
        private string urlAddress { set; get; }

        protected CommCategory() { }

        public static CommCategory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommCategory();
            }
            _instance.urlAddress = ("http://c414305-001-site1.btempurl.com");
            return _instance;
        }

        public bool ChangeCategory(Kategoria kategoria)
        {
            try
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in ChangeCategory: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in ChangeCategory");
        }

        public void DeleteCategory(Kategoria kategoria)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Category{kategoria.idKategorii}";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("cache-control", "no-cache");
                client.Execute(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in DeleteCategory: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in DeleteCategory");
        }

        public IEnumerable<Kategoria> GetCategories()
        {
            try
            {
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
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in GetCategories: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in GetCategories");
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in RegisterCategory: {ex}{Environment.NewLine}");
            }
            return false;
        }
    }
}
