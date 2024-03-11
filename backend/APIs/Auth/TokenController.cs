using System.Threading.Tasks;
using backend.Exceptions;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.APIs.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Creates  accessToken and refreshToken
        /// </summary>
        /// <response code="200">Returns the accessToken as a string and a refreshToken object that contains the refreshToken and meta datas.</response>
        /// <response code="400">If the request model validation failed</response>
        /// <param name="request">An object of CreateAuthTokensRequest</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "userName": "JohnDoe",
        ///       "password": "AtLeast6Character"
        ///     }
        /// </remarks>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAuthTokensAsync([FromBody] CreateAuthTokensRequest request)
        {
            var authTokensResponse = await _tokenService.CreateAuthTokensAsync(request);
            return Ok(authTokensResponse);
        }

        /// <summary>
        /// Creates a new accessToken
        /// </summary>
        /// <response code="200">Returns a new accessToken.</response>
        /// <response code="400">
        ///     If the request model validation failed. 
        ///     The supplied refreshToken does not exist or has expired. 
        ///     The accessToken and the refreshToken owners are different.
        /// </response>
        /// <param name="request">An object of RefreshAccessTokenRequest</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSm9obkRvZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJuYmYiOjE2MTA0NjIwMDIsImV4cCI6MTYxMDQ2MjEyMiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3QiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdCJ9.aD7qM2Qy_3dBUk6ps1e5fhGdaxMHIqQaZA8adi_oEII",
        ///       "refreshToken": {
        ///         "id": "7350c288-ca4c-464e-8311-d703228420eb",
        ///         "userName": "JohnDoe",
        ///         "token": "qmtd4qMikztW2NIwn0I6iIznuzvIc8Sk5XvcFyT0KwQ=",
        ///         "created": 1610462002293,
        ///         "expires": 1626014002293
        ///       }
        ///     }
        /// </remarks>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessTokenAsync([FromBody] RefreshAccessTokenRequest request)
        {
            try
            {
                string accessToken = await _tokenService.RefreshAccessTokenAsync(request);
                return Ok(new { accessToken });
            }
            catch (RequestValidationException e)
            {
                return BadRequest(new { errors = e.Errors });
            }
        }

        /// <summary>
        ///     Revokes the refreshToken. Client will not be able to use the given refreshToken to issue a new accessToken.
        /// </summary>
        /// <response code="200">Returns a new accessToken.</response>
        /// <response code="400">
        ///     If the request model validation failed. 
        ///     The supplied refreshToken id is not a valid Guid.
        /// </response>
        /// <param name="refreshTokenID">A string that is a valid Guid and is an existing refreshToken id.</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     "7350c288-ca4c-464e-8311-d703228420eb"
        /// </remarks>

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] string refreshTokenID)
        {
            await _tokenService.RevokeRefreshTokenAsync(refreshTokenID);
            return Ok(new { code = "RefreshTokenRevoked", description = "The given refresh token is revoked or has been revoked." });
        }
    }
}
