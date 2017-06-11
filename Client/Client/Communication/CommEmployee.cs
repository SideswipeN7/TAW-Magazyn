using Client.Interfaces;
using System;
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
    class CommEmployee : ICommEmployee
    {
        private static CommEmployee _instance;
        private string urlAddress {  get; } = ("http://c414305-001-site1.btempurl.com");

        protected CommEmployee() { }

        public static CommEmployee GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommEmployee();
            }
            return _instance;
        }

        public void DeleteEmployee(int idPracownika)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Employee/{idPracownika}";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("cache-control", "no-cache");
                client.Execute(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in DeleteEmployee: {ex}{Environment.NewLine}");
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
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in GetEmployees: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in GetEmployees");
        }

        public void ModifyEmployee(PracownikAdress PracownikAdres)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Employee";
                var client = new RestClient(baseUrl);
                var request = new RestRequest();
                request.Method = Method.PUT;
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(PracownikAdres);
                client.Execute(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in ModifyEmployee: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in ModifyEmployee");
        }

        public bool RegisterEmployee(PracownikAdress PracownikAdres)
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Employee";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(PracownikAdres);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.Equals(HttpStatusCode.Created)) return true;
                if (response.StatusCode.Equals(HttpStatusCode.Conflict)) return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in RegisterEmplyee: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in RegisterEmployee");
        }

        
    }
}
