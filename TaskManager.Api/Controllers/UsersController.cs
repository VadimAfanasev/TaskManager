using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models.Data;
using TaskManager.Api.Models.Services;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;
        public UsersController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult TestApi()
        {
            return Ok("Сервер запущен. Время запуска:" + DateTime.Now);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _usersService.Create(userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _usersService.Update(id, userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            bool result = _usersService.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUser(int id)
        {
            var user = _usersService.Get(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            var result = await _db.Users.Select(x => x.ToDto()).ToListAsync();
            return Ok(result);
        }

        [HttpPost("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMultipleUsers([FromBody] List<UserModel> userModel)
        {
            if (userModel != null && userModel.Count > 0) 
            {
                bool result = _usersService.CreateMultipleUsers(userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpGet("{id}/admin")]
        public ActionResult<int?> GetProjectAdminId(int id)
        {
            var admin = _usersService.GetProjectAdmin(id);
            return admin == null ? NotFound(null) : Ok(admin.Id);  
        }

    }
}
