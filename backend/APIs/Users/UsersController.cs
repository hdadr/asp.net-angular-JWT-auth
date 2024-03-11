using backend.Exceptions;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Features.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Creates a user
        /// </summary>
        /// <returns>The view model of the created user.</returns>
        /// <response code="200">Returns the newly created user view model.</response>
        /// <response code="400">If the model validation failed or the user could not be added to the database.</response>
        /// <param name="request">An object of CreateUserRequest</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "userName": "JohnDoe",
        ///       "email": "john@doe.com",
        ///       "password": "AtLeast6Character",
        ///       "confirmPassword": "AtLeast6Character",
        ///       "firstName": "John",
        ///       "lastName": "Doe"
        ///     }
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
        {
            try
            {
                var user = await _userService.CreateUserAsync(request);
                return Ok(user);
            }
            catch (RequestHandlingException e)
            {
                return BadRequest(new { message = e.Message, errors = e.Errors });
            }
        }
    }
}
