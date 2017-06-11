using Client.Interfaces;
using System;
using System.Collections.Generic;
using Client.Model;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using static System.Diagnostics.Debug;

namespace Client.Communication
{
    class CommClient :ICommClient
    {
        private static CommClient _instance;
        private string urlAddress { get; } = ("http://c414305-001-site1.btempurl.com");
        protected CommClient() { }

        public static CommClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommClient();
            }
            return _instance;
        }

        public bool ChangeClient(Klient klient)
        {
            try
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
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("500"))
                {
                    throw new Exception("Server Error");
                }
                else
                    WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  ChangeClient: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in ChangeClient");
        }

        public void DeleteClient(int id)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Clients/{id}";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("cache-control", "no-cache");
                client.Execute(request);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("500"))
                {
                    throw new Exception("Server Error");
                }
                else
                    WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  DeleteClient: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in DeleteClient");
        }

        public IEnumerable<Klient> GetClients()
        {
            try
            {
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
                if (ex.Message.Contains("500"))
                {
                    throw new Exception("Server Error");
                }
                else
                    WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  GetClients: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in GetClients");
        }

        public int RegisterClient(KlientAdress klientAdres)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Clients";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");               
                request.AddJsonBody(klientAdres);
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Request: {request.ToString()}{Environment.NewLine}");
                IRestResponse response = client.Execute(request);
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}response: {response.Content}{Environment.NewLine}{response}");
                if (response.StatusCode.Equals(HttpStatusCode.Created)) return Int32.Parse(response.Content);
                if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return 0;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("500"))
                {
                    throw new Exception("Server Error");
                }
                else
                    WriteLine($"{Environment.NewLine}Exception in {nameof(_instance)}  RegisterClient: {ex}{Environment.NewLine}");
                return 0;
            }
            return 0;
        }

        public bool ChangeAddress(Adres adres)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Adres";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.PUT);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(adres);
                var response = client.Execute(request);
                if (response.StatusCode.Equals(HttpStatusCode.OK)) return true;
                if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("500"))
                {
                    throw new Exception("Server Error");
                }
                else
                    WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  ChangeAddress: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in ChangeAddress");
        }
    }
}
