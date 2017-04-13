using System.Net;
using System.IO;
namespace PluginExecutor
{
    public class PluginLogin : IPluginLogin
    {
        public string Login(string login, string password)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://o1018869-001-site1.htempurl.com/api/Login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"login\": \"" + login + "\",\"password\": \"" + password + "\"}";
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
    }
}
