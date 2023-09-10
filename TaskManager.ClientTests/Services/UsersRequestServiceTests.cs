using TaskManager.Client.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TaskManager.Common.Models;
using TaskManager.Client.Models;

namespace TaskManager.Client.Services.Tests
{
    [TestClass()]
    public class UsersRequestServiceTests
    {
        private AuthToken _token;
        private UsersRequestService _service;
        public UsersRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("trim-agency@yandex.ru", "qwerty123");
            _service = new UsersRequestService();
        }

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
        public void GetAllUsersTest()
        {
            var service = new UsersRequestService();
            var token = service.GetToken("trim-agency@yandex.ru", "qwerty123");

            var result = service.GetAllUsers(token);
            Console.WriteLine(result.Count);
            Assert.AreNotEqual(Array.Empty<UserModel>(), result.ToArray());
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            var service = new UsersRequestService();
            var token = service.GetToken("trim-agency@yandex.ru", "qwerty123");

            var result = service.DeleteUser(token, 7);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void CreateMultipleUsersTest()
        {
            var service = new UsersRequestService();
            var token = service.GetToken("trim-agency@yandex.ru", "qwerty123");

            UserModel userTest1 = new UserModel("Boris", "Britva", "borBrit@gmail.com", "HrenPopadesh", UserStatus.User, "+79521654820");
            UserModel userTest2 = new UserModel("Tony", "BulletTooth", "motherfucker93@mail.ru", "fuckfuckfuck", UserStatus.Editor, "+9134576231");
            UserModel userTest3 = new UserModel("Black", "Swordsman", "gats@yandex.ru", "killalldemons", UserStatus.Editor, "+9166669999");

            List<UserModel> users = new List<UserModel>() { userTest1, userTest2, userTest3 };
            var result = service.CreateMultipleUsers(token, users);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            var service = new UsersRequestService();
            var token = service.GetToken("trim-agency@yandex.ru", "qwerty123");

            UserModel userTest = new UserModel("Tony", "BulletTooth", "motherfucker93@mail.ru", "fuckfuckfuck", UserStatus.Editor, "+79134576231");
            userTest.Id = 14;

            var result = service.UpdateUser(token, userTest);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void GetProjectUserAdminTest()
        {
            var id = _service.GetProjectUserAdmin(_token, 4);
            Assert.AreEqual(5, id);
        }
    }
}