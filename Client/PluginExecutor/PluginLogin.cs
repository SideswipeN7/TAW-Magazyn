using System;
using System.IO;
using System.Net;

namespace PluginExecutor
{
    public class PluginLogin : IPluginLogin
    {
        public string Login(string login, string password)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://c414305-001-site1.btempurl.com/api/Login");

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"login\": \"" + login + "\",\"password\": \"" + password + "\"}";
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //System.Diagnostics.Debug.WriteLine($"RECIVED: {response.Content}");
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in PluginLogin: {ex}");
                throw new Exception("ERROR in PluginLogin");
            }
        }
    }
}