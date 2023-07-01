﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Data;
using TaskManager.Api.Models.Services;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;
        private readonly ProjectsService _projectsService;
        public ProjectsController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
            _projectsService = new ProjectsService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectModel>> Get()
        {
            return await _db.Projects.Select(x => x.ToDto()).ToListAsync();
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProjectModel projectModel)
        {
            if (projectModel != null)
            {
                var user = _usersService.GetUser(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    var admin = _db.ProjectAdmins.FirstOrDefault(x => x.UserId == user.Id);
                    if (admin == null) 
                    {
                        admin = new ProjectAdmin(user);
                        _db.ProjectAdmins.Add(admin);
                    }
                    projectModel.AdminId = admin.Id;
                }
                bool result = _projectsService.Create(projectModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
            
        }

        [HttpPatch]
        public IActionResult Update(int id, [FromBody] ProjectModel projectModel)
        {
            if (projectModel != null)
            {
                bool result = _projectsService.Update(id, projectModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();          
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
                bool result = _projectsService.Delete(id);
                return result ? Ok() : NotFound();         
        }
    }
}
