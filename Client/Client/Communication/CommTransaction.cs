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
    class CommTransaction: ICommTransaction
    {
        private static CommTransaction _instance;
        private string urlAddress {  get; } = ("http://c414305-001-site1.btempurl.com");

        protected CommTransaction() { }

        public static CommTransaction GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommTransaction();
            }
            return _instance;
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Error in {nameof(_instance)} RegisterTransaction: {ex}{Environment.NewLine}");
                return 0;
            }
        }

        public IEnumerable<Transakcja> GetTransactions()
        {
            try
            {
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
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine} in {nameof(_instance)}  GetTransactions: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in GetTransactions");
        }
    }
}
