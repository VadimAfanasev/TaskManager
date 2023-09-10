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

        public UserModel GetCurrentUser(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, HOST + "account/info", token);
            UserModel users = JsonConvert.DeserializeObject<UserModel>(response);
            return users;
        }

        public UserModel GetUserById(AuthToken token, int userId)
        {
            string response = GetDataByUrl(HttpMethod.Get, _usersControllerUrl + $"/{userId}", token);
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

        public int? GetProjectUserAdmin(AuthToken token, int userId)
        {
            var result = GetDataByUrl(HttpMethod.Get, _usersControllerUrl + $"/{userId}/admin", token);

            int adminId;

            bool parseResult = int.TryParse(result, out adminId);

            if (parseResult)
                return adminId;
            else 
                return null;
        }
    }
}
