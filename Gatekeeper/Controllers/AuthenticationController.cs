using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gatekeeper.Services;
using System.Threading.Tasks;
using Gatekeeper.Models.Api.Requests;
using Gatekeeper.Models.Api.Responses;

namespace Gatekeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IJwtTokenService _jwtService;
        private IAuthenticationService _authService;

        public AuthenticationController(IJwtTokenService jwtService, IAuthenticationService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var loginResponse = await _authService.AuthenticateUser(request);

            if (loginResponse == null)
                return Unauthorized();

            return Ok(new
            {
                token = _jwtService.GenerateJwt()
            });
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLogin(CreateLoginRequest request)
        {
            var response = await _authService.TryCreateLogin(request.Username, request.Password);

            if (response.Success)
            {
                return Ok(new LoginResponse()
                {
                    Id = 666, // TODO
                    Username = request.Username
                });
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}