using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using TaskManager.Client.Models;

namespace TaskManager.Client.Services
{
    public class UsersRequestService
    {
        private const string HOST = "http://localhost:54067/api/";

        private string GetDataByUrl(string url, string userName, string password)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            if (userName != null && password != null)
            {
                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));
                request.Headers.Add("Authorization", "Basic " + encoded);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader  = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string responseStr = reader.ReadToEnd();
                    result = responseStr;
                }
            }
            return result;
        }

        public AuthToken GetToken(string userName, string password) 
        {
            string url = HOST + "account/token";
            string resultStr = GetDataByUrl(url, userName, password);
            AuthToken token = JsonConvert.DeserializeObject<AuthToken>(resultStr);
            return token;
        }

        //private async Task<string> GetDataByUrl(string url, string userName, string password)
        //{
        //    var client = new HttpClient();
        //    var authToken = Encoding.UTF8.GetBytes(userName + ":" + password);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
        //    var response = await client.PostAsync(url, new StringContent(authToken.ToString()));
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        //public AuthToken GetToken(string userName, string password)
        //{
        //    string url = HOST + "account/token";
        //    var resultStr = GetDataByUrl(url, userName, password);
        //    AuthToken token = JsonConvert.DeserializeObject<AuthToken>(resultStr.Result);
        //    return token;
        //}
    }
}
