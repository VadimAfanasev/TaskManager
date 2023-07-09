using Microsoft.AspNetCore.Authorization;
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
    public class DesksController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;
        private readonly DesksService _deskService;

        public DesksController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
            _deskService = new DesksService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<CommonModel>> GetDeskForCurrentUser()
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                return await _deskService.GetAll(user.Id).ToListAsync();
            }
            return Array.Empty<CommonModel>();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var desk = _deskService.Get(id);
            return desk == null ? NotFound() : Ok(desk);
        }

        [HttpGet("project")] // Если убираем [HttpGet("project/{projectId}")], то прописываем параметры в ссылке запроса (в Postman)
        public async Task<IEnumerable<CommonModel>> GetProjectDesks(int projectId)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                return await _deskService.GetProjectDesks(projectId, user.Id).ToListAsync();
            }
            return Array.Empty<CommonModel>();
        }

        [HttpPost]
        public IActionResult Create([FromBody] DeskModel deskModel)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                if (deskModel != null)
                {
                    deskModel.AdminId = user.Id;
                    bool result = _deskService.Create(deskModel);
                    return result ? Ok() : NotFound();  
                }
                return BadRequest();
            }
            return Unauthorized();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] DeskModel deskModel)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                if (deskModel != null)
                {
                    bool result = _deskService.Update(id, deskModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = _deskService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
