using Arcology.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions.MonoHttp;
using System.Net;

namespace Arcology.Communication
{
    public class ExperimentClient : IExperimentClient
    {
        private const string ServiceAddress = "http://arcology.prime.future-processing.com";

        //private string[] commands = new string[] { "Import food", "Produce", "Propaganda", "Clean", "Build Arcology", "Expand Population Capacity", "Expand Food Capacity", "We are ready" };
        public Result Describe(string login, string token)
        {
            var encodedLogin = HttpUtility.UrlEncode(login);
            string baseUrl = $"{ServiceAddress}/describe?login={encodedLogin}&token={token}";

            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<Result>(response.Content);
        }

        public HttpStatusCode Execute(string login, string token)
        {
            string baseUrl = $"{ServiceAddress}/execute";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", CreateRestartCommand(login, token), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.StatusCode;
        }

        public HttpStatusCode ExecuteCommand(string login, string token, string command, int value)
        {
            string baseUrl = $"{ServiceAddress}/execute";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", CreateCommand(login, token, command, value), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.StatusCode;
        }

        private static string CreateRestartCommand(string login, string token)
        {
            return $"{{\"Command\": \"Restart\", \"Login\": \"{login}\", \"Token\": \"{token}\"}}";
        }

        private static string CreateCommand(string login, string token, string command, int value)
        {
            return $"{{\"Command\": \"{command}\", \"Login\": \"{login}\", \"Token\": \"{token}\", \"Parameter\": \"{value}\"}}";
        }
    }
}