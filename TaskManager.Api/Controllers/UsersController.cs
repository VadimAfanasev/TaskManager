using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Data;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;
        public UsersController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult TestApi()
        {
            return Ok("Всем привет!");
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                User newUser = new User(userModel.FirstName, userModel.LastName, userModel.Email, 
                    userModel.Password, userModel.Status, userModel.Phone, userModel.Photo);
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPatch("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                User userForUpdate = _db.Users.FirstOrDefault(x => x.Id == id);
                if (userForUpdate != null)
                {
                    userForUpdate.FirstName = userModel.FirstName;
                    userForUpdate.LastName = userModel.LastName;
                    userForUpdate.Password = userModel.Password;
                    userForUpdate.Phone = userModel.Phone;
                    userForUpdate.Photo = userModel.Photo;
                    userForUpdate.Status = userModel.Status;
                    userForUpdate.Email = userModel.Email;

                    _db.Users.Update(userForUpdate);
                    _db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            User userForUpdate = _db.Users.FirstOrDefault(x => x.Id == id);
            if (userForUpdate != null)
            {
                _db.Users.Remove(userForUpdate);
                _db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _db.Users.Select(x => x.ToDto()).ToListAsync();
        }

        [HttpPost("create/all")]
        public async Task<IActionResult> CreateMultipleUsers([FromBody] List<UserModel> userModel)
        {
            if (userModel != null && userModel.Count > 0) 
            {
                var newUsers = userModel.Select(x => new User(x));
                _db.Users.AddRange(newUsers);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
