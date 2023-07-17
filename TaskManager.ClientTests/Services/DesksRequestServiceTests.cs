using TaskManager.Client.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services.Tests
{
    [TestClass()]
    public class DesksRequestServiceTests
    {
        private AuthToken _token;
        private DesksRequestService _service;

        public DesksRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("trim-agency@yandex.ru", "qwerty123");
            _service = new DesksRequestService();
        }

        [TestMethod()]
        public void GetAllDesksTest()
        {
            var desks = _service.GetAllDesks(_token);

            Console.WriteLine(desks.Count);
            Assert.AreNotEqual(Array.Empty<DeskModel>(), desks);
        }

        [TestMethod()]
        public void GetDeskByIdTest()
        {
            var desk = _service.GetDeskById(_token, 9);

            Assert.AreNotEqual(null, desk);
        }

        [TestMethod()]
        public void GetDeskByProjectTest()
        {
            var desks = _service.GetDeskByProject(_token, 9);
            Assert.AreNotEqual(0, desks.Count);
        }

        [TestMethod()]
        public void CreateDeskTest()
        {
            var desk = new DeskModel("Доска для тестов", "Описание доски для тестирования", true, new string[] { "Новая", "Готовая" });
            desk.ProjectId = 9;
            desk.AdminId = 4;

            var result = _service.CreateDesk(_token, desk);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateDeskTest()
        {
            var desk = new DeskModel("Доска для тестов. Изменение", "Описание доски для тестирования", true, new string[] { "Новая", "Готовая" });
            desk.ProjectId = 9;
            desk.AdminId = 4;
            desk.Id = 11;

            var result = _service.UpdateDesk(_token, desk);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteDeskByIdTest()
        {
            var result = _service.DeleteDeskById(_token, 10);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}