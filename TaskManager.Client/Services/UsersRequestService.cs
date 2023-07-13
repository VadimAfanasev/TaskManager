using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Documents;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services
{
    public class UsersRequestService
    {
        private const string HOST = "http://localhost:54067/api/";
        private string _userController = HOST + "users";

        private string GetDataByUrl(string url, string userName = null, string password = null)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";

            if (userName != null && password != null)
            {
                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));
                request.Headers.Add("Authorization", "Basic " + encoded);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader  = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string responseStr = reader.ReadToEnd();
                result = responseStr;
            }
            return result;
        }

        private HttpStatusCode DoActionDataByUrl(string url, AuthToken token, string data, HttpMethod method)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            if (method == HttpMethod.Post)
                result = client.PostAsync(url, content).Result;
            if (method == HttpMethod.Patch)
                result = client.PatchAsync(url, content).Result;
            if (method == HttpMethod.Delete)
                result = client.DeleteAsync(url).Result;

            return result.StatusCode;
        }

        public AuthToken GetToken(string userName, string password) 
        {
            string url = HOST + "account/token";
            string resultStr = GetDataByUrl(url, userName, password);
            AuthToken token = JsonConvert.DeserializeObject<AuthToken>(resultStr);
            return token;
        }

        public HttpStatusCode CreateUser(AuthToken token, UserModel user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            var result = DoActionDataByUrl(_userController, token, userJson, HttpMethod.Post);
            return result;
        }

        public List<UserModel> GetAllUsers(AuthToken token) 
        {
            string response = GetDataByUrl(_userController);
            List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(response);
            return users;
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
