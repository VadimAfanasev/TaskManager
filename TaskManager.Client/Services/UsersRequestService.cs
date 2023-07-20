using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services
{
    public class UsersRequestService : CommonRequestService
    {
        private string _usersControllerUrl = HOST + "users";        

        public AuthToken GetToken(string userName, string password) 
        {
            string url = HOST + "account/token";
            string resultStr = GetDataByUrl(HttpMethod.Post, url, null, userName, password);
            AuthToken token = JsonConvert.DeserializeObject<AuthToken>(resultStr);
            return token;
        }

        public UserModel GetCurrent(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, HOST + "account/info", token);
            UserModel users = JsonConvert.DeserializeObject<UserModel>(response);
            return users;
        }

        public HttpStatusCode CreateUser(AuthToken token, UserModel user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            var result = SendDataByUrl(HttpMethod.Post, _usersControllerUrl, token, userJson);
            return result;
        }

        public List<UserModel> GetAllUsers(AuthToken token) 
        {
            string response = GetDataByUrl(HttpMethod.Get, _usersControllerUrl, token);
            List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(response);
            return users;
        }

        public HttpStatusCode DeleteUser(AuthToken token, int userId)
        {
            var result = DeleteDataByUrl(_usersControllerUrl + $"/{userId}", token);
            return result;
        }

        public HttpStatusCode CreateMultipleUsers(AuthToken token, List<UserModel> users)
        {
            string userJson = JsonConvert.SerializeObject(users);
            var result = SendDataByUrl(HttpMethod.Post, _usersControllerUrl + "/all", token, userJson);
            return result;
        }

        public HttpStatusCode UpdateUser(AuthToken token, UserModel user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            var result = SendDataByUrl(HttpMethod.Patch, _usersControllerUrl + $"/{user.Id}", token, userJson);
            return result;
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
