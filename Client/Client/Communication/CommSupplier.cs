using Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Model;
using RestSharp;
using Newtonsoft.Json;

namespace Client.Communication
{
    class CommSupplier: ICommSupplier
    {
        private static CommSupplier _instance;
        private string urlAddress { set; get; }

        protected CommSupplier() { }

        public static CommSupplier GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommSupplier();
            }
            _instance.urlAddress = ("http://c414305-001-site1.btempurl.com");
            return _instance;
        }   

        public IEnumerable<Dostawca> GetSuppliers()
        {
            try
            {
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
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in GetSuppliers: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in GetSuppliers");
        }
    }
}
