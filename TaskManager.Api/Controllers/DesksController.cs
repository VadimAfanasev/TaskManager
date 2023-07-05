using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public DesksController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<CommonModel>> GetDeskForCurrentUser()
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
        }

        [HttpGet("project/{projectId}")]
        public IActionResult GetProjectDesks(int projectId)
        {
            
        }

        [HttpPost]
        public IActionResult Create([FromBody] DeskModel deskModel)
        {

        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] DeskModel deskModel)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
        }
    }
}
