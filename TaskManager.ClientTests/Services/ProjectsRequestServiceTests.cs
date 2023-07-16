using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services.Tests
{
    [TestClass()]
    public class ProjectsRequestServiceTests
    {
        private AuthToken _token;
        private ProjectsRequestService _service;

        public ProjectsRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("trim-agency@yandex.ru", "qwerty123");
            _service = new ProjectsRequestService();
        }

        [TestMethod()]
        public void GetAllProjectsTest()
        {
            var projects = _service.GetAllProjects(_token);

            Console.WriteLine(projects.Count);
            Assert.AreNotEqual(Array.Empty<ProjectModel>(), projects);
        }

        [TestMethod()]
        public void GetProjectByIdTest()
        {
            var project = _service.GetProjectById(_token, 8);

            Assert.AreNotEqual(null, project);
        }

        [TestMethod()]
        public void CreateProjectByIdTest()
        {
            ProjectModel project = new ProjectModel("Тестовый проект", "Новый тестовый проект", ProjectStatus.InProgress);
            project.AdminId = 18;
            var result = _service.CreateProject(_token, project);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateProjectTest()
        {
            ProjectModel project = new ProjectModel("Тестовый проект. Обновленный", "Новый тестовый проект. Обновленный", ProjectStatus.InProgress);
            project.Id = 10;
            var result = _service.UpdateProject(_token, project);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteProjectTest()
        {
            var result = _service.DeleteProject(_token, 11);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }
        
        [TestMethod()]
        public void AddUsersToProjectTest()
        {
            var result = _service.AddUsersToProject(_token, 10, new List<int> { 15, 16, 17});

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void RemoveUsersFromProjectTest()
        {
            var result = _service.RemoveUsersFromProject(_token, 10, new List<int> { 17 });

            Assert.AreEqual(HttpStatusCode.OK, result);
        }


    }
}