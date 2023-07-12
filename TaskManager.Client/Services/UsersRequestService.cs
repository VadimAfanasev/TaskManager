using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;

namespace TaskManager.Client.Services
{
    public class UsersRequestService
    {
        private const string HOST = "http://localhost:54067/api/";

        private async Task<string> GetDataByUrl(string url, string userName, string password)
        {
            var client = new HttpClient();
            var authToken = Encoding.UTF8.GetBytes(userName + ":" + password);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            var response = await client.PostAsync(url, new StringContent(authToken.ToString()));
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public AuthToken GetToken(string userName, string password)
        {
            string url = HOST + "account/token";
            var resultStr = GetDataByUrl(url, userName, password);
            AuthToken token = JsonConvert.DeserializeObject<AuthToken>(resultStr.Result);
            return token;
        }
    }
}
