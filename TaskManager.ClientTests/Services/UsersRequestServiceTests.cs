using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services.Tests
{
    [TestClass()]
    public class UsersRequestServiceTests
    {
        [TestMethod()]
        public void GetTokenTest()
        {
            var token = new UsersRequestService().GetToken("trim-agency@yandex.ru", "qwerty123");
            Console.WriteLine(token);
            Assert.IsNotNull(token);
        }

        [TestMethod()]
        public void CreateUserTest()
        {
            var service = new UsersRequestService();
            var token = service.GetToken("trim-agency@yandex.ru", "qwerty123");
            UserModel userTest = new UserModel("Michael", "Corleone", "GodFather02@gmail.com", "AlPachino", UserStatus.User, "+79521765520");
            var result = service.CreateUser(token, userTest);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void GetAllUsers()
        {
            var service = new UsersRequestService();
            var token = service.GetToken("trim-agency@yandex.ru", "qwerty123");
            var result = service.GetAllUsers(token);
            Console.WriteLine(result.Count);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}