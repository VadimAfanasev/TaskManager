using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services.Tests
{
    [TestClass()]
    public class TasksRequestServiceTests
    {
        private AuthToken _token;
        private TasksRequestService _service;

        public TasksRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("trim-agency@yandex.ru", "qwerty123");
            _service = new TasksRequestService();
        }

        [TestMethod()]
        public void GetAllTaskTest()
        {
            var tasks = _service.GetAllTasks(_token);

            Console.WriteLine(tasks.Count);
            Assert.AreNotEqual(0, tasks.Count);
        }
    }
}