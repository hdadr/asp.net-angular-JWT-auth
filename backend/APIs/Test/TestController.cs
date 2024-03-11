using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            var data = new List<string> { "logged1", "logged2", "logged3" };
            return Ok(new { unauthorizedData = data });
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetUsers()
        {
            var users = new List<string> { "user1", "user2", "user3" };
            return Ok(new { userRoleData = users });
        }

        [HttpGet("admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsersAdminRole()
        {
            var users = new List<string> { "admin1", "admin2", "admin3" };
            return Ok(new { adminRoleData = users });
        }
    }
}
