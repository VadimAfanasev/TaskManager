using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TaskManager.Api.Models.Data;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using TaskManager.Api.Models.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Api.Models;
using TaskManager.Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UserService _userService;
        public AccountController(ApplicationContext db)
        {
            _db = db;
            _userService = new UserService(db);
        }

        [Authorize]
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo() 
        {
            string userName = HttpContext.User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.Email == userName);
            if (user != null) 
            {
                return Ok(user.ToDto());
            }
            return BadRequest();
        }

        [HttpPost("token")]
        public IActionResult GetToken() 
        {
            var userData = _userService.GetUserLoginPassFromBasicAuth(Request);
            var login = userData.Item1;
            var password = userData.Item2;
            var identity = _userService.GetIdentity(login, password);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPatch("update")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                string userName = HttpContext.User.Identity.Name;
                User userForUpdate = _db.Users.FirstOrDefault(x => x.Email == userName);
                if (userForUpdate != null)
                {
                    userForUpdate.FirstName = userModel.FirstName;
                    userForUpdate.LastName = userModel.LastName;
                    userForUpdate.Password = userModel.Password;
                    userForUpdate.Phone = userModel.Phone;
                    userForUpdate.Photo = userModel.Photo;

                    _db.Users.Update(userForUpdate);
                    _db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
