using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Common.Models;
using System.Net;

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

        [TestMethod()]
        public void GetTaskByIdTest()
        {
            var task = _service.GetTaskById(_token, 10);

            Assert.AreNotEqual(null, task);
        }

        [TestMethod()]
        public void GetTaskByDeskTest()
        {
            var task = _service.GetTaskByDesk(_token, 8);
            Assert.AreNotEqual(0, task.Count);
        }

        [TestMethod()]
        public void CreateTaskTest()
        {
            var task = new TaskModel("Задача", "Описание задачи", DateTime.Now, DateTime.Now, "New");
            task.DeskId = 9;
            task.ExecutorId = 4;

            var result = _service.CreateTask(_token, task);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateTaskTest()
        {
            var task = new TaskModel("Задача из теста", "Описание задачи из теста", DateTime.Now, DateTime.Now, "In Progress");
            task.Id = 10;
            task.ExecutorId = 10;

            var result = _service.UpdateTask(_token, task);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteTaskByIdTest()
        {
            var result = _service.DeleteTask(_token, 10);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}